using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class WallCode : Wall {

    public GameObject numberPrefab;

    public float maxTime = 15.0f;

    public float gap = 1.0f;

    public float chargeTime = .5f;

    private List<GameObject> numbers;

    private int currentNumber;

    private float codeX;

    private float chargingTime;

    private GazePointDataComponent gazePointDataComponent;

    protected override void Awake()
    {
        base.Awake();

        gazePointDataComponent = GetComponent<GazePointDataComponent>();
        Vector3 meshBounds = GetComponent<Renderer>().bounds.size;
        codeX = transform.position.x + meshBounds.x / 2 + numberPrefab.GetComponent<Renderer>().bounds.size.x / 2;
    }

    protected override void OnUpdate()
    {
        var lastGazePoint = gazePointDataComponent.LastGazePoint;
        Vector3 gazePointInWorldSpace = transform.position;

        if (lastGazePoint.IsValid)
        {
            Vector3 gazePointInScreenSpace = lastGazePoint.Screen;

            gazePointInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(gazePointInScreenSpace.x, gazePointInScreenSpace.y, -transform.position.x));

            LightManager.instance.FocusTarget(gazePointInWorldSpace);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = gazePointInWorldSpace - Camera.main.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {
            if (hit.collider.tag == "Number")
            {
                int index = numbers.IndexOf(hit.collider.gameObject);

                if (index == currentNumber)
                {
                    if (chargingTime >= chargeTime)
                    {
                        chargingTime = 0.0f;
                        currentNumber++;
                    } else
                    {
                        chargingTime += Time.deltaTime;
                    }
                } else if (index > currentNumber)
                {
                    for (int i = 0; i <= currentNumber; i++)
                    {
                        numbers[i].GetComponentInChildren<TextMesh>().color = Color.red;
                    }
                    currentNumber = 0;

                    chargingTime = 0.0f;
                } 
            }
            else
            {
                chargingTime -= Time.deltaTime;
            }
        }
        else
        {
            chargingTime -= Time.deltaTime;
        }

        chargingTime = Mathf.Clamp(chargingTime, 0.0f, chargeTime);
        if (currentNumber < numbers.Count) numbers[currentNumber].GetComponentInChildren<TextMesh>().color = Color.Lerp(Color.red, Color.green, chargingTime / chargeTime);
    }

    protected override bool CheckFail()
    {
        return time > maxTime;
    }

    protected override bool CheckFinish()
    {
        return currentNumber >= numbers.Count;
    }

    protected override void OnActivate()
    {
        chargingTime = 0.0f;

        currentNumber = 0;
        numbers = new List<GameObject>();

        for (int i = 1; i <= level; i++)
        {
            SpawnNumber(i);
        }
    }

    private void SpawnNumber(int number)
    {
        Vector3 position = new Vector3(codeX, UnityEngine.Random.Range(yMin, yMax), UnityEngine.Random.Range(zMin, zMax));

        int layerMask = 1 << 0;

        int count = 0;
        while (Physics.CheckSphere(position, gap, layerMask))
        {
            position = new Vector3(codeX, UnityEngine.Random.Range(yMin, yMax), UnityEngine.Random.Range(zMin, zMax));

            if (count > 10)
            {
                return;
            }
            count++;
        }
        
        GameObject numberObject = Instantiate(numberPrefab, position, Quaternion.identity) as GameObject;
        numberObject.GetComponentInChildren<TextMesh>().text = "" + number;

        numbers.Add(numberObject);
    }

    protected override void OnFail()
    {
    }

    protected override void OnFinish()
    {
    }

    protected override void OnReset()
    {
        foreach (GameObject o in numbers)
        {
            Destroy(o);
        }
    }
}
