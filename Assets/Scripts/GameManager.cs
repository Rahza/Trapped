using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float cameraSpeed = 3.0f;

    public int lifes = 3;

    public static GameManager instance = null;

    public Wall[] walls;

    private Wall activeWall;
    private int activeWallId;

    private int health;

    void Start() {
        instance = this;
        health = lifes;

        InitWall();
        InitCamera();        
    }

    void InitWall()
    {
        activeWallId = 0;
        activeWall = walls[activeWallId];
        activeWall.Activate();
    }

    void InitCamera()
    {
        Vector3 position = new Vector3();

        foreach (Wall w in walls)
        {
            position += w.transform.position;
        }

        position /= walls.Length;

        Camera.main.transform.position = position;
        Camera.main.transform.LookAt(activeWall.transform);
    }

    public void WallFinished()
    {
        RotateCamera();
        if (activeWallId < walls.Length - 1)
        {
            activeWallId++;
        } else
        {
            activeWallId = 0;
        }

        activeWall = walls[activeWallId];
        activeWall.Activate();

        // UPDATE CAMERA PROTOTYPE
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        Vector3 targetPoint = activeWall.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - Camera.main.transform.position, Vector3.up);
        
        while (Quaternion.Angle(Camera.main.transform.rotation, targetRotation) > 0.5f)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, targetRotation, Time.deltaTime * cameraSpeed);
            yield return null;
        }
        
    }
}
