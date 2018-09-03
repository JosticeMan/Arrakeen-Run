using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

/**
 * Justin Yau
 * */
public class JustinJoinGame : MonoBehaviour {

	List<GameObject> roomList = new List<GameObject> ();

	[SerializeField]
	private Text status;

	[SerializeField] 
	private GameObject roomButtonPrefab;

	[SerializeField] 
	private Transform roomListParent;

	private NetworkManager nManager; 

	// Use this for initialization
	void Start () {
		nManager = JustinNetworkPlayerSpawner.singleton;
		if (nManager.matchMaker == null) {
			nManager.StartMatchMaker ();
		}

		refreshRoomList ();
	}
	
	public void refreshRoomList() {
		clearRoomList ();
		nManager.matchMaker.ListMatches (0, 20, "", false, 0, 0, onMatchList);
		status.text = "Loading...";
	}

	public void onMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches) {
		status.text = "";

		if (matches == null) {
			status.text = "Couldn't get matches...";
			return;
		}

		foreach (MatchInfoSnapshot match in matches) {
			GameObject roomGO = Instantiate (roomButtonPrefab);
			roomGO.transform.SetParent (roomListParent);

			JustinRoomButton btn = roomGO.GetComponent<JustinRoomButton> ();
			if (btn != null) {
				btn.Setup (match, joinRoom);
			}

			roomList.Add (roomGO);
		}

		if (roomList.Count == 0) {
			status.text = "No rooms at the moment...";
		}

	}

	public void clearRoomList() {

		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList [i]);
		}

		roomList.Clear ();

	}

	public void joinRoom(MatchInfoSnapshot match) {
		nManager.matchMaker.JoinMatch (match.networkId, "", "", "", 0, 0, nManager.OnMatchJoined);
		clearRoomList ();
		status.text = "Joining";
	}

}
