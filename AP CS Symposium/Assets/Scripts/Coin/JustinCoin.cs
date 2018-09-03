using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinCoin : MonoBehaviour {

    public int coinValue; //The amount of points the player gets per coin

    private JustinHighscore score; //The actual score component of the game

	// Use this for initialization
	void Start () {

        score = GameObject.Find("Highscore").GetComponent<JustinHighscore>();

	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            score.AddScore(coinValue);
            Destroy(gameObject);
        }    
    }

}
