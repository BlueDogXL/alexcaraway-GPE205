using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public int lives;
    // Start is called before the first frame update
    public virtual void Start()
    {
        // abstract class so we don't need anything here
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // ditto   
    }
    public virtual void AddToScore(int amount)
    {

    }

}
