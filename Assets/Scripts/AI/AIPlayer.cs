using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public Board board;
    private int boardX;
    private int boardY;

    private void Awake()
    {
        boardX = board.boardX;
        boardY = board.boardY;
    }



    public AICellData[] ScanBoard()
        //scans current board and flattens cell data into an array
    {
        AICellData[] flattenedCellArray = new AICellData[32];
        for (int y = 0; y < boardY; y++)
        {
            for (int x = 0; x < boardX; x++)
            {
                int index = y * boardX + x;
                Cell cell = board.cells[x, y];
                flattenedCellArray[index].value = (int)cell.GetValue();
                flattenedCellArray[index].player = cell.GetColor();
                flattenedCellArray[index].faceup = cell.GetFlipState();
                flattenedCellArray[index].position = cell.GetCoordinate();
            }
        }
        return flattenedCellArray;
    }


}

public struct AICellData
{
    public int value;
    public bool faceup;
    public Player player;
    public Vector2 position;

    public void Reset()
    {
        (value, faceup, player) = (0, true, Player.Empty);
    }
}