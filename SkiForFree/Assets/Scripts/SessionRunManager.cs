using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SessionRunManager : MonoBehaviour {
	private float sessionTime;
	private float sessionTimeInt;
	private string sessionTimeString;
	private float distanceNumber;
	private string distanceNumberString;
	public Text sessionTimeText;
	public Text distanceNumberText;

	// Use this for initialization
	void Start () 
	{
		sessionTime = 0.0f;
		//sessionTimeString = "Time: ";
		distanceNumber = 0.0f;
		//distanceNumberString = "Distance: ";
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
	// Update is called once per frame
	void Update () 
	{
		sessionTime = sessionTime + Time.deltaTime;
		sessionTimeInt = (int)sessionTime;
		sessionTimeString = ("Time: " + sessionTimeInt.ToString ());
		sessionTimeText.text = sessionTimeString;
		//distanceNumber = distanceNumber + Time.deltaTime;
		//distanceNumberString = "Distance: " + distanceNumber.ToString ();
		//distanceNumberText.text = distanceNumberString;
	}
}
