using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/**
 * Justin Yau
 * */
public class JustinHostGame : MonoBehaviour {

	[SerializeField] 
	private uint roomSize = 2;

	private string roomName;

	private NetworkManager nManager;

	// Use this for initialization
	void Start () {
		nManager = NetworkManager.singleton;
		if (nManager.matchMaker == null) {
			nManager.StartMatchMaker ();
		}
		roomName = "default";
	}
	
	public void setRoomName(string rName) {
		roomName = rName;
	}

	public void createRoom() {
		if (roomName != "" && roomName != null) {
			Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players.");
			nManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, nManager.OnMatchCreate);
		}
	}

}
