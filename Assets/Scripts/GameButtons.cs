using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : Button
{
    public void FlipAllPiecesUp()
    {
        //test
        
        foreach (GameObject cellGM in board.cells)
        {
            Cell cell = cellGM.GetComponent<Cell>();
            if(cell.)
            cell.ChangeValue(false);
        }
    }

    public void ResetBoard()
    {
        board.ClearBoard();
        board.RandomizeCellValues();
    }
}
