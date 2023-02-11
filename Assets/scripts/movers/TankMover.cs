using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;
    // Start is called before the first frame update
    public override void Start()
    {
        rb = GetComponent<Rigidbody>(); // get the rigidbody we're attached to
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime; // we move per second rather than per frame
        rb.MovePosition(rb.position + moveVector); // move that rigidbody
    }

    public override void Rotate(float speed)
    {
        transform.Rotate(0, speed * Time.deltaTime, 0); // rotate that transform
    }
}
