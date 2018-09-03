using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;

/**
 * Justin Yau
 * */
public class JustinSkinEquip : MonoBehaviour {

	public int skinEquipped;
	public TextMeshProUGUI alert;
	public JustinSkinLoader loader;
	private int coins;

	// Use this for initialization
	void Start () {
		alert.SetText ("ALERT: Waiting...");
		Update ();
	}
		
	public void updateCurrentInfo() { 
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("LP").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("player_Data");
				skinEquipped = (int) data.GetInt("currentSkin");
				coins = (int) data.GetInt("playerCoins");
			} 
		});
	}

	// Update is called once per frame
	void Update () {
		updateCurrentInfo ();
	}

	public void updateSelection(int num) {
		updateCurrentInfo ();
		new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("PLAYER").SetEventAttribute ("COINS", coins).SetEventAttribute("CS", num).Send ((response) => {
			if (!response.HasErrors) {
				alert.SetText ("ALERT: Item sucessfully equipped!");
			} else {
				alert.SetText ("ALERT: An error has occured while attempting to process your equip! Try again.");
			}
		});
		updateCurrentInfo ();
	}

}
