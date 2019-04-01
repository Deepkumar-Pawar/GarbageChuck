using UnityEngine;
using System.Collections;

[System.Serializable]
public class Trajectory{
	public Vector3[] Locations = new Vector3[0];
	public Vector3[] Directions = new Vector3[0];
}

public class TrajCalc : MonoBehaviour {

	public LayerMask CollisionMask = new LayerMask ();
	public int Points = 30;
	public float Power = 0f;
	public float Drag = 0.05f;
	public Vector3 LaunchVector = Vector3.zero;
	public float Gravity = 0.125f;
	public bool DEBUG = false;

	Trajectory CurrentTrajectory = new Trajectory();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Points = Mathf.Clamp (Points, 0, 128);
		LaunchVector = Vector3.Normalize (LaunchVector);

		if (Power > 0f && Points > 2) {

			Vector3 tempDir = LaunchVector;
			CurrentTrajectory.Directions = new Vector3[Points];
			CurrentTrajectory.Directions [0] = tempDir;

			Vector3 tempLoc = transform.position;
			CurrentTrajectory.Locations = new Vector3[Points];
			CurrentTrajectory.Locations[0] = tempLoc;

			float tempPower = Power;

			if (DEBUG)
				Debug.DrawRay (transform.position, Power * LaunchVector, Color.yellow);

			for (int i = 1; i < CurrentTrajectory.Locations.Length; i++) {

				tempPower = Mathf.Clamp ((tempPower - Drag), 0f, Mathf.Infinity);

				tempLoc += (tempDir * tempPower) + (Vector3.down * Gravity);
				CurrentTrajectory.Locations [i] = tempLoc;

				tempDir = Vector3.Normalize (tempLoc - CurrentTrajectory.Locations [i - 1]);
				CurrentTrajectory.Directions [i] = tempDir;
			}

			CurrentTrajectory = CheckForCollision (CurrentTrajectory);

			if (DEBUG) {
				for (int i = 0; i < CurrentTrajectory.Locations.Length-1; i++) {
					Debug.DrawLine (CurrentTrajectory.Locations [i], CurrentTrajectory.Locations [i + 1], Color.cyan);
					Debug.DrawRay (CurrentTrajectory.Locations [i], CurrentTrajectory.Directions [i] * Power, Color.magenta);
				}
			}
		}
	}

	public Trajectory CheckForCollision(Trajectory traj)
	{
		bool archHit = false;
		RaycastHit hit = new RaycastHit ();
		int cullListAt = 0;

		for (int i = 0; i < traj.Locations.Length-1 && !archHit; i++) {
			if (Physics.Linecast(traj.Locations[i], traj.Locations[i+1], out hit, CollisionMask)) {
				cullListAt = i + 1;
				archHit = true;
				if(DEBUG){
					Debug.DrawLine(traj.Locations[i], hit.point, Color.white);
				}
			}
		}
		if (archHit)
		{
			Vector3[] temp = new Vector3[cullListAt];
			for (int i = 0; i < temp.Length; i++) {
				temp[i] = traj.Locations[i];
			}
			
			traj.Locations = temp;
			traj.Locations[traj.Locations.Length-1] = hit.point;
			
			temp = new Vector3[cullListAt];
			for (int i = 0; i < temp.Length; i++) {
				temp[i] = traj.Directions[i];
			}
			traj.Directions = temp;
		}
		return traj;
	}
	
	public Trajectory GetTrajectory()
	{
		return CurrentTrajectory;
	}

	public Trajectory GetTrajectoryCloned()
	{
		Trajectory clone = new Trajectory ();
		clone.Directions = new Vector3[CurrentTrajectory.Directions.Length];
		clone.Locations = new Vector3[CurrentTrajectory.Locations.Length];
		for (int i = 0; i < CurrentTrajectory.Locations.Length; i++) {
			clone.Locations [i] = CurrentTrajectory.Locations [i];
		}
		for (int i = 0; i < CurrentTrajectory.Directions.Length; i++) {
			clone.Directions [i] = CurrentTrajectory.Directions [i];
		}
		//clone.Locations =(Vector3[]) CurrentTrajectory.Locations.Clone();
		//clone.Directions =(Vector3[]) CurrentTrajectory.Directions.Clone();


		return clone;
	}

	public Vector3[] GetPath()
	{
		return CurrentTrajectory.Locations;
	}
	
	public Vector3[] GetTrajs()
	{
		return CurrentTrajectory.Directions;
	}
}