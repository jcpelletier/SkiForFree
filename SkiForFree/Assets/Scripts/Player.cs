using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private GameManager_V2 _gameManager;

    // Use this for initialization
    void Start () {
        _gameManager = GameManager_V2.Instance;
    }
	
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.tag == "Obstacle")
        {
            _gameManager.handleDeath();
        }
    }
}
