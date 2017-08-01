using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {

    public GameObject[] Team1TopWaypoints;
    public GameObject[] Team2TopWaypoints;
    public GameObject[] Team1BotWaypoints;
    public GameObject[] Team2BotWaypoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] PassOutWaypointArray (Lane lane, Team team)
    {
       
        if (lane == Lane.top)
        {
            if (team == Team.Team1)
            {
                return Team1TopWaypoints;
            }
            if (team == Team.Team2)
            {
                return Team2TopWaypoints;
            }
        }

        if (lane == Lane.bottom)
        {
            if (team == Team.Team1)
            {
                return Team1BotWaypoints;
            }
            if (team == Team.Team2)
            {
                return Team2BotWaypoints;
            }
        }

        throw new UnityException("Waypoint Manager tried to pass out a Waypoint Array but couldn't.");
    }
}
