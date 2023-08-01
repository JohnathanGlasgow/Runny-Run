using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decrement : MonoBehaviour
{
    public void DecrementBrainCells()
    {
        GameManager.BrainCells--;
        // log the new value of BrainCells to the console
        Debug.Log(GameManager.BrainCells);
    }

}
