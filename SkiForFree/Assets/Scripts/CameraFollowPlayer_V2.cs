using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer_V2 : MonoBehaviour {
    public Transform player;

    private Vector3 _cameraOffSet = new Vector3(0, 30, -20);
    private Vector3 _cameraOffSet2 = new Vector3(0, 0, 0);
    private Vector3 _cameraOffSet3 = new Vector3(0, 250, 750);

    private Quaternion _startRotation;
    private Quaternion _bigViewRotation = new Quaternion(0, -90, 0, 0);

    private float startFieldOfView = 38;
    private float clippPlaneFar = 1000;

    private float startFieldOfView1 = 100;
    private float clippPlaneFar1 = 2000;

    private Camera _blahpoop;

    void Start()
    {
        _startRotation = this.transform.rotation;
        _blahpoop = this.transform.GetComponent<Camera>();
    }

    void Update()
    {
        // TEMPORARY
        if (Input.GetKey(KeyCode.LeftControl))
        {
            this.transform.localPosition = player.transform.localPosition + _cameraOffSet2;
        } else if (Input.GetKey(KeyCode.Z))
        {
            _blahpoop.farClipPlane = clippPlaneFar1;
            _blahpoop.fieldOfView = startFieldOfView1;
            this.transform.rotation = _bigViewRotation;
            this.transform.localPosition = player.transform.localPosition + _cameraOffSet3;
        } else
        {
            _blahpoop.fieldOfView = startFieldOfView;
            _blahpoop.farClipPlane = clippPlaneFar;
            this.transform.localPosition = player.transform.localPosition + _cameraOffSet;
            this.transform.rotation = _startRotation;
        }
    }
}
