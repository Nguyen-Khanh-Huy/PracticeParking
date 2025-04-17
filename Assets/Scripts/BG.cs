using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public float WallThickness = 0.5f;

    private void Start()
    {
        PositionWalls();
    }

    private void PositionWalls()
    {
        Camera cam = Camera.main;

        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;


        topWall.transform.position = new Vector3(0, cam.orthographicSize + WallThickness / 2f, 0);
        bottomWall.transform.position = new Vector3(0, -cam.orthographicSize - WallThickness / 2f, 0);
        leftWall.transform.position = new Vector3(-camWidth / 2f - WallThickness / 2f, 0, 0);
        rightWall.transform.position = new Vector3(camWidth / 2f + WallThickness / 2f, 0, 0);


        topWall.transform.localScale = new Vector3(WallThickness, camWidth, 1);
        bottomWall.transform.localScale = new Vector3(WallThickness, camWidth, 1);

        leftWall.transform.localScale = new Vector3(WallThickness, camHeight, 1);
        rightWall.transform.localScale = new Vector3(WallThickness, camHeight, 1);
    }

}