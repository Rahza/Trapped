using UnityEngine;
using System.Collections;
using System;

public class WallBlink : Wall {

    public GameObject indicatorPrefab;

    public float maxTime = 10.0f;

    private Pattern[] pattern;

    private int toSolve = 10;

    private int currentIndex = 0;

    private Indicator indicator;
    private EyePositionDataComponent eyePositionData;

    protected override void Awake()
    {
        base.Awake();
        eyePositionData = GetComponent<EyePositionDataComponent>();
    }

    protected override bool CheckFail()
    {
        return time > maxTime;
    }

    protected override bool CheckFinish()
    {
        return toSolve <= 0;
    }

    protected override void OnActivate()
    {
        GameObject obj = Instantiate(indicatorPrefab, transform.position, Quaternion.identity) as GameObject;
        indicator = obj.GetComponent<Indicator>();

        currentIndex = 0;
        toSolve = level * 2;
        pattern = new Pattern[toSolve];

        for (int i = 0; i < toSolve; i++)
        {
            pattern[i] = new Pattern();
        }

        ShowPattern();
    }

    private void ShowPattern()
    {
        indicator.SetPattern(pattern[currentIndex]);
    }

    protected override void OnFail()
    {
    }

    protected override void OnFinish()
    {
    }

    protected override void OnReset()
    {
    }

    protected override void OnUpdate()
    {
        Debug.Log(eyePositionData.LastEyePosition.LeftEye);

        if (CheckPattern()) {
            currentIndex++;

            if (currentIndex < pattern.Length)
            {
                ShowPattern();
            }
        }
    }

    private bool CheckPattern()
    {
        return false;
    }
}
