using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAIController : AIController
{
    
    // Start is called before the first frame update
    new void Start()
    {
        ChangeState(AIState.ChooseTarget);
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeRandomDecisions();
    }

    public void MakeRandomDecisions()
    {
        switch (currentState)
        {
            case AIState.ChooseTarget:
                DoChooseTargetState();
                if (CanHear(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                DoSeekState();
                ChangeState(AIState.Attack);
                break;
            case AIState.Attack:
                DoAttackState();
                ChangeState(AIState.Chase);
                break;
        }
    }

    public override void DoSeekState()
    {
        
        int randomNumber = Random.Range(0, 10);
        if (randomNumber == 0)
        {
            pawn.TurnLeft();
        }
        else if (randomNumber == 1)
        {
            pawn.TurnRight();
        }
        else
        {
            pawn.MoveForward();
        }
    }
    public override void DoAttackState()
    {
        pawn.Shoot();
    }

}
