using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;
    public float zzz;
    //private void Start()
    //{
    //    PositionWalls();
    //}
    //private void Update()
    //{
    //    PositionWalls();
    //}

    private void PositionWalls()
    {
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));

        Debug.Log(Screen.width / 2 );


        topWall.transform.position = new Vector3(0, screenTopRight.y, 0);
        bottomWall.transform.position = new Vector3(0, screenBottomLeft.y, 0);
        leftWall.transform.position = new Vector3(screenBottomLeft.x, 0, 0);
        rightWall.transform.position = new Vector3(screenTopRight.x, 0, 0);

        topWall.transform.localScale = new Vector3(screenTopRight.x * 2, topWall.transform.localScale.y, topWall.transform.localScale.z);
        bottomWall.transform.localScale = new Vector3(screenTopRight.x * 2, bottomWall.transform.localScale.y, bottomWall.transform.localScale.z);
        leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x, screenTopRight.y * 2 , leftWall.transform.localScale.z);
        rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x, screenTopRight.y * 2, rightWall.transform.localScale.z);
    }

}