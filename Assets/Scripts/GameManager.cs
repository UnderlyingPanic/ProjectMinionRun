using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float[] team1Damage;
    public float[] team2Damage;

    public float[] team1MaxHP;
    public float[] team2MaxHP;

    //Top Melee, Top Mage, Top Archer, Mid Melee, Mid Mage, Mid Archer, Bot Melee, Bot Mage, Bot Archer
    public int[] unitIndex;

    public GameObject[] units;

    // Use this for initialization
    void Start ()
    {
        InitialiseUnitDamageArray();
        InitialiseUnitHPArray();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void InitialiseUnitDamageArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1Damage[i] = creep.damage;
            i++;
        }

        team1Damage[3] = team1Damage[0];
        team1Damage[4] = team1Damage[1];
        team1Damage[5] = team1Damage[2];
        team1Damage[6] = team1Damage[0];
        team1Damage[7] = team1Damage[1];
        team1Damage[8] = team1Damage[2];

        int z = 0;

        foreach (float x in team1Damage)
        {
            team2Damage[z] = x;
            z++;
        }

    }

    private void InitialiseUnitHPArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Health creep = unit.GetComponent<Health>();
            team1MaxHP[i] = creep.maxHealth;
            i++;
        }

        team1MaxHP[3] = team1MaxHP[0];
        team1MaxHP[4] = team1MaxHP[1];
        team1MaxHP[5] = team1MaxHP[2];
        team1MaxHP[6] = team1MaxHP[0];
        team1MaxHP[7] = team1MaxHP[1];
        team1MaxHP[8] = team1MaxHP[2];

        int z = 0;

        foreach (float x in team1MaxHP)
        {
            team2MaxHP[z] = x;
            z++;
        }

    }

    public float PassOutDamage(Unit unitType, Lane lane, Team team)
    {
        int i = 1;
        
        if (unitType == Unit.mage)
        {
            i = 2;
        }
        if (unitType == Unit.archer)
        {
            i = 3;
        }

        if (lane == Lane.mid)
        {
            i += 3;
        }
        if (lane == Lane.bottom)
        {
            i += 6;
        }


        if (team == Team.Team1)
        {
            return team1Damage[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2Damage[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Damage and failed miserably.");
    }

    public float PassOutHealth(Unit unitType, Lane lane, Team team)
    {
        int i = 1;

        if (unitType == Unit.mage)
        {
            i = 2;
        }
        if (unitType == Unit.archer)
        {
            i = 3;
        }

        if (lane == Lane.mid)
        {
            i += 3;
        }
        if (lane == Lane.bottom)
        {
            i += 6;
        }


        if (team == Team.Team1)
        {
            return team1MaxHP[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2MaxHP[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Health and failed miserably.");
    }
}
