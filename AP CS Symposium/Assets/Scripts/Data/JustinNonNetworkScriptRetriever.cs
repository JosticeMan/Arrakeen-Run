using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;

public class JustinNonNetworkScriptRetriever : MonoBehaviour {

	public GameObject[] prefabs;
	public GameObject camera;
	public GameObject mainGame;

	private int skinEquipped;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Kills", 0);
		updateCurrentInfo ();
	}

	public void updateCurrentInfo() { 
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				skinEquipped = (int) data.GetInt("currentSkin");
				GameObject player = Instantiate (prefabs [skinEquipped], mainGame.transform);
				player.transform.position = new Vector3(0f, 1f, 0f);
				camera.SetActive (true);
				mainGame.SetActive (true);
				gameObject.SetActive (false);
			} 
		});
	}

}
