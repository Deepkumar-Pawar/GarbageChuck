using UnityEngine;
using System.Collections;

public class TruckMove : MonoBehaviour {

	public float speed;
//	public Transform truckSpawnPoint;
	public Vector3 leftLimit;
	public Vector3 rightLimit;
	public GameObject truckFR;
	public GameObject truckFL;

	private TruckHitScript truckHitRR;
	private TruckHitScript truckHitRL;
	private TruckHitScript truckHitLR;
	private TruckHitScript truckHitLL;
	private bool facingRight;

	// Use this for initialization
	void Awake() {
		facingRight = true;
		truckFR.SetActive (true);
		truckFL.SetActive (false);
		gameObject.transform.position = leftLimit;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (facingRight) {
			truckHitRR = GameObject.Find ("TrailerShowRR").GetComponent<TruckHitScript> ();
			truckHitRL = GameObject.Find ("TrailerShowRL").GetComponent<TruckHitScript> ();

			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
		else if (!facingRight) {
			truckHitLR = GameObject.Find ("TrailerShowLR").GetComponent<TruckHitScript> ();
			truckHitLL = GameObject.Find ("TrailerShowLL").GetComponent<TruckHitScript> ();

			transform.Translate (Vector3.left * speed * Time.deltaTime);
		}

		if (transform.position.x >= rightLimit.x || transform.position.x <= leftLimit.x) {
				Flip ();
		}
		Mathf.Clamp (transform.position.x, leftLimit.x, rightLimit.x);
	}
	void Flip()
	{
		if (facingRight) {
			facingRight = false;

			truckFL.SetActive (true);
			truckFR.SetActive (false);
		}
		else if (!facingRight) {
			facingRight = true;

			truckFR.SetActive (true);
			truckFL.SetActive (false);
		}
	}
}
