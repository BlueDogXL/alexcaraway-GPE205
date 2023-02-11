using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform shootPoint; // where we spawn bullets
    // Start is called before the first frame update
    public override void Start()
    {
        // *brian david gilbert voice* there's NOTHING HERE!
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }
    
    public override void Shoot(GameObject shellPrefab, float shootForce, float damageDone, float lifespan)
    {
        // instantiate projectile
        GameObject newShell = Instantiate(shellPrefab, shootPoint.position, shootPoint.rotation);
        // get damage on hit script
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        if (doh != null) // this stuff is delivered to the health component
        {
            doh.damageDone = damageDone;
            doh.owner = GetComponent<Pawn>();
        }

        Rigidbody rb = newShell.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(shootPoint.forward * shootForce); // launch that rigidbody with the might of shootForce
        }

        Destroy(newShell, lifespan); // and, if we miss, destroy it so it doesn't clog up unity. there's a smarter way to say that but i forgor :skull:
    }
}
