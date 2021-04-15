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
                flattenedCellArray[y * boardX + x].value = (int)board.cells[x, y].GetValue();
            }
        }
        return flattenedCellArray;
    }

    public struct AICellData
    {
        public int value;
        public bool faceup;
        public Player player;
    }
}
