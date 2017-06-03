using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
		public float turn = 1;
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

		public void TurnLeft()
		{
			//float turn = 1;
			m_Car.Move(-0.5f, 0, 0, 0f);
		}

		public void TurnRight()
		{
			//float turn = -1;
			m_Car.Move(0.5f, 0, 0, 0f);
		}

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            
#else
            //m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
