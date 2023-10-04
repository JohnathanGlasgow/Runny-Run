using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public float currentScore = 0f;
    public bool isPlaying = false;
    public UnityEvent onPlay;
    public UnityEvent onGameOver;
    public Data data;

    private void Start()
    {
        string loadedData = SaveSystem.Load("save");
        // data = new Data();

        data = (loadedData != null) ? JsonUtility.FromJson<Data>(loadedData) : new Data();
    }
    private void Update()
    {
        if (isPlaying)
        {
            currentScore += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        currentScore = 0;
    }   

    public void GameOver()
    {
        
        if (currentScore > data.highScore)
        {
            data.highScore = currentScore;
            SaveSystem.Save("save", data);
        }
        isPlaying = false;
        onGameOver.Invoke();
    }
    public string PrettyScore(float score) => Mathf.RoundToInt(score).ToString();

}

