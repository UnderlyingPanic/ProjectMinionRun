using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { top, mid, bottom, all };
public enum Team { Team1, Team2 };
public enum Unit { swordsman, mage, archer, all };
public enum Stat { damage, hp, attackSpeed, moveSpeed, armour, shield }

public class GameManager : MonoBehaviour {

    public float time;
    public float captainDistance;
    public float ATDistance;

    public float[] team1Damage;
    public float[] team2Damage;

    public float[] team1MaxHP;
    public float[] team2MaxHP;

    public float[] team1AttackSpeedMod;
    public float[] team2AttackSpeedMod;

    public float[] team1MoveSpeed;
    public float[] team2MoveSpeed;

    public float[] team1LifeSteal;
    public float[] team2LifeSteal;

    public float[] team1Armour;
    public float[] team2Armour;

    public float[] team1Shield;
    public float[] team2Shield;

    public float[] team1SecondWind = new float[3];
    public float[] team2SecondWind = new float[3];

    public float[] team1Captain = new float[3];
    public float[] team2Captain = new float[3];

    public float[] team1AT = new float[3];
    public float[] team2AT = new float[3];

    // Top Mid Bot
    //Top Melee, Top Mage, Top Archer, Mid Melee, Mid Mage, Mid Archer, Bot Melee, Bot Mage, Bot Archer
    public int[] unitIndex;

    public GameObject[] units;

    public GameObject selectedObject;

    // Use this for initialization
    void Start ()
    {
        InitialiseUnitDamageArray();
        InitialiseUnitHPArray();
        InitialiseAttackSpeedArray();
        InitialiseMoveSpeedArray();
        InitialiseShieldArray();
        InitialiseArmourArray();
        InitialiseLifeStealArray();
        InitialiseSecondWind();
        InitialiseCaptain();
    }
	
	// Update is called once per frame
	void Update ()
    {
        time = Time.timeSinceLevelLoad;
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

    private void InitialiseAttackSpeedArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1AttackSpeedMod[i] = creep.attackSpeedMod;
            i++;
        }

        team1AttackSpeedMod[3] = team1AttackSpeedMod[0];
        team1AttackSpeedMod[4] = team1AttackSpeedMod[1];
        team1AttackSpeedMod[5] = team1AttackSpeedMod[2];
        team1AttackSpeedMod[6] = team1AttackSpeedMod[0];
        team1AttackSpeedMod[7] = team1AttackSpeedMod[1];
        team1AttackSpeedMod[8] = team1AttackSpeedMod[2];

        int z = 0;

