using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float _currentTime = 0;
    private int _maxTime = 100;
    private int _lastTime;
    
    private void Update() {

        TimeCount();
        text.text = "남은 시간 : " + _lastTime;
    }

    private void TimeCount(){
        _currentTime += Time.deltaTime;
        _lastTime = _maxTime - (int)_currentTime;

        text.text = "남은 시간 : " + _lastTime;

        
        if(_lastTime == 0){
            GameOverPanel.Instance.GameOver();
        }
    }
}
