using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkiPlayerController : MonoBehaviour {
	Rigidbody rb;
    
	public int TurnStrength;
	public int RollStrength;
	public int ForwardStrength;
	public int JumpStrength;

	bool JumpReady;
	// Use this for initialization
	void Start () {
		JumpReady = true;
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision collision)
	{
		JumpReady = true;
	}
	// Update is called once per frame
	void FixedUpdate () {
		// Controls
		if (Input.GetKey("a"))
		{
			rb.AddTorque (transform.up * - RollStrength);
			rb.AddForce (transform.right * -TurnStrength);
			Debug.Log ("go left");
		}

		if (Input.GetKey("d"))
		{
			rb.AddTorque (transform.up * RollStrength);
			rb.AddForce (transform.right * TurnStrength);
		}

		if (Input.GetKey("w"))
		{
			
			rb.AddForce (transform.forward * ForwardStrength);
		}
		if (Input.GetKey("s"))
		{
			rb.AddForce (transform.forward * -1 * TurnStrength / 10);
		}

		if (Input.GetKey("space") && JumpReady == true)
		{
			JumpReady = false;
			rb.AddForce (transform.up * JumpStrength, ForceMode.Impulse);
		}

	}
}
