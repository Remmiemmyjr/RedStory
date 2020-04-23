using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float Speed = 0.09f;
    public int Health = 1;
    public int Damage = 1;


    void Start()
    {

    }


    void Update()
    {
        transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);

        if (Health <= 0)
        {
            Death();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EmptyScript boundry = collision.gameObject.GetComponent<EmptyScript>();
        if (boundry != null)
        {
            print("Collided");
            Speed = -Speed;
        }
    }
    void Death()//edit death here
    {
        Destroy(gameObject);
    }
}
