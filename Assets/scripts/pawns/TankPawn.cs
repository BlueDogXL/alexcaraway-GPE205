using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public Shooter shooter;
    public Mover mover;
    public GameObject shellPrefab;
    public NoiseMaker noiseMaker;
    public float shootForce;
    public float damageDone;
    public float shellLifespan;
    public float fireRate;
    public bool canIShoot;
    private float fireRateTimer; // hey designers take all my variables for use in an inspector near you
     
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shooter = GetComponent<Shooter>();
        mover = GetComponent<Mover>(); // get our shooter and mover n stuff
        noiseMaker = GetComponent<NoiseMaker>();
        canIShoot = true;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Time.time >= fireRateTimer)
        {
            canIShoot = true;
        }
        if (noiseMaker != null && noiseMaker.volumeDistance > 0)
        {
            noiseMaker.volumeDistance = noiseMaker.volumeDistance - .05f;
        }
        
    }
    // schmoovement functions
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 5;
        }
        
        Debug.Log("Forward!");
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 5;
        }
        Debug.Log("Backward!");
    }

    public override void TurnLeft()
    {
        mover.Rotate(-turnSpeed);
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 5;
        }
        Debug.Log("Leftward!");
    }

    public override void TurnRight()
    {
        mover.Rotate(turnSpeed);
        if (noiseMaker != null)
        {
            noiseMaker.volumeDistance = 5;
        }
        Debug.Log("Rightward!");
    }
    // this one here is really only useful for AI 
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot() // tank moment
    {
        if (canIShoot == true)
        {
            shooter.Shoot(shellPrefab, shootForce, damageDone, shellLifespan);
            Debug.Log("Shootward!");
            canIShoot = false;
            fireRateTimer = Time.time + fireRate;
            if (noiseMaker != null)
            {
                noiseMaker.volumeDistance = 15;
            }
        }
        else
        {
            Debug.Log("I don't have a shell ready.");
        }
    }
}