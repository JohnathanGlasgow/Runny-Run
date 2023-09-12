using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUi;
    [SerializeField] private GameObject gameOverUi;
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
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }
}
