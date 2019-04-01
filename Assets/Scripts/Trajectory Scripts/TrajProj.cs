using UnityEngine;
using System.Collections;

public class TrajProj : MonoBehaviour {

	public float Speed = 4f;
	public float PathSmoothing = 0.25f;
	public LayerMask CollisionMask = new LayerMask ();
	public GameObject Splosion;
	public float LifePastPath = 3f;

	Trajectory traj;
	int currentDest = 0;
	Vector3 lastPos = Vector3.zero;
	bool onPath = true;
	float timer = 0f;

	public void Init (Trajectory newTraj){
		traj = new Trajectory ();
		traj.Locations = newTraj.Locations;
		traj.Directions = newTraj.Directions;

		transform.forward = traj.Directions [0];
		transform.position = traj.Locations [0];
		lastPos = transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<BinProperties> ().isLive) {
			if (onPath) {
				if (traj != null && traj.Locations.Length > 0 && currentDest < traj.Locations.Length) {
					transform.position = Vector3.MoveTowards (transform.position, traj.Locations [currentDest], (Time.deltaTime * Time.timeScale) * Speed);
					transform.forward = Vector3.MoveTowards (transform.forward, traj.Directions [currentDest], (Time.deltaTime * Time.timeScale) * Speed);
					if (transform.position == traj.Locations [currentDest])
						currentDest++;
					else if (currentDest > 0) {
						float legDist = Vector3.Distance (traj.Locations [currentDest], traj.Locations [currentDest - 1]);
						if (Vector3.Distance (transform.position, traj.Locations [currentDest]) <= PathSmoothing * legDist)
							currentDest++;
					}
				} else
					onPath = false;
			} else {
				transform.position += transform.forward * (Speed * (Time.deltaTime * Time.timeScale));
				timer += (Time.deltaTime * Time.timeScale);
				if (timer >= LifePastPath)
					Kill (false);
			}
			CollisionCheck ();
		}
	}

	public void CollisionCheck()
	{
		
		RaycastHit hit;
		if (Physics.Linecast(lastPos, transform.position, out hit, CollisionMask)) {
			transform.position = hit.point;
			//Kill (true);
		}
		lastPos = transform.position;
	}

	public void Kill(bool explode = true)
	{
		if (explode && Splosion) {
			GameObject splosion = Instantiate (Splosion, transform.position, Quaternion.identity) as GameObject;
			splosion.transform.up = -transform.forward;
		}
//		Debug.Log ("Dead");
		Destroy (gameObject);
	}
}
