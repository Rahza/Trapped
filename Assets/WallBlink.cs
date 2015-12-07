using UnityEngine;
using System.Collections;
using System;

public class WallBlink : Wall {

    public GameObject indicatorPrefab;

    public float maxTime = 10.0f;
    public float chargeTime = 0.5f;

    private Pattern[] pattern;

    private int currentIndex = 0;

    private float charge = 0.0f;

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
        return currentIndex >= pattern.Length;
    }

    protected override void OnActivate()
    {
        GameObject obj = Instantiate(indicatorPrefab, transform.position, Quaternion.identity) as GameObject;
        indicator = obj.GetComponent<Indicator>();

        charge = 0.0f;
        currentIndex = 0;

        pattern = new Pattern[level*2];

        for (int i = 0; i < pattern.Length; i++)
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
        CheckPattern();
        indicator.SetCharge(1 - (charge / chargeTime));

        if (charge >= chargeTime) {
            charge = 0.0f;
            currentIndex++;

            if (currentIndex < pattern.Length)
            {
                ShowPattern();
            }
        }
    }

    private void CheckPattern()
    {
        bool leftEye = eyePositionData.LastEyePosition.LeftEye.IsValid;
        bool rightEye = eyePositionData.LastEyePosition.RightEye.IsValid;

        Pattern currentPattern = pattern[currentIndex];

        if (leftEye == currentPattern.left && rightEye == currentPattern.right)
        {
            charge += Time.deltaTime;
        } else
        {
            charge -= Time.deltaTime;
        }

        charge = Mathf.Clamp(charge, 0.0f, chargeTime);
    }
}
