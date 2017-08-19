﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float currHealth;
    public float healthAsDecimal;
    public float armour;
    public float shield;

    public int goldAward;

    private Animator animator;
    private Team team;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

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
                PlayerManager.essence += goldAward;
            }
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
