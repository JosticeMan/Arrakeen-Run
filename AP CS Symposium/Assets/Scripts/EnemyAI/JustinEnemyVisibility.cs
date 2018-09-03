using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinEnemyVisibility : MonoBehaviour {

	public float sightDistance; //The distance that the visibility detector goes to

	public GameObject mainBody; //The main body of the player

	private BoxCollider2D visibilityCollider; //The collider that was created 
	private JustinEnemyMovement body; //The motor of the body

    //Spawns the collider to detect collisions
	// Use this for initialization
	void Start () {
		visibilityCollider = gameObject.AddComponent<BoxCollider2D>();
		visibilityCollider.offset = new Vector2 (2.5f, 0f);
		visibilityCollider.size = new Vector2 (sightDistance, 1f);
		visibilityCollider.isTrigger = true;
		body = mainBody.GetComponent<JustinEnemyMovement> ();
	}

    //Updates the transform 
	void Update() {
        if(mainBody != null)
        {
            gameObject.transform.position = mainBody.transform.position;
            gameObject.transform.localScale = mainBody.transform.localScale;
        }
	}

    //Called when the collider collides with an object
    //If the collision is with a player, then it will set the appropriate states for the game
	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			body.enemySpotted = true;
            body.playerSpotted = collision.gameObject;
            body.updateSpotted();
            if (body.usingGun)
            {
                body.runFromPlayer();
            }
            //Debug.Log ("Player Spotted!");
        }
	}

    //Once the collider no longer sees a player, then set the appropriate states back to normal
	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			body.enemySpotted = false;
            body.playerSpotted = null;
            //Debug.Log ("Player no longer spotted!");
        }
	}
}
