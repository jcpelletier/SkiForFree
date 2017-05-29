using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_V2 : MonoBehaviour {
    public Transform player;

    public Canvas deathMenu;

    public Text ScoreText;
    float ScoreFloat = 0.0f;

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

    void FixedUpdate()
    {
        ScoreFloat = ScoreFloat + Time.deltaTime;
        ScoreText.text = ((int)ScoreFloat).ToString();
    }
    
    public void handleDeath()
    {
        deathMenu.enabled = true;

        Transform scoreVal = deathMenu.transform.Find("ScoreVal");
        Text scoreValText = scoreVal.GetComponent<Text>();
        scoreValText.text = ScoreText.text;
    }
}
