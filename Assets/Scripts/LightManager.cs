using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    // Distance between the previous and current target necessary for the light to actually move/rotate
    public float threshold = 0.5f;

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

    // Current target of the light
    private Vector3 currentTarget;

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

        float distance = 0.0f;
        if (currentTarget != null) distance = Vector3.Distance(target, currentTarget);

        if (distance >= threshold)
        {
            transform.LookAt(target);
            currentTarget = target;
        }
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
