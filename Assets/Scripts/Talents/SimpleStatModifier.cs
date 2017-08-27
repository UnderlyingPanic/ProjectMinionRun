using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleStatModifier : MonoBehaviour {

    public Unit unitToModify;
    public Stat stat;
    public float modifier;

    private GameManager gameManager;
    private Lane lane;
    private int index;


    // Use this for initialization
    void Start () {
        gameManager = FindObjectOfType<GameManager>();
        lane = GetComponentInParent<ResearchTree>().lane;
        CalculateIndex();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRankUp ()
    {
        CalculateIndex();
        if (unitToModify != Unit.all)
        {
           
            switch (stat)
            {
                case Stat.damage:
                    gameManager.team1Damage[index - 1] *= modifier;
                    break;
                case Stat.hp:
                    gameManager.team1MaxHP[index - 1] *= modifier;
                    break;
                case Stat.attackSpeed:
                    gameManager.team1AttackSpeedMod[index-1] *= modifier;
                    break;
                case Stat.moveSpeed:
                    gameManager.team1MoveSpeed[index - 1] *= modifier;
                    break;
                case Stat.armour:
                    gameManager.team1Armour[index - 1] *= modifier;
                    break;
                case Stat.shield:
                    gameManager.team1Shield[index - 1] *= modifier;
                    break;
                default:
                    Debug.Log("modding Stat messed up");
                    break;
            }
        }

        if (unitToModify == Unit.all)
        {
            
            switch (stat)
            {
                case Stat.damage:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1Damage[index - 1] *= modifier;
                        index++;
                    }
                    break;
                case Stat.hp:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1MaxHP[index - 1] *= modifier;
                        index++;
                    }
                    break;
                case Stat.attackSpeed:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1AttackSpeedMod[index - 1] *= modifier;
                        index++;
                    }
                    break;
                case Stat.moveSpeed:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1MoveSpeed[index - 1] *= modifier;
                        index++;
                    }
                    break;
                case Stat.armour:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1Armour[index - 1] *= modifier;
                        index++;
                    }
                    break;
                case Stat.shield:
                    for (int x = 0; x < 3; x++)
                    {
                        gameManager.team1Shield[index - 1] *= modifier;
                        index++;
                    }
                    break;
                default:
                    Debug.Log("modding Stat messed up");
                    break;
            }
        }
    }

    private void CalculateIndex()
    {
        index = 1;
        if (unitToModify == Unit.mage)
        {
            index = 2;
        }
        if (unitToModify == Unit.archer)
        {
            index = 3;
        }

        if (lane == Lane.mid)
        {
            index += 3;
        }
        if (lane == Lane.bottom)
        {
            index += 6;
        }
    }
}
