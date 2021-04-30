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
    public GameObject computer;
    public bool canMove = true;
    private AIPlayer ai;

    public event EventHandler<OnEndTurnArgs> OnEndTurn;
    public class OnEndTurnArgs : EventArgs
    {
        public Player newTurn = Player.Red;
    }

    private void Start()
    {
        turn = Player.Red; // red goes first
        ai = computer.GetComponent<AIPlayer>();
    }

    public void EndTurn()
    {
        if (turn == Player.Red)
        {
            TextInfo.GetComponent<TextMeshProUGUI>().text = "Blue Turn";
            turn = Player.Blue;
            StartCoroutine(ai.FinishMove());
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
        canMove = false;
        Debug.Log(player + "wins");
    }

    public void NewGame()
        //resets game
    {
        canMove = true;
        turn = Player.Red;
    }
}

