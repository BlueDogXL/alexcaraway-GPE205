using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SpeedPowerup : Powerup
{
    public float speedToAdd;
    public TankPawn targetTank;
    public override void Apply(PowerupManager target)
    {
        // TODO: figure out how to modify speed (perhaps not at 12AM this time)
        Debug.Log("This is where I'd put my speed increase... if I had one.");
        // Apply speed changes
         targetTank = target.GetComponent<TankPawn>();
         if (targetTank != null)
        {
            // The second parameter is the pawn who caused the speeding - in this case, they speeded themselves
            targetTank.moveSpeed = targetTank.moveSpeed + speedToAdd;
        }
    }
    public override void Remove(PowerupManager target)
    {
        targetTank.moveSpeed = targetTank.moveSpeed - speedToAdd;
        Debug.Log("This is where I'd remove my speed increase... if I had one.");
    }
}
