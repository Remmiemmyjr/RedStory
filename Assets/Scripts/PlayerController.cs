using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

/**********************************
 * PlayerController.cs
 * Written By: Emmy Berg
 * Date: 4/20/20
 * Description: Contains all attributes that interact with the player, as well as the players core functions 
 (movement/physics, health/respawn, attacking, animation etc.)
**********************************/

public class PlayerController : MonoBehaviour
{
    //World Objects
    Rigidbody2D myRB;
    GameObject Player;
    GameObject Enemy;
    GameObject Spikes;
    GameObject Camera;
    GameObject Checkpoint;

    //Spawning and Checkpoints
    public Vector3 savePoint;
    public Vector3 spawnPointCamera;
    private Transform checkPoint;
    public bool respawn;

    //Health Variables
    public int health = 5;

    //Movement Variables
    public float moveSpeed = 5;
    public Vector3 velocity;

    //Jumping Variables
    public int jumpCounter = 2;
    public float jumpHeight = 10f;
    public float constantHeight = 10f;
    public float weight = 2f;

    private void Awake()
    {
        Player = this.gameObject;
        Enemy = GameObject.Find("Enemy");
        Spikes = GameObject.Find("Spikes");
        Camera = GameObject.Find("Main Camera");
        Checkpoint = GameObject.Find("Checkpoint");
    }

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        savePoint = transform.position;
        spawnPointCamera = Camera.transform.position;
    }

    void Update()
    {
        Jump();
        Movement();
        Respawn();
    }

    void Movement()
    {
        //Sets the RigidBody's velocity equal to our own velocity
        velocity = myRB.velocity;

        //Left and Right Controls
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -moveSpeed;
        }
        //Sets velocity to 0 if controls are not being used
        else
        {
            // stop it from creeping forever, adds delay when movement is 0/null
            if (Mathf.Abs(velocity.x) < moveSpeed / 7)  
            {
                velocity.x = 0;
            }
            else
            {
                velocity.x = velocity.x / 2;
            }
        }

        //Sets our own velocity equal to the value of the Rigidbody velocity
        myRB.velocity = velocity;
    }

    void Jump()
    {
        velocity = myRB.velocity;

        //Jump controls, implements double jump restraints by decreasing the "counter" each use
        if (Input.GetKeyDown(KeyCode.UpArrow) && (jumpCounter > 0))
        {
            //Height is multiplied to compensate for the increase of negative velocity
            velocity.y = jumpHeight * 1.4f;
            jumpCounter--;
        }
        //When the player touches a layer (ground) the double jump cap is reset
        else if (myRB.velocity.y == 0 && myRB.IsTouchingLayers())
        {
            jumpCounter = 2;
            //Resets the height to its original value to prevent a constant height increase
            jumpHeight = constantHeight;
        }
        //Increases down-ward momentum to simulate weight
        if (myRB.velocity.y < 0 && myRB.velocity.y > -35f)
        {
            velocity.y *= 1 + (.6f * Time.deltaTime) * weight;
            //myRB.velocity += Vector2.up * Physics2D.gravity.y * (weight - 1) * Time.deltaTime;

        }

        myRB.velocity = velocity;
    }

    //Respawn Function. Has the majority of the death circumstances, and controls respawning/checkpoints.
    void Respawn()
    {
        if (respawn)
        {
            Player.transform.position = savePoint;
            Camera.transform.position = spawnPointCamera;
            respawn = false;
        }
        if (transform.position.y < -5)
        {
            respawn = true;
            health--;
        }
        if (health <= 0)
        {
            //SceneManager.LoadScene("NoHealthEnd");
        }
    }

    //Collision based aspects (taking damage, collecting, etc)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            respawn = true;
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            health--;
            respawn = true;
        }
    }
}

