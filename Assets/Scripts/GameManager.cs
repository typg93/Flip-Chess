using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Player turn = Player.Red;
    public GameObject TextInfo;

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
        if (turn == Player.Red)
        {
            TextInfo.GetComponent<TextMeshProUGUI>().text = "Blue Turn";
            turn = Player.Blue;

            //OnEndTurnArgs e = new OnEndTurnArgs { newTurn = this.turn };
            //OnEndTurn?.Invoke(this, e);
        }

        else if (turn == Player.Blue)
        {
            TextInfo.GetComponent<TextMeshProUGUI>().text = "Red Turn";
            turn = Player.Red;
        }
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

