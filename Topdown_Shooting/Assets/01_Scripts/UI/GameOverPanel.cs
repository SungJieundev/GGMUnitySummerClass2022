using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{   

     public static GameOverPanel Instance = null;

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

        GameManager.Instance._gameOverPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.GameRestart();
        }
    }
}
