using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : Pickup
{
    public ScorePowerup powerup;
    public void OnTriggerEnter(Collider other)
    {
        // get the other object's PowerupManager
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();

        // If the other object has a PowerupManager
        if (powerupManager != null)
        {
            // Add the powerup
            powerupManager.Add(powerup);

            // Destroy this pickup
            Destroy(gameObject);
        }
    }
}
