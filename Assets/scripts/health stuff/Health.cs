using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public float currentHealth; // current health isn't useful to designers so we usually hide it in the inspector EXCEPT i wanna let the AI use it to determine when to flee so i'm publicizing it
    public float maxHealth;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // when we start we usually haven't taken damage so we can just set current health to max health
        // get image component
        
    }
    void Update()
    {
        image.fillAmount = (currentHealth / maxHealth);
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
    public void Heal(float amount, Pawn source)
    {
        currentHealth = currentHealth + amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log(source.name + " healed " + amount + " damage from itself");
    }

    public void Die(Pawn source)
    {
        Debug.Log(source.name + " destroyed " + gameObject.name);
        Destroy(gameObject); // you lost at tank game, and everyone in the debug log knows you suck
    }
}