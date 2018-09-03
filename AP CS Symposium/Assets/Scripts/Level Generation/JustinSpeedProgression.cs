using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinSpeedProgression : MonoBehaviour {

	public float intervalIncrease; //The amount of seconds before the speed of the player goes up
	public float increaseAmount; //The amount that is increased each interval

	private JustinPlayerController body; //The body of the player on a network server
	private float timePassed; //The amount of time that passed since the last increase

	// Use this for initialization
	void Start () {
		timePassed = 0f;
		body = GetComponent<JustinPlayerController> ();
		if (body == null) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed > intervalIncrease && body != null) {
			if (body.playerVelocity + increaseAmount <= body.maxSpeed) {
				body.playerVelocity += increaseAmount;
				timePassed = 0f;
			}
		}
	}
}
