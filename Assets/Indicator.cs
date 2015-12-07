using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

    public GameObject leftIndicator, rightIndicator;

    Vector3 leftScale, rightScale;

    Pattern pattern;
    
    void Awake()
    {
        leftScale = leftIndicator.transform.localScale;
        rightScale = rightIndicator.transform.localScale;
    }

    public void SetPattern(Pattern pattern)
    {
        this.pattern = pattern;
        UpdateIndicator();
    }
    
    public void SetCharge(float charge)
    {
        leftIndicator.transform.localScale = leftScale * charge;
        rightIndicator.transform.localScale = rightScale * charge;
    } 

    private void UpdateIndicator()
    {
        leftIndicator.GetComponent<Renderer>().material.color = pattern.left ? Color.green : Color.red;
        rightIndicator.GetComponent<Renderer>().material.color = pattern.right ? Color.green : Color.red;
    }
}
