using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currHealth;
    public float healthAsDecimal;
    public float armour;
    public float shield;
    public float damageModifier;

    public int goldAward;

    
    private Animator animator;
    private Team team;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        damageModifier = 1f;

        if (GetComponent<Pylon>()) {
            team = GetComponent<Pylon>().team;
            armour = 0;
            shield = 0;
        }
        if (GetComponent<Creep>())
        {
            team = GetComponent<Creep>().team;
            armour = GetComponent<Creep>().armour;
            shield = GetComponent<Creep>().shield;
        }
    }
	
	// Update is called once per frame
	void Update () {
        healthAsDecimal = (currHealth / maxHealth);

        if (currHealth <= 0)
        {
            animator.SetBool("isDead", true);
            if (team != PlayerManager.playerTeam)
            {
                PlayerManager.essence += (goldAward * PlayerManager.essenceModifier);
            }
            DestroyDeadUnit();
        }

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
	}

    public void TakeDamage (float damage)
    {
        currHealth = currHealth - damage;
    }

    public void Heal(float amount)
    {
       currHealth = currHealth + amount;
    }

    public void DestroyDeadUnit ()
    {
        Destroy(this.gameObject);
    }

    public float CalculateDamageTaken(float damage)
    {
        return (damage * damageModifier);
    }
    public float ReturnMissingHealth ()
    {
        float missingHealth = maxHealth - currHealth;
        return missingHealth;
    }
}
