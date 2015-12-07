using UnityEngine;
using System.Collections;
using System;

public class WallBlink : Wall {

    public GameObject openEyePrefab, closedEyePrefab;

    public Material eyeBrowMaterial;

    public Transform leftPosition, rightPosition;

    public float maxTime = 10.0f;
    public float chargeTime = 0.5f;

    private Pattern[] pattern;

    private int currentIndex = 0;
    private Pattern currentPattern;

    private float charge = 0.0f;

    private GameObject leftEye, rightEye;

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
        charge = 0.0f;
        currentIndex = 0;

        pattern = new Pattern[level*2];

        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = new Pattern();
        }

        currentPattern = pattern[currentIndex];
        ShowPattern();
    }

    private void ShowPattern()
    {
        if (leftEye != null) Destroy(leftEye);
        if (rightEye != null) Destroy(rightEye);

        GameObject toInstantiate = currentPattern.left ? openEyePrefab : closedEyePrefab;
        leftEye = Instantiate(toInstantiate, leftPosition.position, leftPosition.rotation) as GameObject;

        toInstantiate = currentPattern.right ? openEyePrefab : closedEyePrefab;
        rightEye = Instantiate(toInstantiate, rightPosition.position, rightPosition.rotation) as GameObject;
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

        eyeBrowMaterial.color = Color.Lerp(Color.red, Color.green, charge/chargeTime);

        if (charge >= chargeTime) {
            charge = 0.0f;
            currentIndex++;

            if (currentIndex < pattern.Length)
            {
                currentPattern = pattern[currentIndex];
                ShowPattern();
            }
        }
    }

    private void CheckPattern()
    {
        bool leftEye = eyePositionData.LastEyePosition.LeftEye.IsValid;
        bool rightEye = eyePositionData.LastEyePosition.RightEye.IsValid;

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
