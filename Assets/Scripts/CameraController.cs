using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public NodeUI nodeUI;

    [Header("PC Values")]
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public Vector2 panLimit;

    public float scrollSpeed = 5f;
    public float minY = -100f;
    public float maxY = 50f;
    public float minX = -50f;
    public float maxX = 75f;

    [Header("Mobile Values")]
    
    public static bool isPanning = false;
    public static bool justZoomed = false;
    //public static bool isBuildingTower = false;
    public float dragSpeed = 100f;
    //private Vector3 dragOrigin;
    private Vector2 dragOrigin;
    private float startPosX;
    private float startPosY;

    // Update is called once per frame
    void Update () {

        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
        if (PauseMenu.isPaused)
        {
            this.enabled = true;
            return;
        }

        if (Input.GetKey("w"))// || Input.mousePosition.y >= Screen.height - panBorderThickness)        Get rid of slashes for mouse usage for camera.
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("s"))//  || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("d"))//  || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey("a"))//  || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.Self);
        }

        Vector3 pos = transform.position;

        //Old Scroll below

        /*
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
        */

        //Some Mobile stuff below

        //More Old Mouse/Mobile Input
        /*
        if (Input.touchCount == 2)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isPanning = true;
            startPosX = transform.localPosition.x;
            startPosY = transform.localPosition.y;
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            isPanning = false;
            return;
        }

        if (Vector3.Distance(Input.mousePosition, dragOrigin) >= 10)
        {
            nodeUI.Hide();
        }

        if (Vector3.Distance(Input.mousePosition, dragOrigin) >= 10 && isPanning)
        {
            pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            transform.localPosition = new Vector3(startPosX - pos.x * dragSpeed, startPosY - pos.y * dragSpeed, 0);
        }*/

        if (Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                /*When a touch has first been detected, record the starting position*/
                case TouchPhase.Began:
                    isPanning = true;
                    dragOrigin = touch.position;
                    startPosX = transform.localPosition.x;
                    startPosY = transform.localPosition.y;
                    break;

                /*Determine if the touch is a moving touch*/
                case TouchPhase.Moved:
                    if (Vector3.Distance(touch.position, dragOrigin) >= 10 && isPanning)
                    {
                        //isBuildingTower = false;
                        //nodeUI.Hide();
                        pos = Camera.main.ScreenToViewportPoint(touch.position - dragOrigin);
                        //transform.localPosition = new Vector3(startPosX - pos.x * dragSpeed, startPosY - pos.y * dragSpeed, 0);
                        transform.localPosition = new Vector3(Mathf.Clamp(startPosX - pos.x * dragSpeed, minX, maxX), Mathf.Clamp(startPosY - pos.y * dragSpeed, minY, maxY), 0);
                        break;
                    } else if (justZoomed)
                    {
                        //isBuildingTower = false;
                        isPanning = true;
                        dragOrigin = touch.position;
                        startPosX = transform.localPosition.x;
                        startPosY = transform.localPosition.y;
                        justZoomed = false;
                        break;
                    } else break;


                /* Report that the touch has ended when it ends*/
                case TouchPhase.Ended:
                    isPanning = false;
                    break;
            }
        } else
        {
            isPanning = false;
        }
        

    }
}
/*
    isPanning = true;
    Touch touchZero = Input.GetTouch(0);
    dragOrigin = touchZero.position;
    startPosX = transform.localPosition.x;
    startPosY = transform.localPosition.y;

Debug.Log(Vector2.Distance(Input.GetTouch(0).position, dragOrigin));
if (Vector2.Distance(Input.GetTouch(0).position, dragOrigin) >= 10)
{
    nodeUI.Hide();
}

if (Vector2.Distance(Input.GetTouch(0).position, dragOrigin) >= 10 && isPanning)
{
    pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position - dragOrigin);
    transform.localPosition = new Vector3(startPosX - pos.x * dragSpeed, startPosY - pos.y * dragSpeed, 0);
}
*/
