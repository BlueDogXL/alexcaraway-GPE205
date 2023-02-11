using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public Shooter shooter;
    public Mover mover;
    public GameObject shellPrefab;
    public float shootForce;
    public float damageDone;
    public float shellLifespan;
    public float fireRate; // hey designers take all my variables for use in an inspector near you
     
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shooter = GetComponent<Shooter>();
        mover = GetComponent<Mover>(); // get our shooter and mover n stuff
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    // schmoovement functions
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
        Debug.Log("Forward!");
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
        Debug.Log("Backward!");
    }

    public override void TurnLeft()
    {
        mover.Rotate(-turnSpeed);
        Debug.Log("Leftward!");
    }

    public override void TurnRight()
    {
        mover.Rotate(turnSpeed);
        Debug.Log("Rightward!");
    }
    // this one down here is really only useful for AI and it's only appearing in project 1 because i'm a dirty procrastinator
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot() // tank moment
    {
        shooter.Shoot(shellPrefab, shootForce, damageDone, shellLifespan);
        Debug.Log("Shootward!");
    }
}