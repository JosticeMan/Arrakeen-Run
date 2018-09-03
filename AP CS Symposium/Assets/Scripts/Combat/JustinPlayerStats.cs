using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Justin Yau
 * */
public class JustinPlayerStats : NetworkBehaviour {

    public float maxArmor = 100f; //Max armor a player can have
    public float maxHealth = 100f; //Max health a player can have

    public JustinHealthBar healthDisplay; //The health bar display will be here
    public int scoreValue; //The value of the player upon death\

	[SerializeField]
    private GameObject mainGame; //The main game
	[SerializeField]
    private GameObject gameOver; //The gameover menu

    private JustinHighscore score; //The current score of the player
    private float health; //Current health will be stored here
    private float armor; //Current armor will be stored here

	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		GameObject.Find ("Main Camera").GetComponent<JustinCameraFollow> ().target = gameObject.transform;
        health = maxHealth;
        armor = 0;
        score = GameObject.Find("Highscore").GetComponent<JustinHighscore>();
        JustinReference r = GameObject.Find("ReferencePoint").GetComponent<JustinReference>();
        mainGame = r.mainMenu;
        gameOver = r.gameOver;
		if (GameObject.Find ("Player(Clone)") != null) {
			gameOver = r.gameOverNetwork;
		}
        if (transform.gameObject.tag == "Player")
        {
            healthDisplay = GameObject.Find("Green Health Bar").GetComponent<JustinHealthBar>();
        }
        if(healthDisplay != null)
        {
            healthDisplay.maxHealth = maxHealth;
            healthDisplay.currentHealth = health;
        }
	}

	// Update is called once per frame
	void Update () { 
		if (!isLocalPlayer) {
			return;
		}
		if(health <= 0)
        {
            score.AddScore(scoreValue);
            if(scoreValue == 0)
            {
                score.scoreIncreasing = false;
            }
            if(transform.gameObject.tag == "Player" && gameOver != null && mainGame != null)
            {
                gameOver.SetActive(true);
                mainGame.SetActive(false);
            }
            if(gameObject.transform.parent == null)
            {

                Destroy(gameObject);

            } else
            {
                if(gameObject.transform.parent.gameObject.tag == "Player")
                {
                    Destroy(gameObject.transform.parent.gameObject);
                }
            }
        }
	}

    //Adds the specified hp to the current health
    public void updateHealth(float add)
    {
        Debug.Log(true);
		if (!isLocalPlayer) {
			return;
		}
        health += add;
        if(health < 0)
        {
            health = 0;
        }
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(healthDisplay != null)
        {
            healthDisplay.currentHealth = health;
        }
    }

    //Adds the specified armor to the current armor
    public void updateArmor(float add)
    {
		if (!isLocalPlayer) {
			return;
		}
        armor += add;
        if(armor < 0)
        {
            armor = 0;
        }
        if(armor > maxArmor)
        {
            armor = maxArmor;
        }
    }

}
