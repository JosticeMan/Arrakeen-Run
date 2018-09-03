using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;

public class JustinScoreRetriever : MonoBehaviour {

	public GameObject newKillDisplay;
	public GameObject newGoalDisplay;
	public int missionReward;

    private TextMeshProUGUI scoreText;
	private int kills;
	private int goals;
	private int bonus;

    // Use this for initialization
    void Start()
    {
		newKillDisplay.SetActive (false);
		newGoalDisplay.SetActive (false);

        scoreText = GetComponent<TextMeshProUGUI>();

		int score = (int) Mathf.Round (PlayerPrefs.GetFloat ("Last Score"));
		scoreText.SetText("Score : " + score);

		updatePlayerStats (score);
		handleCoinReward (score);

    }

	void updatePlayerStats(int score) {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GSTATS").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("stats_Data");
				kills = (int) data.GetInt("playerKillGoals");
				goals = (int) data.GetInt("playerGoals");
				determineAchievement(score);
			}
		});
	}

	void determineAchievement(int score) {
		if (PlayerPrefs.GetInt ("Kills") >= ((kills * 10) + 10)) {
			newKillDisplay.SetActive (true);
			new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("STATS").SetEventAttribute ("K", kills + 1).SetEventAttribute ("S", goals).Send ((response) => {
				rewardCoins(((missionReward * kills) + missionReward));
			});
		}
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GSTATS").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("stats_Data");
				kills = (int) data.GetInt("playerKillGoals");
				if (score >= ((goals * 50) + 50)) {
					newGoalDisplay.SetActive (true);
					new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("STATS").SetEventAttribute ("K", kills).SetEventAttribute ("S", goals + 1).Send ((response1) => {
						rewardCoins(((missionReward * goals) + missionReward));
					});
				}
			}
		});
	}
		
	void rewardCoins(int score) {
		int coins = 0;
		int currentSkin = 0;

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				coins = (int) data.GetInt("playerCoins") + score;
				currentSkin = (int) data.GetInt("currentSkin");

				new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("PLAYER").SetEventAttribute ("COINS", coins).SetEventAttribute("CS", currentSkin).Send ((response1) => {
				});
			} 
		});
	}

	void handleCoinReward(int score) {
		int coins = 0;
		int currentSkin = 0;

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				coins = (int) data.GetInt("playerCoins") + score;
				currentSkin = (int) data.GetInt("currentSkin");

				new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("PLAYER").SetEventAttribute ("COINS", coins).SetEventAttribute("CS", currentSkin).Send ((response1) => {
				});
			} 
		});

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("SCORE").SetEventAttribute ("S", score).Send((response) => {
		});
	}

}
