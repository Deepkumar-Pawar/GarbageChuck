using UnityEngine;
using System.Collections;

public class BinsConveyorBelt : MonoBehaviour {

	public float binSpawnInterval;
	public float binSpawnIntervalTime;
	public GameObject binObj;
	public Transform binSpawnPoint;

	public int binMissed = 0;
	// Use this for initialization
	void Awake () {
		binSpawnInterval = binSpawnIntervalTime;
	}
	
	// Update is called once per frame
	void Update () {
		binSpawnInterval -= Time.deltaTime;

		if (binSpawnInterval <= 0f) {
			Instantiate (binObj, binSpawnPoint.position, Quaternion.identity);
			binSpawnInterval = binSpawnIntervalTime;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.ToUpper() == "Bin".ToUpper()) {
			binMissed++;
			other.attachedRigidbody.useGravity = true;
			Destroy (other.gameObject, 0.5f);
			other.gameObject.GetComponent<BoxCollider> ().enabled = false;

		}
	}
}
