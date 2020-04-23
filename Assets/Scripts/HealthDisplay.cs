using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject Life;
    public GameObject Death;
    public GameObject Player;
    //Array is created to save the sprites that will be displayed
    GameObject[] Sprites;

    public int lastHealth;

    PlayerController playerScript;

    void Start()
    {
        playerScript = Player.GetComponent<PlayerController>();
        lastHealth = playerScript.health;
        Sprites = new GameObject[lastHealth];
        //New vector declared to save the position of the "Life" gameobject (the first heart used to make copies)
        Vector3 lifePosition = Life.transform.position;
        //Gets the spacing between life and death relative to where I put the originals
        float spacing = Death.transform.position.x - Life.transform.position.x;
        for(int i = 0; i < lastHealth; i++)
        {
            /*New copy of the original heart is created, and is set to be the child of the 
            object that holds this script (the canvas)*/
            Sprites[i] = Instantiate(Life, Life.transform.parent);
            //Sets the position of the new copy
            Sprites[i].transform.position = lifePosition;
            //Determines the spacing/positioning of the copies and adds an offset
            lifePosition = new Vector3(lifePosition.x + spacing, lifePosition.y);
        }
        //Ensures that both original copies are hidden after instantiation, since they wont be used
        Life.GetComponent<Renderer>().enabled = false;
        Death.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        //Checks to see if the health has decreased, and is less than what it used to be
        
        if (playerScript.health < lastHealth)
        {
            
            if (playerScript.health < 0)
            {
                playerScript.health = 0;
            }
            while (lastHealth > playerScript.health)
            {
                //Last health is pointing at a sprite 1 over, so there needs to be a decrement
                lastHealth--;
                //Saved the new death object in a temp area so we can replace the old object (Repeats what was done to life)
                var temp = Instantiate(Death, Death.transform.parent);
                temp.transform.position = Sprites[lastHealth].transform.position;
                //Ensures that any death instatiations are now rendered
                temp.GetComponent<Renderer>().enabled = true;
                Destroy(Sprites[lastHealth]);
                //Replaces the Life sprite with Death
                Sprites[lastHealth] = temp;
            }
        }
    }
}
