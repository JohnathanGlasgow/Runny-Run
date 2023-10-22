using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for managing the game state.
/// It is a singleton class, meaning that there can only be one instance of it.
/// It is used in conjunction with the UIManager class.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion
    [SerializeField] private Animator animator;
    public float CurrentScore;
    public Data Data;
    public bool IsPlaying = false;
    public UnityEvent OnGameOver;
    public UnityEvent OnPlay;

    private UIManager uiManager;

    # region MonoBehaviour
    private void Start()
    {
        uiManager = UIManager.Instance;
        string loadedData = SaveSystem.Load("save");
        Data = (loadedData != null) ? JsonUtility.FromJson<Data>(loadedData) : new Data();
        uiManager.UpdateStartMenuHighScore();
    }

    private void Update()
    {
        if (IsPlaying)
        {
            CurrentScore += Time.deltaTime;
        }
    }
    #endregion

    public void GameOver()
    {
        if (CurrentScore > Data.HighScore)
        {
            Data.HighScore = CurrentScore;
            SaveSystem.Save("save", Data);
        }
        IsPlaying = false;
        OnGameOver.Invoke();
    }

    public void StartGame()
    {
        OnPlay.Invoke();
        IsPlaying = true;
        CurrentScore = 0;
        animator.SetBool("IsRunning", true);
        // log out the animator bool
        Debug.Log("animator.IsRunning: " + animator.GetBool("IsRunning"));
    }   

    public string PrettyScore(float score) => Mathf.RoundToInt(score).ToString();
}

