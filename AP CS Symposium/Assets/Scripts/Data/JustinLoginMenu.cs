﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;

/**
 * Justin Yau
 * */
public class JustinLoginMenu : MonoBehaviour {

	public TextMeshProUGUI text;
	public GameObject serverMenu;
	public GameObject mainMenu;
	public GameObject networkManager;
	public GameObject joinGame;

	private string userName;
	private string password;

	void Start() {
		userName = "";
		password = "";
		if (GS.Authenticated) {
			serverMenu.SetActive(true);
			mainMenu.SetActive(true);
			networkManager.SetActive (true);
			joinGame.SetActive (true);
			this.gameObject.SetActive(false);
		}
	}

	public void setUsername (string user) {
		userName = user;
	}

	public void setPassword (string pass)  {
		password = pass;
	}


	public void attemptLogin () {
		new GameSparks.Api.Requests.AuthenticationRequest().SetUserName(userName).SetPassword(password).Send((response) => {
			if (!response.HasErrors) {
				serverMenu.SetActive(true);
				mainMenu.SetActive(true);
				networkManager.SetActive (true);
				joinGame.SetActive(true);
				this.gameObject.SetActive(false);
			} else {
				text.SetText("Alert: Incorrect Username or Password!");
			}
		});
	}

	public void attemptRegister() {
		new GameSparks.Api.Requests.RegistrationRequest()
			.SetDisplayName(userName)
			.SetPassword(password)
			.SetUserName(userName)
			.Send((response) => {
				if (!response.HasErrors) {
					text.SetText("Alert: " + userName + " sucessfully registered!");
				}
				else
				{
					text.SetText("Alert: Error registering " + userName + ". Perhaps it is already taken?");
				}
			}
	   );
		new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("PLAYER").SetEventAttribute ("COINS", 500).SetEventAttribute("CS", 0).Send ((response) => {
		});
		new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("STATS").SetEventAttribute ("S", 0).SetEventAttribute("K", 0).Send ((response) => {
		});
		new GameSparks.Api.Requests.LogEventRequest ().SetEventKey ("SKINS").SetEventAttribute ("SK1", "false").SetEventAttribute ("SK2", "false").Send ((response) => {
		});
	} 


}
