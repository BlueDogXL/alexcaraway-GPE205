using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float currentHealth; // current health isn't useful to designers so we hide it in the inspector
    public float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // when we start we usually haven't taken damage so we can just set current health to max health
    }
    public void TakeDamage(float amount, Pawn source) // get a damage and responsibility from elsewhere
    {
        currentHealth = currentHealth - amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(source.name + " did " + amount + " damage to " + gameObject.name);

        if (currentHealth <= 0) // if we sucked too bad, we die
        {
            Die(source);
        }
    }

    public void Die(Pawn source)
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(gameObject); // you lost at tank game, and everyone in the debug log knows you suck
    }
}