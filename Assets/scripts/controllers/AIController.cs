using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Guard, Chase, Flee, Patrol, Attack, BackToPost, ChooseTarget};
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;
    private Vector3 vectorToTarget;
    public float fleeDistance;
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public bool isLooping;
    public Health health;
    public float hearingDistance;
    public float fieldOfView;
    public float maxViewDistance;


    // Start is called before the first frame update
    public override void Start()
    {
        currentState = AIState.Idle;
        health = GetComponent<Health>();
        base.Start();
    }
     
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MakeDecisions();
        if (target != null)
        {
            vectorToTarget = target.transform.position - transform.position;
        }
        
    }
    public void MakeDecisions()
    {
        Debug.Log("Making decisions!");
        switch (currentState)
        {
            case AIState.Idle:
                // Do work 
                DoIdleState();
                // Check for transitions
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                else if (CanHear(target))
                {
                    ChangeState(AIState.Chase);
                }
                
                    break;
            case AIState.Chase:
                // Do work
                DoSeekState();
                // Check for transitions
                if ((IsDistanceLessThan(target, 5)) && (CanSee(target) == true))
                {
                    ChangeState(AIState.Attack);
                }
                else if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                else if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                else if (health.currentHealth <= (health.maxHealth / 4))
                {
                    ChangeState(AIState.Flee);
                }
                else if (!IsDistanceLessThan(target, 5))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                break;
            case AIState.ChooseTarget:
                DoChooseTargetState();
                ChangeState(AIState.Idle);
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;

        lastStateChangeTime = Time.time;
    }

    #region States
    public virtual void DoSeekState()
    {
        Seek(target);
    }

    public virtual void DoAttackState()
    {
        Seek(target);
        pawn.Shoot();
    }

    public virtual void DoFleeState()
    {
        Flee();
    }

    public virtual void DoPatrolState()
    {
        Patrol();
    }
    public virtual void DoChooseTargetState()
    {
        TargetPlayerOne();
    }

    public virtual void DoIdleState()
    {
        // *wind whistles by, tumbleweed rolls past*
    }
    #endregion States

    #region Behaviors
    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);
        pawn.MoveForward();
    }

    public void Seek(Transform targetTransform)
    {
        pawn.RotateTowards(targetTransform.position);
        pawn.MoveForward();
    }

    public void Seek(Pawn targetPawn)
    {
        pawn.RotateTowards(targetPawn.transform.position);
        pawn.MoveForward();
    }

    public void Seek(Vector3 targetVector)
    {
        pawn.RotateTowards(targetVector);
        pawn.MoveForward();
    }

    public bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Flee()
    {
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position); // distance between us and target
        float percentOfFleeDistance = targetDistance / fleeDistance; // what percent of the flee distance are we from target
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance); // make it usable for multiplication
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance; // make it actually work how we want it to
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the Vector away from our target by multiplying by -1
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * (fleeDistance * flippedPercentOfFleeDistance);
        // now get the heck out of dodge
        Seek(pawn.transform.position + fleeVector);
    }
    protected void Patrol()
    {
        // If we have a enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            // If we are close enough, then increment to next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else if (isLooping == true)
        {
            RestartPatrol();
        }
    }
    protected void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }
    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }
    protected bool IsHasTarget()
    {
        // return true if we have a target, false if we don't
        return (target != null);
    }
    public bool CanHear(GameObject target)
    {
        // Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        // If they don't have one, they can't make noise, so return false
        if (noiseMaker == null)
        {
            return false;
        }
        // If they are making 0 noise, they also can't be heard
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // ... then we can hear the target
            return true;
        }
        else
        {
            // Otherwise, we are too far away to hear them
            return false;
        }
    }
    public bool CanSee(GameObject target)
    {
        // Find the vector from the agent to the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);
        // if that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            Vector3 heightCompensation = new Vector3(0, transform.localScale.y * .5f, 0);
            Ray lineOfSightRay = new Ray(transform.position + heightCompensation, agentToTargetVector); // stole this from last summer semester. basically this is our ray
            RaycastHit hitInfo; // and whatever it hits
            if (Physics.Raycast(lineOfSightRay, out hitInfo, maxViewDistance)) // so we send it out and
            {
                Debug.DrawRay(transform.position + heightCompensation, agentToTargetVector * hitInfo.distance, Color.yellow);
                //if thing hit is our target, return true
                if (hitInfo.collider.gameObject == target)
                {
                    Debug.Log(name + " can see its target!");
                    return true;
                }
                else
                {
                    Debug.Log(name + " sees this: " + hitInfo.collider.gameObject.name);
                    return false;
                }
            }
            else
            {
                Debug.Log(name + "can't see anything?");
                return false;
            }
        }
        else // and if not return false
        {
            return false;
        }
    }
    #endregion Behaviors
}
