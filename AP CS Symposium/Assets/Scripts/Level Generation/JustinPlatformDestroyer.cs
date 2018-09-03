using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinPlatformDestroyer : MonoBehaviour {

    private GameObject destructionPoint; //The point that objects cannot be behind
    private GameObject mC; //The main camera 


	// Use this for initialization
	void Start () {

        destructionPoint = GameObject.Find("PlatformDestructionPoint");

    }
	
    //Checks whether or not to destroy the platform
	// Update is called once per frame
	void Update () {

        if (transform.position.x < destructionPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

	}
}
