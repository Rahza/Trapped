using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    public float fixatedModifier = 3.0f;
    private float speed = 0.3f;

    private float startZ;
    public float distance = 0.0f;

    private GazeAwareComponent gazeAwareComponent;

    // Use this for initialization
    void Start () {
        gazeAwareComponent = GetComponent<GazeAwareComponent>();
        startZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        distance = startZ - transform.position.z;

        if (gazeAwareComponent.HasGaze && distance > 0)
        {
            transform.position -= transform.forward * speed * Time.deltaTime * fixatedModifier;
            //GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            //GetComponent<Renderer>().material.color = Color.red;
        }
	}

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
