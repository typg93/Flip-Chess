using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public int value = 5;
    private bool turn = true; //red is true

    public void EndTurn()
    {
        turn = !turn;
    }
}
