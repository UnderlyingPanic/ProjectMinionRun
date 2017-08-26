using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeWarp : MonoBehaviour {

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
        for (int x = 0; x < 3; x++)
        {
            gameManager.team1AttackSpeedMod[index - 1] = 2;
            index++;
        }
    }

    private void CalculateIndex()
    {
        index = 1;
        
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
