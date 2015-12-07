using UnityEngine;
using System.Collections;

public abstract class Wall : MonoBehaviour {
    
    public float marginX = 1.0f;
    public float marginY = 1.0f;
    public float marginZ = 1.0f;

    private bool active = false;
    protected int level = 0;
    protected float time = 0.0f;

    protected float xMin, xMax, yMin, yMax, zMin, zMax;

    protected virtual void Awake()
    {
        Vector3 meshBounds = GetComponent<Renderer>().bounds.size;

        xMin = transform.position.x - meshBounds.x / 2 + marginX;
        xMax = transform.position.x + meshBounds.x / 2 - marginX;
        yMin = transform.position.y - meshBounds.y / 2 + marginY;
        yMax = transform.position.y + meshBounds.y / 2 - marginY;
        zMin = transform.position.z - meshBounds.z / 2 + marginZ;
        zMax = transform.position.z + meshBounds.z / 2 - marginZ;
    }

    // Update is called once per frame
    protected virtual void Update () {
	    if (active)
        {
            OnUpdate();

            if (CheckFinish())
            {
                Finished();
            } else if (CheckFail())
            {
                Failed();
            }

            time += Time.deltaTime;
        }
	}

    protected virtual void Finished()
    {
        active = false;
        GameManager.instance.WallFinished();

        OnFinish();
        OnReset();
    }

    protected virtual void Failed()
    {
        active = false;
        GameManager.instance.WallFinished();

        OnFail();
        OnReset();
    }

    public void Activate()
    {
        level++;
        active = true;
        time = 0.0f;

        OnActivate();
    }

    protected abstract void OnActivate();
    protected abstract void OnFinish();
    protected abstract void OnFail();

    protected abstract void OnReset();

    protected abstract void OnUpdate();

    protected abstract bool CheckFail();
    protected abstract bool CheckFinish();
}
