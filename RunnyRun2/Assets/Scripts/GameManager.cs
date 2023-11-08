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
    [SerializeField] private GameObject player;
    public float CurrentScore;
    public Data Data;
    public bool IsPlaying = false;
    public UnityEvent OnGameOver;
    public UnityEvent OnPlay;
    public int DifficultyTier = 1;

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
            if (CurrentScore > 0)
            {
                DifficultyTier = Mathf.FloorToInt(CurrentScore / 10) + 1;
            }
        }
    }
    #endregion

    public void GameOver()
    {
        OnGameOver.Invoke();
        if (CurrentScore > Data.HighScore)
        {
            Data.HighScore = CurrentScore;
            SaveSystem.Save("save", Data);
        }
        IsPlaying = false;

    }

    public void StartGame()
    {
        player.SetActive(true);
        OnPlay.Invoke();
        IsPlaying = true;
        CurrentScore = 0;
        // enable player collider
        player.GetComponent<Collider2D>().enabled = true;
        animator.SetBool("IsRunning", true);
    }

    public string PrettyScore(float score) => Mathf.RoundToInt(score).ToString();
}

