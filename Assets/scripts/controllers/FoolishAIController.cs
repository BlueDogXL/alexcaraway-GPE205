using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoolishAIController : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        currentState = AIState.Idle;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeFoolishDecisions();
    }
    public void MakeFoolishDecisions() // just a minor difference from Normal Tank: does not flee when sufficiently damaged. like a fool
    {
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
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                    break;
                }
                // Do work
                DoSeekState();
                // Check for transitions
                if ((IsDistanceLessThan(target, 5)) && (CanSee(target) == true))
                {
                    ChangeState(AIState.Attack);
                }
                else if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                else if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                break;
            case AIState.Attack:
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                    break;
                }
                DoAttackState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                else if (!IsDistanceLessThan(target, 5))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.ChooseTarget:
                DoChooseTargetState();
                ChangeState(AIState.Idle);
                break;
        }
    }
}
