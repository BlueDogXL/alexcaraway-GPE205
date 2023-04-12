using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardlyAIController : AIController
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
        MakeCowardlyDecisions();
    }
    public void MakeCowardlyDecisions()
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
                // Do work
                DoSeekState();
                // Check for transitions
                if ((IsDistanceLessThan(target, 10)) && (CanSee(target) == true))
                {
                    ChangeState(AIState.Attack);
                }
                else if (!IsDistanceLessThan(target, 20))
                {
                    ChangeState(AIState.Idle);
                }
                else if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIState.ChooseTarget);
                }
                else if (health.currentHealth <= (health.maxHealth / 2))
                {
                    ChangeState(AIState.Flee);
                }
                else if (!IsDistanceLessThan(target, 10))
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
}
