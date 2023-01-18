using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public List<GameObject> poolingList = new List<GameObject>();

    private bool isGameOver = false; //���� ������ �Ǿ����� �ƴ���
    public bool IsGameOver
    {
        set
        {
            isGameOver = value;
        }
        get
        {
            return isGameOver;
        }
    }
    public Text scoreText;
    public GameObject gameOverObject;
    private float score = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("�ΰ��� ���ӸŴ����� �����ϰ� �ֽ��ϴ�.");
        }
        Instance = this;

        PoolManager.instance = gameObject.AddComponent<PoolManager>();
        foreach(GameObject item in poolingList)
        {
            PoolManager.instance.CreatePool(item, transform, 10);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && isGameOver == true) //��Ŭ���� ����� ���ӿ��� �����̸�
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //���� �� ��ε�
        }
    }

    public void AddScore(int newScore)
    {
        if(isGameOver == false)
        {
            score += newScore;
            scoreText.text = "Score : " + score; 
        }
    }

    public void OnPlayerDead()
    {
        isGameOver = true;
        gameOverObject.SetActive(true);
    }
}
