using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extraction : MonoBehaviour {

    public float essMod;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnRankUp()
    {
        PlayerManager.essenceModifier += essMod;
    }
}
