using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinLock : MonoBehaviour {

	private Vector3 originalPos;

	// Use this for initialization
	void Start () {
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = originalPos;
	}
}