        foreach (float x in team1AttackSpeedMod)
        {
            team2AttackSpeedMod[z] = x;
            z++;
        }
    }

    private void InitialiseMoveSpeedArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1MoveSpeed[i] = creep.moveSpeed;
            i++;
        }

        team1MoveSpeed[3] = team1MoveSpeed[0];
        team1MoveSpeed[4] = team1MoveSpeed[1];
        team1MoveSpeed[5] = team1MoveSpeed[2];
        team1MoveSpeed[6] = team1MoveSpeed[0];
        team1MoveSpeed[7] = team1MoveSpeed[1];
        team1MoveSpeed[8] = team1MoveSpeed[2];

        int z = 0;

        foreach (float x in team1MoveSpeed)
        {
            team2MoveSpeed[z] = x;
            z++;
        }
    }

    private void InitialiseArmourArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1Armour[i] = creep.armour;
            i++;
        }

        team1Armour[3] = team1Armour[0];
        team1Armour[4] = team1Armour[1];
        team1Armour[5] = team1Armour[2];
        team1Armour[6] = team1Armour[0];
        team1Armour[7] = team1Armour[1];
        team1Armour[8] = team1Armour[2];

        int z = 0;

        foreach (float x in team1Armour)
        {
            team2Armour[z] = x;
            z++;
        }
    }

    private void InitialiseShieldArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1Shield[i] = creep.shield;
            i++;
        }

        team1Shield[3] = team1Shield[0];
        team1Shield[4] = team1Shield[1];
        team1Shield[5] = team1Shield[2];
        team1Shield[6] = team1Shield[0];
        team1Shield[7] = team1Shield[1];
        team1Shield[8] = team1Shield[2];

        int z = 0;

        foreach (float x in team1Shield)
        {
            team2Shield[z] = x;
            z++;
        }
    }

    private void InitialiseLifeStealArray()
    {
        int i = 0;

        foreach (GameObject unit in units)
        {
            Creep creep = unit.GetComponent<Creep>();
            team1LifeSteal[i] = creep.lifeSteal;
            i++;
        }

        team1LifeSteal[3] = team1LifeSteal[0];
        team1LifeSteal[4] = team1LifeSteal[1];
        team1LifeSteal[5] = team1LifeSteal[2];
        team1LifeSteal[6] = team1LifeSteal[0];
        team1LifeSteal[7] = team1LifeSteal[1];
        team1LifeSteal[8] = team1LifeSteal[2];

        int z = 0;

        foreach (float x in team1LifeSteal)
        {
            team2LifeSteal[z] = x;
            z++;
        }
    }

    private void InitialiseSecondWind()
    {
        team1SecondWind[0] = 0;
        team1SecondWind[1] = 0;
        team1SecondWind[2] = 0;
        team2SecondWind[0] = 0;
        team2SecondWind[1] = 0;
        team2SecondWind[2] = 0;
    }

    private void InitialiseCaptain()
    {
        team1Captain[0] = 0;
        team1Captain[1] = 0;
        team1Captain[2] = 0;
        team2Captain[0] = 0;
        team2Captain[1] = 0;
        team2Captain[2] = 0;
    }

    private void InitialiseAT()
    {
        team1AT[0] = 0;
        team1AT[1] = 0;
        team1AT[2] = 0;
        team2AT[0] = 0;
        team2AT[1] = 0;
        team2AT[2] = 0;
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

    public float PassOutAtkSpd(Unit unitType, Lane lane, Team team)
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
            return team1AttackSpeedMod[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2AttackSpeedMod[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Damage and failed miserably.");
    }

    public float PassOutMoveSpeed(Unit unitType, Lane lane, Team team)
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
            return team1MoveSpeed[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2MoveSpeed[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Damage and failed miserably.");
    }

    public float PassOutShields(Unit unitType, Lane lane, Team team)
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
            return team1Shield[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2Shield[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Shields and failed miserably.");
    }

    public float PassOutArmour(Unit unitType, Lane lane, Team team)
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
            return team1Armour[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2Armour[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Shields and failed miserably.");
    }

    public float PassOutLifeSteal(Unit unitType, Lane lane, Team team)
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
            return team1LifeSteal[i - 1];
        }
        if (team == Team.Team2)
        {
            return team2LifeSteal[i - 1];
        }

        throw new UnityException("Game Manager tried to pass out Shields and failed miserably.");
    }

    public float PassOutSecondWind(Team team, int index)
    {
        if (team == Team.Team1)
        {
            return team1SecondWind[index];
        }
        if (team == Team.Team2)
        {
            return team2SecondWind[index];
        }

        throw new UnityException("Game Manager tried to pass out SecondWindBonus and failed miserably.");
    }

    public float PassOutCaptain(Team team, int index)
    {
        if (team == Team.Team1)
        {
            return team1Captain[index];
        }
        if (team == Team.Team2)
        {
            return team2Captain[index];
        }

        throw new UnityException("Game Manager tried to pass out SecondWindBonus and failed miserably.");
    }

    public float PassOutAT(Team team, int index)
    {
        if (team == Team.Team1)
        {
            return team1AT[index];
        }
        if (team == Team.Team2)
        {
            return team2AT[index];
        }

        throw new UnityException("Game Manager tried to pass out AdvancedTactics Bonus and failed miserably.");
    }

    public void SetSelectedObject (GameObject obj)
    {
        selectedObject = obj;
    }
}
