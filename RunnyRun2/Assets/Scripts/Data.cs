using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used for storing any persistent data that needs to be saved between game sessions.
/// This class is used in conjunction with the SaveSystem class.
/// This save system is not currently used in the game, but is left in for future use.
/// </summary>
[System.Serializable]
public class Data
{
    public float HighScore;
}
