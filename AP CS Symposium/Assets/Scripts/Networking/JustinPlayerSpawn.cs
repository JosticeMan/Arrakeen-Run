using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinPlayerSpawn : MonoBehaviour {

	private GameObject mainGame; //The gameobject that contains all the objects for the mainGame

	// Use this for initialization
	void Start () {
		mainGame = GameObject.Find ("MainGame");
		transform.parent = mainGame.transform;
	}

}
