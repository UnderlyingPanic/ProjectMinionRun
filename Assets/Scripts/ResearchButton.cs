using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour {

    public int cost;
    public int maxRank;
    public int currentRank;
    private Text rankText;

    // Use this for initialization
    void Start () {
        currentRank = 0;
        rankText = GetComponentInChildren<Text>();

        rankText.text = "0/5";
    }
	
	// Update is called once per frame
	void Update () {
        rankText.text = currentRank + "/" + maxRank;
        if (currentRank > 0 && currentRank < maxRank)
        {
            rankText.color = Color.yellow;
        }
        if (currentRank == maxRank)
        {
            rankText.color = Color.green;
        }
	}

   public void RankUp ()
    {
        if (currentRank < maxRank && PlayerManager.essence >= cost)
        {
            currentRank++;
            PlayerManager.essence -= cost;
        }
    }
}
