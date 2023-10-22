using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// This class is used for managing the UI.
/// It is attached to the UICanvas game object in the scene.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    #endregion
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    [SerializeField] private TextMeshProUGUI startMenuHighScoreUI;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.OnGameOver.AddListener(ActivateGameOverUI);  
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();
    }

    public void ActivateGameOverUI()
    {
        startMenuHighScoreUI.text = "Highscore: " + gm.PrettyScore(gm.Data.HighScore);
        gameOverUI.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore(gm.CurrentScore);
        if (gm.CurrentScore > gm.Data.HighScore)
        {
            gameOverHighScoreUI.text = "New Highscore!";
        }
        else
        {
            gameOverHighScoreUI.text = "Highscore: " + gm.PrettyScore(gm.Data.HighScore);
        }
    }

    public void UpdateStartMenuHighScore()
    {
        startMenuHighScoreUI.text = "Highscore: " + gm.PrettyScore(gm.Data.HighScore);
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore(gm.CurrentScore);
    }

}
