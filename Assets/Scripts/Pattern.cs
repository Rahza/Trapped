using UnityEngine;

public class Pattern {

    public bool left = true;
    public bool right = true;

	public Pattern()
    {
        left = Random.Range(0, 2) > 0 ? true : false;
        right = Random.Range(0, 2) > 0 ? true : false;
    }

}
