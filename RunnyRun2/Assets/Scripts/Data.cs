using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for storing any persistent data that needs to be saved between game sessions.
/// This class is used in conjunction with the SaveSystem class.
/// Currently it only stores the high score.
/// </summary>
[System.Serializable]
public class Data
{
    public float HighScore;
}
