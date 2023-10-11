using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only does zooming, not panning lul

public class PanZoom : MonoBehaviour {

    public float zoomOutMin = 5;
    public float zoomOutMax = 40;
    public float zoomSpeed = 20;
    public static bool isZooming = false;
    public NodeUI nodeUI;

    private void Update()
    {
        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        if(Input.touchCount == 2 && !PauseMenu.isPaused)
        {
            //nodeUI.Hide();
            //CameraController.isPanning = false;
            isZooming = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        } else if (isZooming)
        {
            CameraController.isPanning = false;
            CameraController.justZoomed = true;
            isZooming = false;
        }
        else
        {
            isZooming = false;
        }
    }

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (increment * zoomSpeed), zoomOutMin, zoomOutMax);
    }
}
    //Old Mobile Stuff that was taken str8 from a YT Video
    /*
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //Some Mobile Stuff
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        //End of Mobile Stuff
        if (Input.GetMouseButton(0)) // Change this to an else-if for mobile use.
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
	}

    void Zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
    }
}
*/