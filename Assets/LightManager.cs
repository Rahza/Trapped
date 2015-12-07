using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour {

    public static LightManager instance = null;

    private Transform target;
    private bool follow = false;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (follow) transform.LookAt(target);
	}

    public void SetTarget(Transform target)
    {
        follow = true;
        this.target = target;
    }

    public void FocusTarget(Vector3 target)
    {
        follow = false;
        transform.LookAt(target);
    }
}
