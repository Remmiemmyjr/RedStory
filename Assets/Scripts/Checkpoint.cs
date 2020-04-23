using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject Player;
    PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            playerScript.savePoint = this.transform.position;
            playerScript.spawnPointCamera = this.transform.position;
        }
        
    }
}
