using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameSparks.Core;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

/**
 * Justin Yau
 * */
public class JustinHighscoreLoader : MonoBehaviour {

	public GameObject scorePrefab;
	public Transform parent;

	// Use this for initialization
	void Start () {

		int entryCount = 3;
		int includeFirst = 1;
		int includeLast = 1;
		string leaderboardShortCode = "HIGHSCORE";

		new AroundMeLeaderboardRequest()
			.SetEntryCount(entryCount)
			.SetIncludeFirst(includeFirst)
			.SetIncludeLast(includeLast)
			.SetLeaderboardShortCode(leaderboardShortCode)
			.Send((response) => {

				GSEnumerable<AroundMeLeaderboardResponse._LeaderboardData> data = response.Data;
				GSEnumerable<AroundMeLeaderboardResponse._LeaderboardData> first = response.First;
				GSEnumerable<AroundMeLeaderboardResponse._LeaderboardData> last = response.Last;

				lookThrough(first);
				lookThrough(data);
				lookThrough(last);
					
			});
	}

	void lookThrough(GSEnumerable<AroundMeLeaderboardResponse._LeaderboardData> arr) {
		foreach (var d in arr) {
			GameObject score = Instantiate(scorePrefab);
			score.transform.SetParent(parent);
			score.GetComponent<TextMeshProUGUI>().SetText(d.Rank + ": " + d.UserName + " - " + d.JSONData["S"].ToString());
		}
	}

}
