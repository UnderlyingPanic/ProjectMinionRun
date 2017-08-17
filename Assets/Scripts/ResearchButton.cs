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
    public int teir;
    public ResearchButton[] requiredTalents;
    public int numberRequiredFromPreviousTeir;

    private bool haveRequiredTalents;
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
                gateManager.gateIsResearching = false;
                CompleteRankUp();
            }
        }
    }

   public void RankUp ()
    {
        if (currentRank < maxRank && PlayerManager.essence >= cost && !gateIsResearching)
        {
            if (CheckPrerequisites() == false) {
                return;
            }

            PlayerManager.essence -= cost;
            finishTime = FindObjectOfType<GameManager>().time + PlayerManager.researchTime;
            gateManager.researchFinishTime = finishTime;
            imActive = true;
            GetComponent<RawImage>().color = Color.yellow;
            gateManager.gateIsResearching = true;
        }
    }

    public void CompleteRankUp()
    {
        currentRank++;
        imActive = false;
        GetComponent<RawImage>().color = Color.white;
    }

    public bool CheckPrerequisites()
    {
        //First check Required Talents (if any)
        if (requiredTalents != null)
        {
            foreach (ResearchButton talent in requiredTalents)
            {
                if (talent.currentRank < talent.maxRank)
                {
                    Debug.Log("You do not have the required talents to research this yet");
                    return false;
                }
            }
        }
        //Next, check to see if we have enough points from the previous teir

        int runningTotal = 0;
        foreach (ResearchButton talent in FindObjectsOfType<ResearchButton>())
        {
            if (talent.teir == this.teir-1)
            {
                runningTotal += talent.currentRank;
            }
        }

        if (runningTotal < numberRequiredFromPreviousTeir)
        {
            Debug.Log("We have " + runningTotal + ", but we need " + numberRequiredFromPreviousTeir);
            return false;
        }
        
        // If we passed all the checks, it must be fine to rank up!
        return true;
    }
}
