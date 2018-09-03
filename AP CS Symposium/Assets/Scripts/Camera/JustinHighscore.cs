using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 * Justin Yau
 * */
public class JustinHighscore : MonoBehaviour {

    public TextMeshProUGUI scoreText; //The text mesh responsible for showing the score
    public TextMeshProUGUI highscoreGUI; //The text mesh responsible for showing the highest score

    public float pointsPerSecond; //The points the player gets per second 
    public float highScoreCount; //The highest score of the player
    public float score; //The score of the player
    public bool scoreIncreasing; //The boolean controlling whether the player keeps getting points per second

	// Use this for initialization
	void Start () {
        score = 0;
        highScoreCount = 0;
        highScoreCount = PlayerPrefs.GetFloat("Highscore");
	}
	
    /// <summary>
    /// Resets the current score back to zero
    /// </summary>
    public void ResetScore()
    {
        score = 0;
    }

    /// <summary>
    /// Adds the given amount to the current score
    /// </summary>
    /// <param name="change"> The amount to add to the score. Can be negative </param>
    public void AddScore(int change)
    {
        score += change;
    }

	// Update is called once per frame
	void Update () {

        if(scoreIncreasing)
        {
            score += pointsPerSecond * Time.deltaTime;
            PlayerPrefs.SetFloat("Last Score", score);
        }

        if(score > highScoreCount)
        {
            highScoreCount = score;
            PlayerPrefs.SetFloat("Highscore", highScoreCount);
        }

        scoreText.text = "" + Mathf.Round(score);
        highscoreGUI.text = "Highscore: " + Mathf.Round(highScoreCount);
	}
}
