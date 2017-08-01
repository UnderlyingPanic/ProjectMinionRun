using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour {

    public GameObject projectile;
    public Vector3 offset;
    [HideInInspector] 
    public GameObject target;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position+offset, Quaternion.identity);
        newProjectile.GetComponent<SpellProjectile>().SetTarget(target);
    }
}
