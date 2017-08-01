using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour {

    public GameObject projectile;
    public Vector3 offset;
    [HideInInspector] 
    public GameObject target;

    private GameObject spawnPoint;
    

	// Use this for initialization
	void Start ()
    {
        InitialiseSpawnPoint();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SpawnProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
        newProjectile.GetComponent<SpellProjectile>().SetTarget(target);
    }

    private void InitialiseSpawnPoint()
    {
        spawnPoint = new GameObject("spawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.position = transform.position + offset;
    }
}
