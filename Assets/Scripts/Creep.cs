using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour {

    public float moveSpeed;
    public Transform targetGate;
    public bool ownedByThisPlayer;

    
    private List<GameObject> enemiesInSights = new List<GameObject>();
    private Transform currentTarget;

    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentTarget = targetGate;
	}
	
	// Update is called once per frame
	void Update ()
    {

        
        

        animator.SetBool("isRunning", false);
        MoveToCurrentTarget();
    }

    private void FixedUpdate()
    {

       

        Debug.Log(name + "- Current List of Enemies in Sight: " + enemiesInSights.Count);

        //Do these two RIGHT AT THE END
        enemiesInSights.Clear();
    }

    private void MoveToCurrentTarget()
    {
        transform.LookAt(currentTarget);
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if (moveSpeed > 0)
        {
            animator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.GetComponent<Pylon>())
        {
            if (other.GetComponent<Pylon>().ownedByThisPlayer != this.ownedByThisPlayer)
            {
                if (enemiesInSights.Contains(other.gameObject))
                {
                    return;
                }
                else
                {
                    enemiesInSights.Add(other.gameObject);
                }
            }
        }
        if (other.GetComponent<Creep>())
        {
            if (other.GetComponent<Creep>().ownedByThisPlayer != this.ownedByThisPlayer)
            {

                if (enemiesInSights.Contains(other.gameObject))
                {
                    return;
                } else
                {
                    enemiesInSights.Add(other.gameObject);
                }
                
            }
        }

    }

    private Transform ChooseTarget ()
    {
        Transform closestTarget = currentTarget;

        foreach (GameObject enemy in enemiesInSights)
        {
            if (Vector3.Distance (this.transform.position, enemy.transform.position) < Vector3.Distance (this.transform.position, closestTarget.transform.position))
            {
                closestTarget = targetGate;
            }
        }

        return closestTarget;
    }
}
