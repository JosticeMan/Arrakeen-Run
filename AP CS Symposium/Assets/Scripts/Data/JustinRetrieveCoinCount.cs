using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;

/**
 * Justin Yau
 * */
public class JustinRetrieveCoinCount : MonoBehaviour {

	public TextMeshProUGUI coinCount;
	public TextMeshProUGUI alert;
	public JustinSkinLoader loader;
	public int[] prices;

	public int coins;
	public int skinCount;
	public string[] skinPurchased;

	private int currentSkin;

	// Use this for initialization
	void Start () {
		skinPurchased = new string[skinCount];
		for (int i = 0; i < skinPurchased.Length; i++) {
			skinPurchased [i] = "false";
		}
		updateCoinCount ();
		updateSkin ();
		alert.SetText ("ALERT: Waiting...");
	}

	void Update() {
		updateCoinCount ();
		updateSkin ();
		coinCount.SetText ("COINS: " + coins);
	}

	public void updateCoinCount() {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				coins = (int) data.GetInt("playerCoins");
				currentSkin = (int) data.GetInt("currentSkin");
			} else {
				coins = -1;
			}
		});
	}

	public void updateSkin() {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GSKINS").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("skin_Data");
				List<GSData> d = data.GetGSDataList("SKINS");
				for(int i = 0; i < d.Count; i++) {
					skinPurchased[i] = d[i].GetString("PURCHASED");
				}
			}
		});
	}

	public void attemptPurchase(int skinNumber) {
		int price = prices[skinNumber - 1];
		updateCoinCount ();
		if (coins - price >= 0) {
			buySkin (skinNumber);
		} else {
			alert.SetText ("ALERT: You do not have enough coins to make the purchase!");
		}
	}

	public void deductCoins(int skinNumber) {
		int price = prices[skinNumber - 1];
		new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("PLAYER").SetEventAttribute ("COINS", coins - price).SetEventAttribute("CS", currentSkin).Send ((response) => {
			if (!response.HasErrors) {
				alert.SetText ("ALERT: Item purchased successfully!");
			} else {
				alert.SetText ("ALERT: An error has occured while attempting to process your purchase! Try again.");
			}
		});
		updateCoinCount ();
	}

	public void buySkin(int skinNumber) {
		updateSkin (); 
		if (skinPurchased[skinNumber - 1] == "true") {
			alert.SetText ("ALERT: Skin already owned!");
			return;
		}
		if (skinNumber == 1) {
			new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("SKINS").SetEventAttribute ("SK1", "true").SetEventAttribute ("SK2", skinPurchased[1]).Send ((response) => {
				if (!response.HasErrors) {
					updateSkin ();
					deductCoins(skinNumber);
				} 
			});
		}
		else if (skinNumber == 2) {
			new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("SKINS").SetEventAttribute ("SK1", skinPurchased[0]).SetEventAttribute("SK2", "true").Send ((response) => {
				if (!response.HasErrors) {
					updateSkin ();
					deductCoins(skinNumber);
				}
			});
		}
	}

}
