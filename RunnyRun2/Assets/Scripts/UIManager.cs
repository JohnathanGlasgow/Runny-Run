using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUi;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI gameOverHighScoreUI;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(ActivateGameOverUI);  
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();

    }

    public void ActivateGameOverUI()
    {
        gameOverUi.SetActive(true);
        gameOverScoreUI.text = "Score: " + gm.PrettyScore(gm.currentScore);
        if (gm.currentScore > gm.data.highScore)
        {
            gameOverHighScoreUI.text = "New Highscore!";
        }
        else
        {
        gameOverHighScoreUI.text = "Highscore: " + gm.PrettyScore(gm.data.highScore);
        }
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore(gm.currentScore);
    }
}
