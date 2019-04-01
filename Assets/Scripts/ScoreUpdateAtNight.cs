using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpdateAtNight : MonoBehaviour {

	public TextMesh dayDryScoreText;
	public TextMesh dayWetScoreText;

	public bool isDry;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isDry) {
			gameObject.GetComponent<TextMesh> ().text = dayDryScoreText.text;
		} else {
			gameObject.GetComponent<TextMesh> ().text = dayWetScoreText.text;
		}
	}
}
