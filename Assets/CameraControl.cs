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
    private float hScrollSens;
    private float vScrollSens;
    

    // Use this for initialization
    void Start () {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        vScrollSens = (verticalScrollPercentage / 100);
        hScrollSens = (horizontalScrollPercentage / 100);
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



        if (Input.mousePosition.x < (screenWidth * hScrollSens))
        {
            
            transform.Translate(Vector3.left * scrollSpeed);
            

        }
        if (Input.mousePosition.x > (screenWidth * (1-hScrollSens)))
        {
            
            transform.Translate(Vector3.left * -scrollSpeed);
            
        }
        if (Input.mousePosition.y < (screenHeight * vScrollSens))
        {
           
            transform.Translate(Vector3.forward * -scrollSpeed, Space.World);
            
        }
        if (Input.mousePosition.y > (screenHeight * (1 - vScrollSens)))
        {
            
            transform.Translate(Vector3.forward * scrollSpeed, Space.World); 
        }
    }
}
