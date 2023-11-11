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
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    public float CurrentScore;
    public bool IsPlaying = false;
    public UnityEvent OnGameOver;
    public UnityEvent OnPlay;
    public int DifficultyTier = 1;
    public int HighScore;
    private UIManager uiManager;

    # region MonoBehaviour
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
        if (CurrentScore > HighScore)
        {
            HighScore = Mathf.RoundToInt(CurrentScore);
            PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(CurrentScore));
        }
        IsPlaying = false;
        DifficultyTier = 1;

        // Fix Player at 0
        // player.transform.position = new Vector3(0, 0, 0);

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

