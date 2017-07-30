using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { top, mid, bottom };
public enum Team { Team1, Team2 };

public class Gate : MonoBehaviour {

    public Lane lane;
    public Team team;

    public GameObject swordsman, mage, archer, mountedMage, mountedSwordsman;
    public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        GameObject newUnit = Instantiate(swordsman, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        Creep creep = newUnit.GetComponent<Creep>();
        creep.AssignLane(lane);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
