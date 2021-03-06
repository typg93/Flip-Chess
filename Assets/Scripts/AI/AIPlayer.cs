using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public Board board;
    private int boardX;
    private int boardY;
    private AISearch ai;

    private void Awake()
    {
        boardX = board.boardX;
        boardY = board.boardY;
        ai = new AISearch();
    }

    public IEnumerator FinishMove()
    {
        if (canMove())
        {
            AISearch ai = new AISearch();
            AIBoardData testBoard = ScanBoard();
            yield return new WaitForSeconds(1);
            AIBoardData bestBoard = ai.BestMove(testBoard, 4, 1);
            MakeMove(testBoard, bestBoard);
        }
    }

    AIBoardData ScanBoard()
        //scans current board and flattens cell data into an array
    {
        AICellData[] flattenedCellArray = new AICellData[32];
        for (int y = 0; y < boardY; y++)
        {
            for (int x = 0; x < boardX; x++)
            {
                int index = y * boardX + x;
                Cell cell = board.cells[x, y];
                flattenedCellArray[index].value = cell.GetValue();
                flattenedCellArray[index].player = cell.GetColor();
                flattenedCellArray[index].faceup = cell.GetFlipState();
                flattenedCellArray[index].position = cell.GetCoordinate();
            }
        }
        return new AIBoardData(flattenedCellArray, false);
    }

    void MakeMove(AIBoardData startBoard, AIBoardData endBoard)
    {
        if (endBoard.chanceNode)
        {
            int x = endBoard.flipIndex % boardX;
            int y = endBoard.flipIndex / boardX;
            board.cells[x,y].FlipMove();
        }
        else
        {
            int moveFromX = endBoard.moveFromIndex % boardX;
            int moveFromY = endBoard.moveFromIndex / boardX;
            int moveToX = endBoard.moveToIndex % boardX;
            int moveToY = endBoard.moveToIndex / boardX;
            board.cells[moveFromX, moveFromY].MoveTo(board.cells[moveToX, moveToY]);
            Debug.Log(moveFromX + " " + moveFromY + " to: " + moveToX + " " + moveToY);
        }
    }

    private bool canMove()
    {
        return GameManager.instance.canMove;
    }

}
