using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour {

    public int cost;
    public int maxRank;
    public int currentRank;
    public int researchTime;
    public GameObject activeRank;
    private Text rankText;
    private bool gateIsResearching;
    private ResearchTree researchTree;
    private bool imActive;

   
    private float finishTime;

    // Use this for initialization
    void Start () {
        currentRank = 0;
        rankText = GetComponentInChildren<Text>();

        rankText.text = "0/5";

        researchTree = GetComponentInParent<ResearchTree>();
        gateIsResearching = researchTree.gateIsResearching;
    }
	
	// Update is called once per frame
	void Update () {
        gateIsResearching = researchTree.gateIsResearching;

        rankText.text = currentRank + "/" + maxRank;
        if (currentRank > 0 && currentRank < maxRank)
        {
            rankText.color = Color.yellow;
        }
        if (currentRank == maxRank)
        {
            rankText.color = Color.green;
        }

        if (imActive)
        {
            if (FindObjectOfType<GameManager>().time >= finishTime)
            {
                CompleteRankUp();
                researchTree.gateIsResearching = false;

            }
        }
    }

   public void RankUp ()
    {
        if (currentRank < maxRank && PlayerManager.essence >= cost && !gateIsResearching)
        {
            PlayerManager.essence -= cost;
            finishTime = FindObjectOfType<GameManager>().time + PlayerManager.researchTime;
            imActive =true;
            researchTree.gateIsResearching = true;
        }
    }

    public void CompleteRankUp()
    {
        currentRank++;
        imActive = false;
    }
}
