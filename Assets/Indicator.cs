using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {

    public GameObject leftIndicator, rightIndicator;

    Pattern pattern;
    
    public void SetPattern(Pattern pattern)
    {
        this.pattern = pattern;
        UpdateIndicator();
    } 

    private void UpdateIndicator()
    {
        leftIndicator.GetComponent<Renderer>().material.color = pattern.left ? Color.red : Color.green;
        rightIndicator.GetComponent<Renderer>().material.color = pattern.right ? Color.red : Color.green;
    }
}
