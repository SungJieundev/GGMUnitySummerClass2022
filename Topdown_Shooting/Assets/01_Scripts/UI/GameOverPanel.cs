using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{   

     public static GameOverPanel Instance = null;


    public GameObject _gameOverPanel;

    public TextMeshProUGUI _scoreText;

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("Multiple Gamemanger is running");
        }
        Instance = this;
    }

    public void GameOver()
    {

        Time.timeScale = 0;

        _gameOverPanel.SetActive(true);
        _scoreText.text = "Score : " + GameManager.Instance._score;

    }
}
