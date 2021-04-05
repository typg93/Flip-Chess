using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public Player turn = Player.Red;

    private void Start()
    {
        turn = Player.Red; // red goes first
    }
    public void EndTurn()
    {
        if (turn == Player.Red) turn = Player.Blue;
        else if (turn == Player.Blue) turn = Player.Red;
    }

    public Player PlayerTurn()
    {
        return turn;
    }
}

public enum Player
{
    Red = 1,
    Empty = 0,
    Blue = -1
}
