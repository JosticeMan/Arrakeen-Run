using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinPlayerNN : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Player(Clone)") != null)
        {
            Destroy(gameObject);
        }
	}
}
