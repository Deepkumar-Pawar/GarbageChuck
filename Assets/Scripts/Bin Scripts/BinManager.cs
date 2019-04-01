using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BinManager : MonoBehaviour {

	private ArrayList binsList = new ArrayList();
	public int maxBinNum = 5;
	public GameObject wetBin;
	public GameObject dryBin;
	public Transform binSpawn;
	public int horizontalSeperation = 50;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < maxBinNum; i++) {
			if (Random.Range (0, 1000) % 2 == 0) {
				binsList.Add (Instantiate (wetBin, binSpawn.position + new Vector3 (i * horizontalSeperation, 0, 0), Quaternion.identity));
			} else {
				binsList.Add (Instantiate (dryBin, binSpawn.position + new Vector3 (i * horizontalSeperation, 0, 0), Quaternion.identity));
			}
		}
	}
	public GameObject ShootBin()
	{
		GameObject result = binsList [0] as GameObject;
		GameObject bin;
		binsList.RemoveAt (0);
		if (Random.Range (0, 1000) % 2 == 0) {
			bin = Instantiate (wetBin, binSpawn.position + new Vector3 ((maxBinNum) * horizontalSeperation, 0, 0), Quaternion.identity) as GameObject;

			binsList.Add (bin);
		} else {
			bin = Instantiate (dryBin, binSpawn.position + new Vector3 ((maxBinNum) * horizontalSeperation, 0, 0), Quaternion.identity) as GameObject;

			binsList.Add (bin);
		}
		for (int i = 0; i < maxBinNum; i++) {
			Vector3 currentPos = ((GameObject)binsList [i]).transform.position;
			Vector3 newPos = new Vector3 (currentPos.x - horizontalSeperation, currentPos.y, currentPos.z);

			((GameObject)binsList [i]).transform.position = newPos;
		}
		return result;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
