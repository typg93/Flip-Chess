using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameButtons : Button
{
    public void FlipAllPiecesUp()
    {
        
        foreach (GameObject cellGM in board.cellsGO)
        {
            Cell cell = cellGM.GetComponent<Cell>();
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
    }

    public void ResetBoardTen()
    {
        for(int i = 0; i< 5; i++)
        {
            Invoke("ResetBoard", i*0.1f);
        }
        Invoke("FlipAllPiecesUp", 1.1f);
    }
}
