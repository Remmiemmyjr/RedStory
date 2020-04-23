using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sectioning : MonoBehaviour
{
    GameObject Player;
    GameObject Camera;
    PlayerController playerScript;
    HealthDisplay healthScript;

    public float offset = 1.5f;
    Vector3[] camPos = new Vector3[20];
    Vector3 priorPos;

    private void Awake()
    {
        Player = GameObject.Find("Player");
        Camera = GameObject.Find("Main Camera");
    }

    void Start()
    {
        playerScript = Player.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        ReturnOnDeath();
    }

    //When the player triggers an offscreen border, the position of the camera will move to the next section
    private void OnTriggerEnter2D (Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            //If the player is going forward through the section
            if (playerScript.velocity.x > 0)
            {
                //When triggered, the current position of the camera will be saved
                priorPos = Camera.transform.position;
                //The position of the camera is now moved to the position of the sectioned piece with an offset to "center" it
                Camera.transform.position = this.transform.position * offset;

                /*Camera.transform.position = this.transform.position * offset;*/
            }
            //If the player backtracks and goes back through the section
            else if (playerScript.velocity.x < 0 && priorPos != null)
            {
                Camera.transform.position = priorPos;
            } 
        }
    }

    //If the player respawns, the camera is moved back to where the players spawnpoing now is
    private void ReturnOnDeath()
    {
        if (playerScript.respawn)
        {
            Camera.transform.position = Player.transform.position;
        }
    }
}
