using UnityEngine;
using System.Collections;

public enum BinType
{
Wet,
	Dry}
;

public class TruckHitScript : MonoBehaviour
{

	public BinType binType;
	public int score;

	//	GameObject scoreObj;
	BinProperties binProp;
	//	TextMesh textM;
	GUIScript guiScript;
	ScoreManager scoreManager;
	// Use this for initialization
	void Start ()
	{
//		if (binType == BinType.Dry) {
//			scoreObj = GameObject.FindGameObjectWithTag ("DryScore");
//		} else {
//			scoreObj = GameObject.FindGameObjectWithTag ("WetScore");
//		}
//
//		textM = scoreObj.gameObject.GetComponent<TextMesh> ();
		guiScript = GameObject.FindGameObjectWithTag ("GUIManager").GetComponent<GUIScript> ();
		scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag.ToUpper () == "Bin".ToUpper ()) {
			binProp = other.gameObject.GetComponent<BinProperties> ();
			if (binProp.binType == binType) {
				if (binType == BinType.Dry) {
					scoreManager.scoreDry++;
				} else
					scoreManager.scoreWet++;

			} else {
				if (guiScript.score > 0) {
					if (binType == BinType.Dry) {
						scoreManager.scoreDry++;
					} else
						scoreManager.scoreWet--;
				}
			}
			if (binType == BinType.Dry) {
				score = scoreManager.scoreDry;
			} else
				score = scoreManager.scoreWet;
		}
			


		scoreManager.textDryM.text = scoreManager.scoreDry.ToString ();
		scoreManager.textWetM.text = scoreManager.scoreWet.ToString ();
	}
}
