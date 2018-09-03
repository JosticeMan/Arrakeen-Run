using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

/**
 * Justin Yau
 * */
public class JustinDisconnectCommunicator : NetworkBehaviour {

	private NetworkManager nManager;

	public void Start() {
		nManager = NetworkManager.singleton;
	}

	public void attemptDisconnect() {
		MatchInfo match = nManager.matchInfo;
		nManager.matchMaker.DropConnection (match.networkId, match.nodeId, 0, nManager.OnDropConnection);
		nManager.StopHost ();
	}

}
