using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_V2 : MonoBehaviour {
    public Transform player;

    private static GameManager_V2 _instance;
    public static GameManager_V2 Instance { get { return _instance; } }

    // Instantiate singleton pattern --
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    
}
