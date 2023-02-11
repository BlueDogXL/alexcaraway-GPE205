using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone; // how much damage and who's responsible
    public Pawn owner;
    
    public void OnTriggerEnter(Collider other)
    {
        Health otherHealth = other.gameObject.GetComponent<Health>(); // get the other thing's health component

        if (otherHealth != null) // if it actually has one and we didn't hit a wall or something
        {
            otherHealth.TakeDamage(damageDone, owner); // damage 
        }
        Destroy(gameObject); // and then self destruct
    }
}
