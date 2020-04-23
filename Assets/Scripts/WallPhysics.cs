using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPhysics : MonoBehaviour
{
    public LayerMask ground;

    public bool grounded;
    public bool onWall;
    public bool latchRight;
    public bool latchLeft;
    int wallSide;

    public float radius;
    public Vector2 bottom, left, right;

    void Start()
    {
        //Creates a circle on the bottom to check if the player is grounded. Bottom circle is touching the layer, this means its on the ground.
        grounded = Physics2D.OverlapCircle((Vector2)transform.position + bottom, radius, ground);
        //Creates circles on the right and left to check if the player is on the wall. Right or left circle could be touching the layers meanings its on a wall.
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + right, radius, ground) || Physics2D.OverlapCircle((Vector2)transform.position + right, radius, ground);
        //Determines what wall we are grabbed onto
        latchRight = Physics2D.OverlapCircle((Vector2)transform.position + right, radius, ground);
        latchLeft = Physics2D.OverlapCircle((Vector2)transform.position + left, radius, ground);
    }

  
    void Update()
    {
        
    }
}
