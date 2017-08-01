using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public Lane lane;
    [Tooltip ("Which Team's Base is this leading to?")] public Team team;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmos()
    {
        if (team == Team.Team1)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, this.transform.position.y + 3f, this.transform.position.z), new Vector3(5, 5, 5));
        }
        if (team == Team.Team2)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(new Vector3(transform.position.x, this.transform.position.y + 3f, this.transform.position.z), new Vector3(5, 5, 5));
        }
    }
}
