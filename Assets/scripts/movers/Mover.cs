using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    // none of this matters it's abstract
    // well i guess we have to add actions here before we can code em elsewhere but like that's it
    public abstract void Start();

    // Update is called once per frame
    public abstract void Move(Vector3 direction, float speed);
    public abstract void Rotate(float speed);
}
