using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currHealth;
    public float healthAsDecimal;

    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        healthAsDecimal = (currHealth / maxHealth);

        if (currHealth <= 0)
        {
            animator.SetBool("isDead", true);
            DestroyDeadUnit();
        }
	}

    public void TakeDamage (float damage)
    {
        currHealth = currHealth - damage;
    }

    public void DestroyDeadUnit ()
    {
        Destroy(this.gameObject);
    }
}
