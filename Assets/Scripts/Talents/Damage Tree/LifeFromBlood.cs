using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFromBlood : MonoBehaviour {

    public float[] modifiers = new float[3];
    public int index;
    public Unit unitToModify;

    private float modifier;
    private Lane lane;

    private void Start()
    {
        lane = GetComponentInParent<ResearchTree>().lane;
        index = 1;
    }

    void Update()
    {

    }

    public void OnRankUp()
    {
        CalculateIndex();
        
        modifier = modifiers[GetComponent<ResearchButton>().currentRank - 1];

        GameManager gameManager = FindObjectOfType<GameManager>();

        if (unitToModify != Unit.all)
        {
            gameManager.team1LifeSteal[index - 1] = modifier;
        }

        if (unitToModify == Unit.all)
        {
            for (int x = 0; x < 3; x++)
            {
                gameManager.team1LifeSteal[index - 1] = modifier;
                index++;
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
