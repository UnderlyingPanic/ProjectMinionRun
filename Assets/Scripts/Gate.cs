using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane { top, mid, bottom };
public enum Team { Team1, Team2 };
public enum Unit { swordsman, mage, archer};

public class Gate : MonoBehaviour {

    public Lane lane;
    public Team team;

    public GameObject swordsman, mage, archer, mountedMage, mountedSwordsman;
    public GameObject spawnPoint;

    private List<GameObject> spawnList = new List<GameObject>();
    public float spawnGap; //Gap between each minion in a wave
    public float waveGap; // Gap between each wave
    private float lastWave = 0f; // Time of Last Wave
    private float currentTime = 0f;
    private float nextWave;
    private float tapout = 10000000000000f; // used to get the NextWave stalled

    // Use this for initialization
    void Start () {
        CreateSpawnList(swordsman, swordsman, swordsman, mage, mage, mage, archer);

        StartCoroutine(SpawnWave());
        nextWave = tapout;
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = currentTime + Time.deltaTime;

        if (currentTime >= nextWave)
        {
            lastWave = currentTime;
            nextWave = tapout;
            StartCoroutine(SpawnWave());
            
        }
	}

    IEnumerator SpawnWave()
    {
        foreach (GameObject unit in spawnList)
        {
            float nextMinion = currentTime + spawnGap;
            SpawnUnit(unit);
            yield return new WaitUntil(() => currentTime >= nextMinion);
        }

        lastWave = currentTime;
        nextWave = currentTime + waveGap;
        yield return null;
    }

    private void CreateSpawnList(GameObject unit1, GameObject unit2, GameObject unit3, GameObject unit4, GameObject unit5, GameObject unit6, GameObject unit7)
    {
        spawnList.Add(unit1);
        spawnList.Add(unit2);
        spawnList.Add(unit3);
        spawnList.Add(unit4);
        spawnList.Add(unit5);
        spawnList.Add(unit6);
        spawnList.Add(unit7);
    }

    private void SpawnUnit(GameObject unit)
    {
        GameObject newUnit = Instantiate(unit, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        Creep creep = newUnit.GetComponent<Creep>();
        creep.AssignLane(lane);
    }
}
