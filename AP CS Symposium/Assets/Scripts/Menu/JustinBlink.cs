using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JustinBlink : MonoBehaviour {

		// the image you want to fade, assign in inspector
		public TextMeshProUGUI text;

		void Start()
		{
			// fades the image out when you click
			StartCoroutine(FadeImage());
		}

		IEnumerator FadeImage()
		{
			while (true) {
				// loop over 1 second backwards
				for (float i = 1; i >= 0; i -= Time.deltaTime)
				{
					// set color with i as alpha
					text.color = new Color(1, 1, 1, i);
					yield return null;
				}
				// loop over 1 second
				for (float i = 0; i <= 1; i += Time.deltaTime)
				{
					// set color with i as alpha
					text.color = new Color(1, 1, 1, i);
					yield return null;
				}
			}
		}
}
