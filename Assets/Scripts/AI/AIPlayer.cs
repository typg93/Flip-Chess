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

    private void Start()
    {
        GameManager.instance.OnEndTurn += GM_OnEndTurn;
    }

    public void GM_OnEndTurn(object sender, EventArgs e)
    {
        AIBoardData testBoard = ScanBoard();
        AIBoardData bestBoard = ai.BestMove(testBoard, 4, 1);
        MakeMove(testBoard, bestBoard);
    }

    public void TestBestMove()
    {
        AISearch ai = new AISearch();
        AIBoardData testBoard = ScanBoard();
        DisplayBoardArray.instance.DisplayBoardValues(ai.BestMove(testBoard, 4, 1));
    }
    public void TestGenerateFlipMove()
    {
        AISearch ai = new AISearch();
        AIBoardData testBoard = ScanBoard();
        AIBoardData bestBoard = ai.BestMove(testBoard, 4, 1);
        MakeMove(testBoard, bestBoard);
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

    public void MakeMove(AIBoardData startBoard, AIBoardData endBoard)
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

}
