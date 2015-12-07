using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WallSpikes : Wall {

    public GameObject spikePrefab;
    public float maxTime = 10.0f;

    public float gap = 2.0f;

    public float minSpeed = 0.1f;
    public float maxSpeed = 1.0f;

    public const float spikeOffset = 2f;

    public float failDistance = 20.0f;

    private float totalSpeed = 1.0f;

    private float spikeZ;

    private List<GameObject> spikes;
    private Spike target;

    protected override void Awake()
    {
        base.Awake();

        Vector3 meshBounds = GetComponent<Renderer>().bounds.size;
        spikeZ = transform.position.z - meshBounds.z / 2 + spikePrefab.GetComponent<Renderer>().bounds.size.z - spikeOffset;
    }

    protected override bool CheckFail()
    {
        foreach (GameObject o in spikes)
        {
            if (o.GetComponent<Spike>().distance >= failDistance)
            {
                return true;
            }
        }

        return false;
    }

    protected override bool CheckFinish()
    {
        return time > maxTime;
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnActivate()
    {
        totalSpeed = level / 3.0f;
        spikes = new List<GameObject>();

        for (int i = 0; i < level; i++)
        {
            SpawnSpike();
        }

        SetSpikeSpeed();
    }

    private void SpawnSpike()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), spikeZ);

        int count = 0;

        while (Physics.CheckSphere(position, gap))
        {
            position = new Vector3(UnityEngine.Random.Range(xMin, xMax), UnityEngine.Random.Range(yMin, yMax), spikeZ);
            
            if (count > 10)
            {
                count = 0;
                return;
            }
            count++;
        }

        GameObject spike = Instantiate(spikePrefab, position, spikePrefab.transform.rotation) as GameObject;
        spikes.Add(spike);
    }

    private void SetSpikeSpeed()
    {
        float[] rnd = new float[spikes.Count];
        float sum = 0.0f;

        for (int i = 0; i < spikes.Count; i++)
        {
            float x = UnityEngine.Random.Range(minSpeed, maxSpeed);

            rnd[i] = x;
            sum += x;
        }

        for (int i = 0; i < spikes.Count; i++)
        {
            rnd[i] = (rnd[i] / sum) * totalSpeed;
            spikes[i].GetComponent<Spike>().SetSpeed(rnd[i]);
        }
    }

    protected override void OnReset()
    {
        foreach (GameObject o in spikes)
        {
            Destroy(o);
        }
    }

    protected override void OnFail()
    {
    }

    protected override void OnFinish()
    {
    }

}
