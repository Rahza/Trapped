using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    public static LightManager instance = null;

    private Transform target;
    private bool follow = false;

    private Light lightComponent;
    private float baseIntensity;

	// Use this for initialization
	void Start () {
        lightComponent = GetComponent<Light>();
        baseIntensity = lightComponent.intensity;

        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (follow) transform.LookAt(target);
	}

    public void SetTarget(Transform target)
    {
        Reset();

        follow = true;
        this.target = target;
    }

    public void FocusTarget(Vector3 target)
    {
        Reset();

        follow = false;
        transform.LookAt(target);
    }

    public void SetIntensity(float intensity)
    {
        lightComponent.intensity = intensity * baseIntensity;
    }

    private void Reset()
    {
        lightComponent.intensity = baseIntensity;
    }
}
