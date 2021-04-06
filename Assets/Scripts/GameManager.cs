using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player turn = Player.Red;

    public event EventHandler<OnEndTurnArgs> OnEndTurn;
    public class OnEndTurnArgs : EventArgs
    {
        public Player newTurn = Player.Red;
    }

    private void Start()
    {
        turn = Player.Red; // red goes first
    }

    public void EndTurn()
    {
        if (turn == Player.Red) turn = Player.Blue;
        else if (turn == Player.Blue) turn = Player.Red;
        OnEndTurnArgs e = new OnEndTurnArgs { newTurn = this.turn };
        OnEndTurn?.Invoke(this, e);
    }

    public Player PlayerTurn()
    {
        return turn;
    }

    public void WinGame(Player player)
    {
        //win game
        Debug.Log(player + "wins");
    }
}

public enum Player
{
    Red = 1,
    Empty = 0,
    Blue = -1
}
