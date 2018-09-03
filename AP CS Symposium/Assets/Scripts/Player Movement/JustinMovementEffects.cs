using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinMovementEffects : MonoBehaviour {

	public GameObject movementEffect; //Movement effect will be stored here
	public GameObject jumpEffect;  //Jump effect will be stored here

	private Animator playerAnim; //Main script handling player animations

	void Start() {
		playerAnim = GetComponent<Animator> ();
		if (movementEffect != null && jumpEffect != null) {
			movementEffect.SetActive (false);
			jumpEffect.SetActive (false);
		}
	}

	// Update is called once per frame
	void Update () {
		if (playerAnim.GetFloat ("Speed") > 0) {
			if (playerAnim.GetBool ("Grounded")) {
				movementEffect.SetActive (true);
				jumpEffect.SetActive (false);
			} else {
				movementEffect.SetActive (false);
				jumpEffect.SetActive (true);
			}
		} else {
			movementEffect.SetActive (false);
			jumpEffect.SetActive (false);
		}
	}
}
