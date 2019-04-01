using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int scoreWet;
	public int scoreDry;

	GUIScript guiScript;
	GameObject scoreWetObj;
	GameObject scoreDryObj;

	[HideInInspector]
	public TextMesh textWetM;
	[HideInInspector]
	public TextMesh textDryM;

	// Use this for initialization
	void Awake () {
		scoreWetObj = GameObject.FindGameObjectWithTag ("WetScore");
		scoreDryObj = GameObject.FindGameObjectWithTag ("DryScore");

		textDryM = scoreDryObj.gameObject.GetComponent<TextMesh> ();
		textWetM = scoreWetObj.gameObject.GetComponent<TextMesh> ();
		guiScript = GameObject.FindGameObjectWithTag ("GUIManager").GetComponent<GUIScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
