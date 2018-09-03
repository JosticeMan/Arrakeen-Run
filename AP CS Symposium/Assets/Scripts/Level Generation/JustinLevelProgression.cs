using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinLevelProgression : MonoBehaviour {

	public float difficultIncreaseInterval; //The amount of seconds before difficulty increases
	public float maxSpawnRate; //The max rate the enemies can spawn at
	public float increaseAmount; //The increase in the percentage each interval

	private JustinObjectGenerator enemySpawner; //The object generator of the enemies. MUST BE THE CORRECT SPAWNER
	private float timePassed;

	// Use this for initialization
	void Start () {
		timePassed = 0f;
		enemySpawner = GetComponent<JustinObjectGenerator> ();
		if (enemySpawner == null) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (enemySpawner != null && timePassed > difficultIncreaseInterval) {
			if (enemySpawner.spawnChance + increaseAmount <= maxSpawnRate) {
				enemySpawner.spawnChance += increaseAmount;
				timePassed = 0f;
			}
		}
	}
}
