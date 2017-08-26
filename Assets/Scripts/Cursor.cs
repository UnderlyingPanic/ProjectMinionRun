using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

   

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, this.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out rayHit, Mathf.Infinity))
            {
                if (rayHit.collider.GetComponentInParent<Creep>())
                {
                    rayHit.collider.GetComponentInParent<Creep>().SetTooltipTarget();
                }
            }
        }

	}
}
