﻿using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    // Static reference to the instance
    public static LightManager instance = null;

    // Target of the light
    private Transform target;
    
    // Should the light keep following the target or just focus it once?
    private bool follow = false;

    // Reference to the Light component
    private Light lightComponent;

    // Save the intensity of the light
    private float baseIntensity;

	void Start () {
        // Get the component
        lightComponent = GetComponent<Light>();

        // Save the intensity value
        baseIntensity = lightComponent.intensity;

        // Initialise the instance variable
        instance = this;
	}
	
	void Update () {
        // If follow is true, rotate the light towards the current target
        if (follow) transform.LookAt(target);
	}

    // Set the target of the light
    public void SetTarget(Transform target)
    {
        Reset();

        follow = true;
        this.target = target;
    }

    // Focus a target (once, do not follow it)
    public void FocusTarget(Vector3 target)
    {
        Reset();
        follow = false;

        Quaternion lookAt = Quaternion.LookRotation(target, Vector3.up);
        Quaternion damp = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime);
         
        transform.LookAt(damp);
    }

    // Set the intensity of the light (intensity being treated as a procentual value)
    public void SetIntensity(float intensity)
    {
        lightComponent.intensity = intensity * baseIntensity;
    }

    // Reset the intensity of the light
    private void Reset()
    {
        lightComponent.intensity = baseIntensity;
    }
}
