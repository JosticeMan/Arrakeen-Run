using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Justin Yau
 * */
public class JustinShowEquip : MonoBehaviour {

	public GameObject equipButton;
	public GameObject equippedDisplay;

	public void showEquipButton() {
		equipButton.SetActive (true);
		equippedDisplay.SetActive (false);
	}

	public void showDisplay() {
		equippedDisplay.SetActive (true);
		equipButton.SetActive (false);
	}

}
