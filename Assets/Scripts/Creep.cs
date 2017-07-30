using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour {

    public float moveSpeed;
    public float attackRange;
    public Team team;
    public Lane lane;
    public float attackSpeedMod = 1f;

    private bool attacking;

    private int arrayI;
    private float sightRange;
    private List<GameObject> enemiesInSights = new List<GameObject>();
    public GameObject currentTarget;
    private Animator animator;
    private GameObject currentWaypoint;
    private WaypointManager waypointManager;
    private GameObject[] pathToTake;
   

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        waypointManager = FindObjectOfType<WaypointManager>();
        arrayI = 0;
        sightRange = GetComponent<BoxCollider>().size.x / 2;

        InitialiseCreepPathing();
    }
	
	// Update is called once per frame
	void Update ()
    {
        animator.speed = 1;

        var targetPoint = currentTarget.transform.position;
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f)); //clamps to only Y rotation

        if (currentTarget == null)
        {
            currentTarget = currentWaypoint;
        }

        animator.SetBool("isRunning", false);
        if (Vector3.Distance(this.transform.position, currentTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position)) > attackRange && !attacking)
        {
            MoveToCurrentTarget();
        } else if (currentTarget == currentWaypoint)
        {
          MoveToCurrentTarget();
        } else
        {
            animator.SetBool("isRunning", false);
            AttackTarget(currentTarget);
        }
    }


    //Target Detection has to go in Fixed Update, because OnCollisionStay syncs with it.
    private void FixedUpdate()
    {
            
        //Do this RIGHT AT THE END
        enemiesInSights.Clear();
    }

    private void MoveToCurrentTarget()
    {
        // Smoothly Rotate to Target
       

        float distanceToCurrentWaypoint = Vector3.Distance(this.transform.position, currentWaypoint.transform.position);
        if (distanceToCurrentWaypoint < 5f) //If we are within 5m of the current waypoint
        {
            if (arrayI <= pathToTake.Length) //Check we there's another waypoint left
            {
                arrayI++; //Add one to the Array Index
                currentWaypoint = pathToTake[arrayI]; //Get the waypoint in the Waypoint Index
                currentTarget = currentWaypoint; //Set the current Target to the current Waypoint
            }
            if (arrayI == pathToTake.Length) // ERROR CHECK: MINION HITS END OF PATH
            {
                Debug.Log("A minion has reached the last waypoint!");
            }
        }

        //Move Forward (duh)
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if (moveSpeed > 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Create a list of attackable units inside the collider
        //We need this list so we can "foreach" them.
        //Currently we need to "foreach" them to see which is closest
        if (other.GetComponent<Pylon>())
        {
            if (other.GetComponent<Pylon>().team != this.team)
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
            if (other.GetComponent<Creep>().team != this.team)
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

        currentTarget = ChooseTarget();

        
    }

    private GameObject ChooseTarget ()
    {
        GameObject closestTarget = currentTarget;

        foreach (GameObject enemy in enemiesInSights)
        {
            if (Vector3.Distance(this.transform.position, enemy.GetComponent<Collider>().ClosestPointOnBounds(transform.position)) < Vector3.Distance(this.transform.position, closestTarget.transform.position) &&
                Vector3.Distance(this.transform.position, enemy.GetComponent<Collider>().ClosestPointOnBounds(transform.position)) < sightRange)
            {
                closestTarget = enemy;
            }
        }

        return closestTarget;
    }

    public void AttackTarget(GameObject target)
    {

        animator.speed = attackSpeedMod;

        int randomNum = Random.Range(0, 2);
        
        if (randomNum == 0)
        {
            animator.SetTrigger("triggerAttackA");
        }
        if (randomNum == 1)
        {
            animator.SetTrigger("triggerAttackB"); // THIS NEVER TRIGGERS FOR SOME REASON. MUST FIGURE OUT WHY.
        }
    }

    //These are needed so the animator can set the variable. The variable stops us sliding while attacking.
    public void SetAttackingTrue ()
    {
        attacking = true;
    }
    public void SetAttackingFalse()
    {
        attacking = false;
    }

    public void AssignLane (Lane laneToAssign)
    {
        lane = laneToAssign;
    }

    private void InitialiseCreepPathing () // Set the first Waypoint based on Lane and set the Sight Range
    {
        Waypoint[] waypoints = GameObject.FindObjectsOfType<Waypoint>();

        if (lane == Lane.mid)
        {
            foreach (Waypoint waypoint in waypoints)
            {
                if (waypoint.lane == Lane.mid && waypoint.team != team)
                {
                    currentTarget = waypoint.gameObject;
                    currentWaypoint = waypoint.gameObject;
                }
            }
        }

        if (lane == Lane.top || lane == Lane.bottom)
        {
            pathToTake = waypointManager.PassOutWaypointArray(lane, team);

            currentTarget = pathToTake[arrayI];
            currentWaypoint = currentTarget;
        }
    }
}
