using UnityEngine;
using System.Collections;

public class TrajCont : MonoBehaviour {

	public bool DEBUG = false;
	public LineRenderer TrajDisplay;
	public int MouseButton = 0;
	public float InputToPower = 1f;
	public float MaxPower = 2f;
	public GameObject Projectile;

	bool anchored = false;
	Vector3 AnchorPos = Vector3.zero;
	Vector3 AnchorDir = Vector3.zero;
	float AnchorDist = 0f;
	TrajCalc TCalc;
	BinManager binMan;
	BinProperties binProp;

	// Use this for initialization
	void Start () {
		

		if (gameObject.GetComponent<TrajCalc>()) {
			TCalc = gameObject.GetComponent<TrajCalc> ();
		} else {
			Debug.LogError ("No TrajCalc Script attached to this object. Cannot continue. Disabling Script.");
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (anchored) {
			if (Input.GetMouseButton(MouseButton)) {
				AnchorDir = Vector3.Normalize (AnchorPos - Input.mousePosition);
				AnchorDist = Vector3.Distance (Input.mousePosition, AnchorPos);
				TCalc.Power = Mathf.Clamp (AnchorDist * InputToPower, 0f, MaxPower);
				TCalc.LaunchVector = AnchorDir;

				if (TrajDisplay != null) {
					Vector3[] lineLocs = TCalc.GetPath ();
					TrajDisplay.SetVertexCount (lineLocs.Length);
					for (int i = 0; i < lineLocs.Length; i++) {
						TrajDisplay.SetPosition (i, lineLocs [i]);
					}
				} else if (TrajDisplay != null)
					TrajDisplay.SetVertexCount(0);
				if (DEBUG) {
					Debug.DrawRay (transform.position, -AnchorDir * TCalc.Power, Color.red);
				}
			}
			if (Input.GetMouseButtonUp (MouseButton)) {
				if (TCalc.Power >= 0.5)
					FireObject ();
				if (TrajDisplay != null)
					TrajDisplay.SetVertexCount(0);
			}
		}
	}

	public void OnMouseDown()
	{
		//Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		//AnchorPos = ray.GetPoint (0.0f);
		//AnchorPos = Input.mousePosition;
		AnchorPos.z = Input.mousePosition.y;
		AnchorPos.x = Input.mousePosition.x;
		AnchorPos.y = Input.mousePosition.y;
		//AnchorPos = new Vector3(transform.position.x, 100 + transform.position.y, 100 + transform.position.z);
		anchored = true;
//		Debug.Log (AnchorPos.x);
	}
	public void FireObject()
	{
		AnchorPos = Vector3.zero;
		AnchorDir = Vector3.zero;
		AnchorDist = 0f;
		TCalc.Power = 0f;
		TCalc.LaunchVector = Vector3.zero;
		anchored = false;
		binMan = GameObject.FindGameObjectWithTag ("BinManager").GetComponent<BinManager> ();
		if (Projectile && Projectile.GetComponent<TrajProj>()) {
			//GameObject proj = Instantiate (Projectile, (transform.position + new Vector3(0, 200, -100)), Quaternion.identity) as GameObject;
			GameObject proj = binMan.ShootBin();
			proj.GetComponent<TrajProj> ().Init (TCalc.GetTrajectoryCloned ());
			binProp = proj.GetComponent<BinProperties> ();
			binProp.isLive = true;


		}
	}
}
