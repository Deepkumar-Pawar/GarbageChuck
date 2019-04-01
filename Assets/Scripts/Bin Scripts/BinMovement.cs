using UnityEngine;
using System.Collections;

public class BinMovement : MonoBehaviour {

	public float speed;

	[HideInInspector]
	public bool isUsed = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!isUsed) {
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
	}
}
