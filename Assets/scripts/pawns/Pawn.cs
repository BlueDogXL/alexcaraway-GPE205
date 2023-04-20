using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public Controller controller;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
    // wow a bunch of abstract functions we implement elsewhere
    public abstract void MoveForward();

    public abstract void MoveBackward();

    public abstract void TurnLeft();

    public abstract void TurnRight();

    public abstract void Shoot();

    public abstract void RotateTowards(Vector3 targetPosition);
}
