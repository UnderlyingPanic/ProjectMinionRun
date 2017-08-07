using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public float minScroll;
    public float maxScroll;
    public float zoomSpeed;
    public float scrollSpeed;

    public float verticalScrollPercentage;
    public float horizontalScrollPercentage;

    private float screenWidth;
    private float screenHeight;

    

    // Use this for initialization
    void Start () {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

    }
	
	// Update is called once per frame
	void Update () {

        

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && transform.position.y > minScroll)
        {
            transform.Translate(Vector3.forward * zoomSpeed);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && transform.position.y < maxScroll)
        {
            transform.Translate(Vector3.forward * -zoomSpeed);
        }



        if (Input.mousePosition.x < (screenWidth * 0.1f))
        {
            
            transform.Translate(Vector3.left * scrollSpeed);
            

        }
        if (Input.mousePosition.x > (screenWidth * 0.9f))
        {
            
            transform.Translate(Vector3.left * -scrollSpeed);
            
        }
        if (Input.mousePosition.y < (screenHeight * 0.1f))
        {
           
            transform.Translate(Vector3.forward * -scrollSpeed, Space.World);
            
        }
        if (Input.mousePosition.y > (screenHeight *  0.9f))
        {
            
            transform.Translate(Vector3.forward * scrollSpeed, Space.World); 
        }
    }
}
