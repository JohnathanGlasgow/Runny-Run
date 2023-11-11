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

    public GameObject Player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion

    public float CurrentScore;
    public bool IsPlaying = false;
    public UnityEvent OnGameOver;
    public UnityEvent OnPlay;
    public int DifficultyTier { get; private set; } = 1;
    public int DifficultyThreshold { get; private set; } = 10;
    public int HighScore { get; private set; }
    private UIManager uiManager;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;

    private void Start()
    {
        uiManager = UIManager.Instance;
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        uiManager.UpdateStartMenuHighScore(PrettyScore(HighScore));
    }

    private void Update()
    {
        if (IsPlaying)
        {
            // Update difficulty tier if threshold has been reached
            if (CurrentScore > 0)
            {
                DifficultyTier = Mathf.FloorToInt(CurrentScore / DifficultyThreshold) + 1;
            }
        }
    }

    /// <summary>
    /// This method is called when the player dies.
    /// It invokes the OnGameOver event.
    /// </summary>
    public void GameOver()
    {
        OnGameOver.Invoke();
        // Update high score if current score is higher
        if (CurrentScore > HighScore)
        {
            HighScore = Mathf.RoundToInt(CurrentScore);
            PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(CurrentScore));
        }
        IsPlaying = false;
        DifficultyTier = 1;
    }

    /// <summary>
    /// This method is called when the player starts the game.
    /// It invokes the OnPlay event.
    /// </summary>
    public void StartGame()
    {
        player.SetActive(true);
        OnPlay.Invoke();
        IsPlaying = true;
        CurrentScore = 0;
        // Reenable player collider
        player.GetComponent<Collider2D>().enabled = true;
        animator.SetBool("IsRunning", true);
    }

    /// <summary>
    /// This method returns a string representation of the score.
    /// </summary>
    /// <param name="score">The score to be converted to a string.</param>
    /// <returns>A string representation of the score.</returns>
    public string PrettyScore(float score) => Mathf.RoundToInt(score).ToString();
}

