using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Creep : MonoBehaviour {

    public float moveSpeed;
    public float attackRange;
    public Unit type;
    public Team team;
    public Lane lane;
    public float attackSpeedMod = 1f;
    public float damage;
    public float armour;
    public float shield;
    public float lifeSteal;
    public List<GameObject> enemiesInSights = new List<GameObject>();
    public GameObject currentTarget;
    public GameObject tooltip;
    public float SecondWindBonus;
    public float atBonusAtSpawn;
    public float cumulativeATBonus;

    public int laneIndex;
    private bool attacking;
    private int arrayI;
    private float sightRange;
    private Animator animator;
    private GameObject currentWaypoint;
    private WaypointManager waypointManager;
    private GameObject[] pathToTake;
    private GameManager gameManager;
    private float calculatedDamage;
    public float captainBuffGained;
    public float captainBuffOnSpawn;
    public bool captain = false;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();

        waypointManager = FindObjectOfType<WaypointManager>();
        arrayI = 0;
        sightRange = GetComponent<BoxCollider>().size.x / 2;

        InitialiseCreepPathing();
        InitialiseCreepStats();

        captainBuffOnSpawn = gameManager.PassOutCaptain(team, laneIndex);
        atBonusAtSpawn = gameManager.PassOutAT(team, laneIndex);
    }
	
	// Update is called once per frame
	void Update ()
    {
        animator.speed = 1;

        if (currentTarget == null)
        {
            currentTarget = ChooseTarget();
        }

        var targetPoint = currentTarget.transform.position;
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f)); //clamps to only Y rotation

        animator.SetBool("isRunning", false);
        if (Vector3.Distance(this.transform.position, currentTarget.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position)) > attackRange && !attacking)
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


    private void FixedUpdate()
    {
        enemiesInSights.RemoveAll(item => item == null);
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

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
    {
        if (enemiesInSights.Contains(other.gameObject))
        {
            enemiesInSights.Remove(other.gameObject);
        }
    }

    private GameObject ChooseTarget ()
    {
               
        GameObject closestTarget = currentWaypoint;

        foreach (GameObject enemy in enemiesInSights)
        {
            if (enemy != null)
            {
                if (closestTarget == currentWaypoint)
                {
                    closestTarget = enemy;
                }
                if (Vector3.Distance(this.transform.position, enemy.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position)) < Vector3.Distance(this.transform.position, closestTarget.transform.position) &&
                    Vector3.Distance(this.transform.position, enemy.GetComponent<CapsuleCollider>().ClosestPointOnBounds(transform.position)) < sightRange)
                {
                    closestTarget = enemy;
                }
            }
        }

        return closestTarget;
    }

    public void AttackTarget(GameObject target)
    {

        if (GetComponent<ProjectileSpawner>())
        {
            GetComponent<ProjectileSpawner>().target = currentTarget;
        }

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

    //DEAL DAMAGE
    public void DealDamage()
    {
        CheckForCaptainAndSetBuff();
        CheckForAdvancedTacticsAndSetBuff();

        Health enemyHealth;
        calculatedDamage = CalculateDamage();

        if (currentTarget.GetComponent<Health>())
        {
            enemyHealth = currentTarget.GetComponent<Health>();

            //This creep sends damage to enemy creep for modification, and then deals that much damage to it.
            //Creep can call CalculateDamageTaken(calculateDamage) to see how much damage it might deal prior to dealing it.
            enemyHealth.TakeDamage(enemyHealth.CalculateDamageTaken(calculatedDamage));

            float lifeStealtoHeal = enemyHealth.CalculateDamageTaken(calculatedDamage) * lifeSteal;
            GetComponent<Health>().Heal(lifeStealtoHeal);
        } else {
            Debug.LogWarning(name + " has tried to deal damage to " + currentTarget + " but it doesn't have health");
            return;
        }
    }

    

    //CALCULATE DAMAGE
    public float CalculateDamage()
    {
        float missingHealth = GetComponent<Health>().ReturnMissingHealth();
        float secondWindCalulation = missingHealth * SecondWindBonus;

        //calculate damage to go send to enemy
        //Calculate SecondWind bonus
        float damageWithSecondWind = damage + secondWindCalulation;
        //Apply Captain
        float damageWithCaptain = (damageWithSecondWind * captainBuffGained) + damageWithSecondWind;
        //Apply Advanced Tactics
        float damageWithAT = (damageWithCaptain * cumulativeATBonus) + damageWithCaptain;

        float damageCalc = damageWithAT;

        //Debugging
        if (team == Team.Team1)
        {
            print("Name: " + name + "Missing Health: " + missingHealth + ", Damage I Deal: " + damageCalc + " including " + cumulativeATBonus * 100 + "% from Advanced Tactics");
        }

        return damageCalc;
    }

    private void InitialiseCreepStats()
    {
        InitialiseLaneIndex();

        damage = gameManager.PassOutDamage(type, lane, team);
        GetComponent<Health>().maxHealth = gameManager.PassOutHealth(type, lane, team);
        GetComponent<Health>().currHealth = gameManager.PassOutHealth(type, lane, team);
        moveSpeed = gameManager.PassOutMoveSpeed(type, lane, team);
        attackSpeedMod = gameManager.PassOutAtkSpd(type, lane, team);
        shield = gameManager.PassOutShields(type, lane, team);
        armour = gameManager.PassOutArmour(type, lane, team);
        lifeSteal = gameManager.PassOutLifeSteal(type, lane, team);
        SecondWindBonus = gameManager.PassOutSecondWind(team, laneIndex);
        atBonusAtSpawn = gameManager.PassOutAT(team, laneIndex);

        if (type == Unit.archer)
        {
            if (gameManager.PassOutCaptain(team, laneIndex) > 0)
            {
                captain = true;
            }
        }
    }

    private void CheckForCaptainAndSetBuff()
    {
        captainBuffGained = 0;

        if (gameManager.PassOutCaptain(team, laneIndex) > 0)
        {
            foreach (Archer archer in FindObjectsOfType<Archer>()) // Team, Distance, Captain
            {
                Creep archerCreep = archer.GetComponent<Creep>();
                if (archerCreep.team == this.team && Vector3.Distance(transform.position, archer.transform.position) <= gameManager.captainDistance && archerCreep.captain)
                {
                    captainBuffGained = archerCreep.captainBuffOnSpawn;
                }
            }
        }
    }

    private void CheckForAdvancedTacticsAndSetBuff()
    {
        int unitCount = 0;
        cumulativeATBonus = 0;

        if (atBonusAtSpawn == 0)
        {
            return;
        }

        foreach (Creep unit in FindObjectsOfType<Creep>())
        {
            if (unit.team == this.team && Vector3.Distance(transform.position, unit.transform.position) <= gameManager.ATDistance)
            {
                unitCount++;
            }
        }
        cumulativeATBonus = atBonusAtSpawn * unitCount;
    }

    public void SetTooltipTarget()
    {
        if (!FindObjectOfType<UnitTooltip>())
        {
            GameObject newToolTip = Instantiate(tooltip, GameObject.Find("General UI").transform);
            newToolTip.GetComponent<UnitTooltip>().selectedObject = this.gameObject;
        }
        else
        {
            FindObjectOfType<UnitTooltip>().selectedObject = this.gameObject;
        }
    }

    private void InitialiseLaneIndex()
    {
        if (lane == Lane.mid)
        {
            laneIndex = 1;
        }

        if (lane == Lane.top)
        {
            laneIndex = 0;
        }

        if (lane == Lane.bottom)
        {
            laneIndex = 2;
        }
    }
}