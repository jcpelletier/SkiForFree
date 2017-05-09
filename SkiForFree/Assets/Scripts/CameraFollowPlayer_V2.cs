using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer_V2 : MonoBehaviour {
    public Transform player;

    private Vector3 _cameraOffSet = new Vector3(0, 30, -20);

    void Update()
    {
        this.transform.localPosition = player.transform.localPosition + _cameraOffSet;
    }
}
