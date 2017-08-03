using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour {

    public float moveSpeed;

    [HideInInspector]
    public float damage;    
    private Transform target;
    private Vector3 targetPos;
    
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        float step = moveSpeed * Time.deltaTime;

        if (target != null)
        {

            targetPos = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);


            if (Vector3.Distance(transform.position, target.position) <= 1.25f)
            {
                DealProjectileDamage();
                Destroy(this.gameObject);
            }
        } else
        {
            Destroy(this.gameObject);
        }

    }

    public void SetTarget (GameObject trgt)
    {
        target = trgt.transform;
    }

    public void DealProjectileDamage()
    {
        Health enemyHealth;

        if (!target.gameObject.GetComponent<Health>())
        {
            Debug.LogWarning(name + " has tried to deal damage to " + target.gameObject.name + " but it doesn't have health");
            return;
        }

        if (target.gameObject.GetComponent<Health>())
        {
            enemyHealth = target.gameObject.GetComponent<Health>();
            enemyHealth.TakeDamage(damage);
        }
    }
}
