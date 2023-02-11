using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    // wahoo abstract functions
    public abstract void Start();

    public abstract void Update();

    public abstract void Shoot(GameObject shellPrefab, float shootForce, float damageDone, float lifespan);
}
