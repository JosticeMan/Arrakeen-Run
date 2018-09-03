using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Justin Yau
 * */
public class JustinOptionMenu : MonoBehaviour {

    public GameObject optionMenuCanvas; //The canvas of the option menu
	public GameObject onlineOptionMenuCanvas; //The online version of the option menu
    public GameObject mainGame; //The game object of the main game

    private bool isMenuOpen; //Whether or not the option menu is open
    private bool online; //Whether or not the menu is online

    // Use this for initialization
    void Start()
    {
        isMenuOpen = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(isMenuOpen && mainGame.activeSelf)
        {
            isMenuOpen = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
            {
                // open menu
                isMenuOpen = true;
				if (GameObject.Find ("Player(Clone)") != null) {
					onlineOptionMenuCanvas.SetActive (true);
                    online = true;
				} else {
					optionMenuCanvas.SetActive(true);
				}
                mainGame.SetActive(false);
            }
            else
            {
                // close menu
                isMenuOpen = false;
                if (online)
                {
                    onlineOptionMenuCanvas.SetActive(false);
                }
                else
                {
                    optionMenuCanvas.SetActive(false);
				}
                mainGame.SetActive(true);
            }
        }
    }
}
