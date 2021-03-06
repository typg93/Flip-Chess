using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : Button
{
    public void FlipAllPiecesUp()
    {
        
        foreach (Cell cell in board.cells)
        {
            if (cell.GetValue() != CellValue.Empty)
            {
                cell.ChangeValue(false);
            }
        }
    }

    public void ResetBoard()
    {
        board.ClearBoard();
        board.RandomizeCellValues();
        GameManager.instance.NewGame();
    }

    public void ResetBoardTen()
    {
        for(int i = 0; i< 5; i++)
        {
            Invoke("ResetBoard", i*0.1f);
        }
        Invoke("FlipAllPiecesUp", 0.6f);
    }
}
