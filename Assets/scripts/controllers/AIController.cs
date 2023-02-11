using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost };
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;

    // Start is called before the first frame update
    public override void Start()
    {
        currentState = AIState.Idle;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MakeDecisions(); 
    }
    public void MakeDecisions()
    {
        Debug.Log("Making decisions!");
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;

        lastStateChangeTime = Time.time;
    }

    #region States
    public void DoSeekState()
    {
        Seek(target);
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
        
    }

    public void Seek(Pawn targetPawn)
    {

    }
    #endregion Behaviors
}
