using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {

		Rigidbody rb;
		public bool JumpReady = true;
		public int JumpStrength = 10;
        private CarController m_Car; // the car controller we want to use

		private void Update()
		{

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
					m_Car.Move (-0.5f, 0, 0, 0f);
				} else if (touch.position.x > Screen.width / 2) {
					m_Car.Move (0.5f, 0, 0, 0f);
				} 

				if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended) 
				{
					m_Car.Move (0.0f, 0, 0, 0f);
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
