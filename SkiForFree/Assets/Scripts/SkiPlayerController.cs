using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiPlayerController : MonoBehaviour {
	public Rigidbody rb;
	public int TurnStrength = 100;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey("a"))
		{
			rb.AddForce (transform.right * -TurnStrength);
			//Debug.Log ("go left");
		}

		if (Input.GetKey("d"))
		{
			rb.AddForce (transform.right * TurnStrength);
		}

		if (Input.GetKey("w"))
		{
			rb.AddForce (transform.forward * TurnStrength);
		}
		if (Input.GetKey("s"))
		{
			rb.AddForce (transform.forward * -1 * TurnStrength / 10);
		}
	}
}
