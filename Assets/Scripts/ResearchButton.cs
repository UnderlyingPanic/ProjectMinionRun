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

    private Lane lane;
    private Text rankText;
    private bool gateIsResearching;
    private bool imActive;
    private GateManager gateManager;
   
    private float finishTime;

    // Use this for initialization
    void Start () {
        currentRank = 0;
        rankText = GetComponentInChildren<Text>();

        rankText.text = "0/5";

        lane = GetComponentInParent<ResearchTree>().lane;

        foreach (GateManager gm in GameObject.FindObjectsOfType<GateManager>())
        {
            if (gm.lane == this.lane)
            {
                gateManager = gm;
            }
        }
        
        gateIsResearching = gateManager.gateIsResearching;
    }
	
	// Update is called once per frame
	void Update () {
        gateIsResearching = gateManager.gateIsResearching;

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
                gateManager.gateIsResearching = false;

            }
        }
    }

   public void RankUp ()
    {
        if (currentRank < maxRank && PlayerManager.essence >= cost && !gateIsResearching)
        {
            PlayerManager.essence -= cost;
            finishTime = FindObjectOfType<GameManager>().time + PlayerManager.researchTime;
            gateManager.researchFinishTime = finishTime;
            imActive = true;
            gateManager.gateIsResearching = true;
        }
    }

    public void CompleteRankUp()
    {
        currentRank++;
        imActive = false;
    }
}
