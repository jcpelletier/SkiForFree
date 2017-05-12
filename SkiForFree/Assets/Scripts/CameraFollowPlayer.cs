using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform player;

    private Vector3 _cameraOffSet = new Vector3(-10, 25, 0);
	
	// Update is called once per frame
	void Update () {
        this.transform.localPosition = player.transform.localPosition + _cameraOffSet;
	}
}
