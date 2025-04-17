using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    // private void Start()
    // {
    //     PositionWalls();
    // }
    // private void Update()
    // {
    //     PositionWalls();
    // }

    private void PositionWalls()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        // Position walls
        topWall.transform.position = new Vector3(0, cam.orthographicSize + (topWall.GetComponent<SpriteRenderer>().bounds.size.y / 2), 0);
        bottomWall.transform.position = new Vector3(0, -cam.orthographicSize - (bottomWall.GetComponent<SpriteRenderer>().bounds.size.y / 2), 0);
        leftWall.transform.position = new Vector3(-camWidth / 2f - (leftWall.GetComponent<SpriteRenderer>().bounds.size.x / 2), 0, 0);
        rightWall.transform.position = new Vector3(camWidth / 2f + (rightWall.GetComponent<SpriteRenderer>().bounds.size.x / 2), 0, 0);

        // Scale walls
        float desiredWidth = camWidth;
        float desiredHeight = camHeight;

        SpriteRenderer topSR = topWall.GetComponent<SpriteRenderer>();
        SpriteRenderer bottomSR = bottomWall.GetComponent<SpriteRenderer>();

        float topOriginalWidth = topSR.sprite.bounds.size.x;
        float bottomOriginalWidth = bottomSR.sprite.bounds.size.x;

        topWall.transform.localScale = new Vector3(desiredWidth / topOriginalWidth, 1, 1);
        bottomWall.transform.localScale = new Vector3(desiredWidth / bottomOriginalWidth, 1, 1);
    }

}