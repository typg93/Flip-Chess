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
            if(index + boardX <= curBoard.Length && ValidMove(curBoard[index], curBoard[index+boardX]))
            {
                //resolvemove top
                possibleBoards.Add(ResolveMove(index, index + boardX));
            }
            if (index - boardX >= 0 && ValidMove(curBoard[index], curBoard[index - boardX]))
            {
                //resolvemove btm
                possibleBoards.Add(ResolveMove(index, index - boardX));
            }
            if (index + 1 <= curBoard.Length && ValidMove(curBoard[index], curBoard[index + 1]))
            {
                //resolvemove right
                possibleBoards.Add(ResolveMove(index, index + 1));
            }
            if (index - 1 >= 0 && ValidMove(curBoard[index], curBoard[index - 1]))
            {
                //resolvemove left
                possibleBoards.Add(ResolveMove(index, index - 1));
            }
        }

        AICellData[] ResolveMove(int start, int end)
        {
            AICellData[] possibleBoard = curBoard;

            if(curBoard[start].value == curBoard[end].value)
            {
                possibleBoard[end].value = 0;
                possibleBoard[end].player = Player.Empty;
            }

            else if (curBoard[start].value > curBoard[end].value)
            {
                possibleBoard[end].value = curBoard[start].value;
                possibleBoard[end].player = curBoard[start].player;
            }

            else if (curBoard[end].value == 5)
            {
                possibleBoard[end].value = curBoard[start].value;
                possibleBoard[end].player = curBoard[start].player;
            }

            possibleBoard[start].value = 0;
            possibleBoard[start].player = Player.Empty;

            return possibleBoard;
        }

        return possibleBoards;
    }

    

    bool ValidMove(AICellData start, AICellData end)
    {
        bool positionCheck = false;

        if (start.position.x == end.position.x && Math.Abs(start.position.y - end.position.y) == 1)
        {
            positionCheck = true;
        }

        else if (start.position.y == end.position.y && Math.Abs(start.position.x - end.position.x) == 1)
        {
            positionCheck = true;
        }

        if (!positionCheck) return false;

        else if (end.faceup == false || end.player == Player.Blue) return false;

        else
        {
            return true;
        }
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