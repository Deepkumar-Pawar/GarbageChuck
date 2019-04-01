using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public float scoreRectWidth;
	public float scoreRectHeight;
	public int score;

	float screenWidth = Screen.width;
	float screenHeight = Screen.height;
	TruckHitScript THSLeft;
	TruckHitScript THSRight;
	LightManager lightManager;
	ScoreManager scoreManager;
	// Use this for initialization
	void Awake () {
		THSLeft = GameObject.FindGameObjectWithTag ("TSL").GetComponent<TruckHitScript> ();
		THSRight = GameObject.FindGameObjectWithTag ("TSR").GetComponent<TruckHitScript> ();
		lightManager = GameObject.FindGameObjectWithTag ("LightManager").GetComponent<LightManager> ();
		scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		score = scoreManager.scoreWet + scoreManager.scoreDry;
	}

	void OnGUI()
	{
		GUI.Box (new Rect (5, 5, scoreRectWidth, scoreRectHeight), "Total Score :" + score.ToString());

		if (GUI.Toggle (new Rect (5, (7 + scoreRectHeight), scoreRectWidth, scoreRectHeight), !lightManager.daytime || lightManager.daytime, "Nighttime")) {
			if (lightManager.daytime) {
				lightManager.daytime = false;
			}
			else if (!lightManager.daytime) {
				lightManager.daytime = true;
			}
		}
		GUI.Toggle (new Rect (5, (7 + scoreRectHeight), scoreRectWidth, scoreRectHeight), !lightManager.daytime, "Nighttime");
	}
}
