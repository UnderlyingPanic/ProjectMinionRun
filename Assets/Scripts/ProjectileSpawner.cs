using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour {

    public GameObject projectile;
    public Vector3 offset;
    [HideInInspector] 
    public GameObject target;

    private float damage;
    private GameObject spawnPoint;
    

	// Use this for initialization
	void Start ()
    {
        InitialiseSpawnPoint();
    }

    // Update is called once per frame
    void Update () {
        damage = GetComponent<Creep>().damage;
	}

    public void SpawnProjectile()
    {
        if (target == null)
        {
            return;
        }

        GameObject newProjectile = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity);
        newProjectile.GetComponent<SpellProjectile>().SetTarget(target);
        newProjectile.GetComponent<SpellProjectile>().targetHealth = target.GetComponent<Health>();
        newProjectile.GetComponent<SpellProjectile>().damage = this.GetComponent<Creep>().CalculateDamage();
        newProjectile.GetComponent<SpellProjectile>().origin = this.gameObject;
    }

    private void InitialiseSpawnPoint()
    {
        spawnPoint = new GameObject("spawnPoint");
        spawnPoint.transform.parent = this.transform;
        spawnPoint.transform.position = transform.position + offset;
    }
}
