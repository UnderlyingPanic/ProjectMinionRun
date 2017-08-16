using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResearchTimer : MonoBehaviour {

    public GateManager myGateManager;

    private Text timerText;

	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (myGateManager.gateIsResearching == false)
        {
            timerText.color = Color.yellow;
            timerText.text = "Idle";
        }
        if (myGateManager.gateIsResearching)
        {
            timerText.color = Color.blue;
            timerText.text = Mathf.RoundToInt(myGateManager.researchFinishTime - FindObjectOfType<GameManager>().time).ToString();
        }
        if (myGateManager.researchFinishTime - FindObjectOfType<GameManager>().time <= 0.5 && myGateManager.gateIsResearching == true)
        {
            timerText.color = Color.green;
            timerText.text = "Ready";
        }
	}
}
