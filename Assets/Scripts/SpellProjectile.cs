using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {

    public float moveSpeed;
    
    private Transform target;
    private Vector3 targetPos;
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        if (target == null)
        {
            Destroy(this.gameObject);
        }

        float step = moveSpeed * Time.deltaTime;
        targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        
        if (Vector3.Distance(transform.position, target.position) <= 1.25f)
        {
            Destroy(this.gameObject);
        }

    }

    public void SetTarget (GameObject trgt)
    {
        target = trgt.transform;
    }
}
