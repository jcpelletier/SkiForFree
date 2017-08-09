using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		public bool turn = false;
		public bool turnleft = false;
		public bool turnright = false;

        private CarController m_Car; // the car controller we want to use

		private void Update()
		{
			#if UNITY_ANDROID
			if (turn)
			{
				m_Car.Move(0.0f, 0, 0, 0f);
			}

			#endif
		}

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

		public void TurnLeft()
		{
			//float turn = 1;
			//m_Car.Move(-0.5f, 0, 0, 0f);
			turnleft = true;
			turnright = false;
		}

		public void TurnRight()
		{
			//m_Car.Move(0.5f, 0, 0, 0f);
			Debug.Log("Right!");
			turnright = true;
			turnleft = false;
		}

        private void FixedUpdate()
        {
			//turnleft = false;
			//turnright = false;
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
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
