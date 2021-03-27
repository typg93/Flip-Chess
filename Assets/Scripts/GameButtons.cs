using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtons : Button
{
    public void FlipAllPiecesUp()
    {
        foreach (GameObject cellGM in board.cells)
        {
            Cell cell = cellGM.GetComponent<Cell>();
            cell.ChangeValue(false);
        }
    }

    public void ResetBoard()
    {
        foreach (GameObject cellGM in board.cells)
        {
            Cell cell = cellGM.GetComponent<Cell>();
            cell.ChangeValue(0, true);
        }

        // to do
    }
}
