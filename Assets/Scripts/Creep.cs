using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour {

    public float moveSpeed;
    public float attackRange;
    public GameObject targetGate;
    public bool ownedByThisPlayer;
    

    private bool attacking;

    private float sightRange;
    private List<GameObject> enemiesInSights = new List<GameObject>();
    private GameObject currentTarget;
    private Animator animator;


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        currentTarget = targetGate;
        sightRange = GetComponent<BoxCollider>().size.x / 2;

        print(sightRange);
	}
	
	// Update is called once per frame
	void Update ()
    {

        print(attacking);

        animator.SetBool("isRunning", false);
        if (Vector3.Distance(this.transform.position, currentTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position)) > attackRange && !attacking)
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
       
        Debug.Log(name + "- " + currentTarget);

        //Do this RIGHT AT THE END
        enemiesInSights.Clear();
    }

    private void MoveToCurrentTarget()
    {
        // Smoothly Rotate to Target
        var targetPoint = currentTarget.transform.position;
        var targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);

        //Move Forward (duh)
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

        if (moveSpeed > 0)
        {
            animator.SetBool("isRunning", true);
        } else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Create a list of attackable units inside the collider
        //We need this list so we can "foreach" them.
        //Currently we need to "foreach" them to see which is closest
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
        animator.SetTrigger("triggerAttackA");
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
}
