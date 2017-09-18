using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Captain : MonoBehaviour {

    public float[] pointsPerRank = new float[5];

    public void OnRankUp()
    {
        int i = GetComponent<ResearchButton>().currentRank - 1;

        Lane lane = GetComponentInParent<ResearchTree>().lane;
        int j = 100;

        if (lane == Lane.mid)
        {
            j = 1;
        }

        if (lane == Lane.top)
        {
            j = 0;
        }

        if (lane == Lane.bottom)
        {
            j = 2;
        }

        FindObjectOfType<GameManager>().team1Captain[j] = pointsPerRank[i];
    }
}
