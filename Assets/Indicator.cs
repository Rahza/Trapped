using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

    public GameObject openEyePrefab, closedEyePrefab;
    public GameObject leftIndicator, rightIndicator;

    private GameObject leftEye, rightEye;

    private Vector3 leftScale, rightScale;
    
    void Awake()
    {
        leftScale = leftIndicator.transform.localScale;
        rightScale = rightIndicator.transform.localScale;
    }

    public void SetPattern(Pattern pattern)
    {
        if (leftEye != null) Destroy(leftEye);
        if (rightEye != null) Destroy(rightEye);

        GameObject toInstantiate = pattern.left ? openEyePrefab : closedEyePrefab;
        leftEye = Instantiate(toInstantiate, leftEye.transform.position, leftEye.transform.rotation) as GameObject;

        toInstantiate = pattern.right ? openEyePrefab : closedEyePrefab;
        rightEye = Instantiate(toInstantiate, rightEye.transform.position, rightEye.transform.rotation) as GameObject;
    }
    
    public void SetCharge(float charge)
    {
        leftIndicator.transform.localScale = leftScale * charge;
        rightIndicator.transform.localScale = rightScale * charge;
    } 
}
