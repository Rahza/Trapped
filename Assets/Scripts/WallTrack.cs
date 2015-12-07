using UnityEngine;
using System.Collections;
using System;

public class WallTrack : Wall {

    public GameObject diskPrefab;
    public float maxTime = 15.0f;
    public float distance = 2.0f;

    public float speed = 3.0f;

    public float maxCharge = 3.0f;

    private float diskZ;

    private GameObject disk;
    private Vector3 target;

    private GazeAwareComponent gazeAwareComponent;

    private float charge;

    protected override void Awake()
    {
        base.Awake();

        Vector3 meshBounds = GetComponent<Renderer>().bounds.size;
        diskZ = transform.position.z + meshBounds.z / 2;
    }

    protected override bool CheckFail()
    {
        return charge <= 0.0f;
    }

    protected override bool CheckFinish()
    {
        return time > maxTime;
    }

    protected override void OnActivate()
    {
        maxCharge /= .9f;
        charge = maxCharge;

        target = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), diskZ);
        Vector3 position = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), diskZ);
        disk = Instantiate(diskPrefab, position, diskPrefab.transform.rotation) as GameObject;

        gazeAwareComponent = disk.GetComponent<GazeAwareComponent>();

        LightManager.instance.SetTarget(disk.transform);
    }

    protected override void OnFail()
    {
    }

    protected override void OnFinish()
    {
    }

    protected override void OnReset()
    {
        Destroy(disk);
    }

    protected override void OnUpdate()
    {
        MoveDisk();
        CheckGaze();
    }

    private void MoveDisk()
    {
        if (Vector3.Distance(target, disk.transform.position) <= distance)
        {
            target = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), diskZ);
        }

        float dist = Vector3.Distance(target, disk.transform.position);
        disk.transform.position = Vector3.MoveTowards(disk.transform.position, target, Mathf.Lerp(0.0f, speed, dist / 5.0f) * Time.deltaTime);
    }

    private void CheckGaze()
    {
        if (!gazeAwareComponent.HasGaze)
        {
            charge -= Time.deltaTime;
        } else
        {
            charge += Time.deltaTime;
        }

        charge = Mathf.Clamp(charge, 0.0f, maxCharge);

        disk.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.green, charge / maxCharge);
    }
}
