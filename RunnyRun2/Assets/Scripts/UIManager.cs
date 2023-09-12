using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private GameObject startMenuUi;
    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    public void PlayButtonHandler()
    {
        gm.StartGame();
        startMenuUi.SetActive(false);
    }

    private void OnGUI()
    {
        scoreUI.text = gm.PrettyScore();
    }
}
