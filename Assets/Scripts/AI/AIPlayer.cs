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
    
    List<AICellData[]> GenerateMoves(AICellData[] curBoard)
    {
        //to do
        List<AICellData[]> possibleBoards = new List<AICellData[]>();

        for (int index = 0; index < curBoard.Length; index++)
        {

        }

        return possibleBoards;
    }

    void MakeMove(AICellData)

    bool ValidMove(AICellData start, AICellData end)
    {
        if (Math.Abs(start.position.x - end.position.x) != 1)
        {
            return false;
        }

        if (Math.Abs(start.position.y - end.position.y) != 1)
        {

        }

        return false;
    }

    public struct AICellData
    {
        public int value;
        public bool faceup;
        public Player player;
        public Vector2 position;
    }
}
