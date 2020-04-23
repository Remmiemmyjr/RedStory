using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    GameObject Player;
    private Transform location;

    Vector3 ogPos;
    public float shakeTime = 1f;
    public float magnitude = 0.7f;
    public float damp = 0.75f;
    public bool tookDamage = false;

    PlayerController playerScript;

    private void Awake()
    {
        //Stores the camera's transform
        if (transform == null)
        {
            location = GetComponent(typeof(Transform)) as Transform;
        }

        Player = GameObject.Find("Player");
    }

    void Start()
    {
        ogPos = transform.localPosition;

        playerScript = Player.GetComponent<PlayerController>();
    }

   
    void Update()
    {
       if(playerScript.respawn)
        {
            if (shakeTime > 0)
            {
                transform.localPosition = ogPos + Random.insideUnitSphere * magnitude;

                shakeTime -= Time.deltaTime * damp;
            }
            else
            {
                shakeTime = 0f;
                transform.localPosition = ogPos;
            }
        }
    }
}
