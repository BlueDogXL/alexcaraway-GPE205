using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ScorePowerup : Powerup
{
    public int scoreToAdd;

    public override void Apply(PowerupManager target)
    {
        // Apply Health changes
        TankPawn targetScore = target.GetComponent<TankPawn>();
        if (targetScore != null)
        {
            // The second parameter is the pawn who caused the healing - in this case, they healed themselves
            targetScore.controller.AddToScore(scoreToAdd);
        }
    }
    public override void Remove(PowerupManager target)
    {
        // todo removes
    }
}
