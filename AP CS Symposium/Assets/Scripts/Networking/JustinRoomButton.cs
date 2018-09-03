using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;


/**
 * Justin Yau
 * */
public class JustinRoomButton : MonoBehaviour {

	public delegate void JoinRoomDelgate(MatchInfoSnapshot snap);
	private JoinRoomDelgate jRoomCallBack;

	[SerializeField] 
	private Text buttonText;

	private MatchInfoSnapshot desc;

	public void Setup(MatchInfoSnapshot match, JoinRoomDelgate callback) {

		desc = match;
		jRoomCallBack = callback;

		buttonText.text = desc.name + " (" + desc.currentSize + "/" + desc.maxSize + ")";

	}

	public void JoinGame() {
		jRoomCallBack.Invoke (desc);
	}
	
}
