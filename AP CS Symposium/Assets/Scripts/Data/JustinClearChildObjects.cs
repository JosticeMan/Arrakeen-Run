using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustinClearChildObjects : MonoBehaviour {

	public void Clear () {
		foreach (Transform t in gameObject.transform) {
			Destroy (t.gameObject);
		}
	}

}
