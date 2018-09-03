using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinNetworkManagerSearch : MonoBehaviour {

	public void getNetworkManagerHostScript() {
		GameObject.Find ("NetworkManager").GetComponent<JustinHostGame> ().createRoom ();
	}

	public void getNetworkManagerNameScript(string rName) {
		GameObject.Find ("NetworkManager").GetComponent<JustinHostGame> ().setRoomName (rName);
	}

}
