using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		public Vector2 startPos;
		public Vector2 direction;
		public bool directionChosen;
		Rigidbody rb;
		public bool JumpReady = true;
		public int JumpStrength = 10;
        private CarController m_Car; // the car controller we want to use
		public Text debugText;
		public Text debugMagnitudeText;

		private void Start()
		{
			debugText.text = "DEBUG";
		}

        private void Awake()
        {
			
            // get the car controller
			JumpReady = true;
            m_Car = GetComponent<CarController>();
			rb = GetComponent<Rigidbody>();
        }

		public void TurnLeft()
		{

		}

		public void TurnRight()
		{

		}

		void OnCollisionEnter(Collision collision)
		{
			JumpReady = true;
		}

		private void Update()
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				// Handle finger movements based on touch phase.
				switch (touch.phase)
				{
				// Record initial touch position.
				case TouchPhase.Began:
					startPos = touch.position;
					directionChosen = false;
					break;

					// Determine direction by comparing the current touch position with the initial one.
				case TouchPhase.Moved:
					direction = touch.position - startPos;
					break;

					// Report that a direction has been chosen when the finger is lifted.
				case TouchPhase.Ended:
					directionChosen = true;
					break;
				}
			}

			if (directionChosen && (direction.sqrMagnitude > 100000.0f))
			{ 
				int directionX = Convert.ToInt32(direction[0]);
				int directionY = Convert.ToInt32(direction[1]);

				debugMagnitudeText.text = direction.sqrMagnitude.ToString ();
				debugText.text = direction.ToString();

				if (directionX < 0) 
				{
					JumpSpinLeft();
				}else if (directionX > 0)
				{
					JumpSpinRight();
				}
				directionChosen = false;
			}
		}

		private void JumpSpinRight()
		{
			rb.AddForce (transform.up * JumpStrength *5, ForceMode.Impulse);
			rb.AddTorque (transform.up * JumpStrength *5, ForceMode.Impulse);
			Debug.Log ("JumpSpinRight");
			//JumpReady = false;
		}

		private void JumpSpinLeft()
		{
			rb.AddForce (transform.up * JumpStrength *5, ForceMode.Impulse);
			rb.AddTorque (-transform.up * JumpStrength * 5, ForceMode.Impulse);
			Debug.Log ("JumpSpinLeft");
			//JumpReady = false;
		}

        private void FixedUpdate()
        {
			//turnleft = false;
			//turnright = false;
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			if ((Input.touchCount > 1 || Input.GetKey("space")) && JumpReady) 
			{
				rb.AddForce (transform.up * JumpStrength, ForceMode.Impulse);
				Debug.Log ("Jump");
				//JumpReady = false;
			}

			if (Input.touchCount > 0) 
			{ // touch controls

				Touch touch = Input.GetTouch (0);
				if (touch.position.x < Screen.width / 2) {
					m_Car.Move (-0.5f, 0, 0, 0f);// turn left
				} else if (touch.position.x > Screen.width / 2) {
					m_Car.Move (0.5f, 0, 0, 0f);// turn right
				} 

				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) 
				{
					m_Car.Move (0.0f, 0, 0, 0f); // set car wheel back to neutral position
				}


				
			}



#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            
#else
            //m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
