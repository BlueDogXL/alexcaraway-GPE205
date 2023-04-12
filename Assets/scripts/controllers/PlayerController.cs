using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerController : Controller
{
    // lets designers set keys for actions
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode turnLeftKey;
    public KeyCode turnRightKey;
    public KeyCode shootKey;
    // score
    public int score;

    // Start is called before the first frame update
    public override void Start()
    {
        // if there is a game manager and it has a list of players, add this playercontroller to it
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Add(this);
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        // checking for inputs, if there are some tell the pawn to do stuff
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(turnLeftKey))
        {
            pawn.TurnLeft();
        }

        if (Input.GetKey(turnRightKey))
        {
            pawn.TurnRight();
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }
    public void OnDestroy()
    {
        // if this gets destroyed remove it from the players list
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }
}
