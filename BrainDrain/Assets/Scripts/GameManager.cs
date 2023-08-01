using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // set the value of BrainCells to a string from player prefs and convert  it to a long
    // or set to 86 billion if no value exists
    public static long BrainCells;
    public TMP_Text BrainCellText;

    void Awake()
    {
        BrainCells = PlayerPrefs.HasKey("BrainCells") ? long.Parse(PlayerPrefs.GetString("BrainCells")) : 86000000000L;
        // log the value of BrainCells to the console
        Debug.Log(BrainCells);
    }

    // Update is called once per frame
    void Update()
    {
        BrainCellText.text = BrainCells.ToString();
        // save the value of BrainCells to player prefs
        PlayerPrefs.SetString("BrainCells", BrainCells.ToString());

        // check if r key is pressed and if so reset BrainCells to 86 billion
        if (Input.GetKeyDown(KeyCode.R))
        {
            BrainCells = 86000000000L;
        }
    }
}
