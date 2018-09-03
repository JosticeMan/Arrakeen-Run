using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Justin Yau
 * */
public class JustinCameraFollow : MonoBehaviour {

    public Transform target; //This should be the transform of the object you want to follow

    public float smoothSpeed = 0.125f; //The speed at which the camera will follow the player
    public Vector3 offset; //The distance from the target 

	private bool found;

	void Start() {
		found = false;
		if (target != null) {
			found = true;
		}
		UpdateTarget ();
	}

	public void UpdateTarget() {
		if (!found) {
			GameObject t = GameObject.Find ("Character(Clone)");
			if (t == null) {
				GameObject[] t1 = GameObject.FindGameObjectsWithTag ("Player");
				foreach (GameObject t2 in t1) {
					if (t2.GetComponent<JustinPlayerController> ().isLocalPlayer) {
						target = t2.transform;
						found = true;
						return;
					}
				}
			} else {
				target = t.transform.GetChild (0).transform;
				found = true;
			}
		}
	}

    void LateUpdate()
    {
		if (target != null) {
			Vector3 desiredPosition = target.position + offset; //Actual calculated position from movement
			Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed); //Smoothed out position using a lerp function
			smoothedPosition.y = offset.y; //Preset y value to prevent camera from moving up or down
			transform.position = smoothedPosition; //Apply the positioning

			//transform.LookAt(target);
		} else {
			UpdateTarget ();
		}
    }
}
