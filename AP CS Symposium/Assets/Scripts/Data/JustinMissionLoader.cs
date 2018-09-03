using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;

/**
 * Justin Yau
 * */
public class JustinMissionLoader : MonoBehaviour {

	public GameObject missionPrefab;
	public Transform parent;

	// Use this for initialization
	void Start () {
		Update ();
	}

	void Update() {
		int kills, goals;

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GSTATS").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("stats_Data");
				kills = (int) data.GetInt("playerKillGoals");
				goals = (int) data.GetInt("playerGoals");
				generateMissions(kills, goals);
			}
		});
	}

	void generateMissions(int kills, int goals) {
		parent.GetComponent<JustinClearChildObjects> ().Clear ();
		GameObject killMission = Instantiate (missionPrefab);
		killMission.transform.SetParent (parent);
		killMission.GetComponent<TextMeshProUGUI> ().SetText ("Kill " + ((kills * 10) + 10) + "+ enemies in one game");
		GameObject goalMission = Instantiate (missionPrefab);
		goalMission.transform.SetParent (parent); 
		goalMission.GetComponent<TextMeshProUGUI> ().SetText ("Reach a score of " + ((goals * 50) + 50) + " in one game");
	}

}
