using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Justin Yau
 * */
public class JustinHighScoreRetriever : MonoBehaviour {

    private TextMeshProUGUI hScoreText;

	// Use this for initialization
	void Start () {

		hScoreText = GetComponent<TextMeshProUGUI> ();
        hScoreText.SetText("Highscore : " + Mathf.Round(PlayerPrefs.GetFloat("Highscore")));

    }

    void Update()
    {
        hScoreText.SetText("Highscore : " + Mathf.Round(PlayerPrefs.GetFloat("Highscore")));
    }

}
