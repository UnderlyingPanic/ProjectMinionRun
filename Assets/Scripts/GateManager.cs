using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour {

    public Lane lane;

    public bool gateIsResearching;
    public float researchPercent;
    public float researchFinishTime;

	// Use this for initialization
	void Start () {
        gateIsResearching = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
