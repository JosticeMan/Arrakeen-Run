using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSparks.Core;

/**
 * Justin Yau
 * */
public class JustinSkinLoader : MonoBehaviour {

	public GameObject[] skinOptions; 
	public bool[] skinPurchased;
	public GameObject defaultSkin;
	public JustinSkinEquip sE;
	public string[] purchased;

	private bool isEquipMenu;

	void Start() {
		skinPurchased = new bool[skinOptions.Length];
		purchased = new string[skinOptions.Length];
		if (sE == null && defaultSkin == null) {
			isEquipMenu = false;
		} else
        {
            isEquipMenu = true;
        }
	}

	public void updateSkin() {
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GSKINS").Send((response) => {
			if (!response.HasErrors) {
				GSData data = response.ScriptData.GetGSData("skin_Data");
				List<GSData> d = data.GetGSDataList("SKINS");
				for(int i = 0; i < d.Count; i++) {
					purchased[i] = d[i].GetString("PURCHASED");
				}
			}
		});
	}

	public void Update() {
		updateSkin ();
		for (int i = 0; i < skinPurchased.Length; i++) {
			if (purchased [i] == "true") {
				skinPurchased [i] = true;
			} else {
				skinPurchased [i] = false;
			}
		}
		if (isEquipMenu) { //For the wardrobe
			showEquippers ();
		} else {
			showOptions ();
		}
	}

	public void showOptions() {
		for (int i = 1; i < skinPurchased.Length + 1; i++) {
			if (isPurchased (i)) {
				skinOptions [i - 1].SetActive (false);
			} else {
				skinOptions [i - 1].SetActive (true); 
			}
		}
	}

	public void showEquippers() {
		for (int i = 1; i < skinPurchased.Length + 1; i++) {
			if (isPurchased (i)) {
				skinOptions [i - 1].SetActive (true);
			} else {
				skinOptions [i - 1].SetActive (false); 
			}
		}
		if (sE != null && defaultSkin != null) { //For the wardrobe
			int currentSkin = sE.skinEquipped;
			if (currentSkin == 0) {
				defaultSkin.GetComponent<JustinShowEquip> ().showDisplay ();
			} else {
				defaultSkin.GetComponent<JustinShowEquip> ().showEquipButton ();
			}
			for (int i = 1; i < skinOptions.Length + 1; i++) {
				if (currentSkin == i) {
					skinOptions[i - 1].GetComponent<JustinShowEquip> ().showDisplay ();
				} else {
					skinOptions[i - 1].GetComponent<JustinShowEquip> ().showEquipButton ();
				}
			}
		}
	}

	public bool isPurchased(int skinNumber) {
		if (skinNumber <= 0) {
			return false;
		}
		return skinPurchased [skinNumber - 1];
	}

}
