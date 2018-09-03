using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinHealthBar : MonoBehaviour {

    public float maxHealth = 100f; //The maximum health of the player
    public float currentHealth; //The current health of the player

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
    }

	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3((currentHealth / maxHealth), 1, 1);
	}
}
