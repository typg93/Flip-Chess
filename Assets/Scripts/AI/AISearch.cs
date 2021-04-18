using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearch
{
    private int boardX = 8;
    private int boardY = 4;
    public int EvaluatePosition(AIBoardData board)
    {
        int pieceScore = 0;
        for(int i = 0; i < board.boardData.Length; i++)
        {
            pieceScore += board.boardData[i].value * (int)board.boardData[i].player;
        }

        return pieceScore;
    }

    //private int count;
    //public void Tester()
    //{
    //    List<AICellData[]> data = GenerateMoves(ScanBoard());
    //    DisplayBoardArray.instance.DisplayBoardValues(data[count]);
    //    count++;
    //}

    public int ExpectiMax(AIBoardData board, bool maximizingPlayer, int depth)
    {
        if(depth == 0)
        {
            return EvaluatePosition(board);
        }
        else if (maximizingPlayer)
        {
            int value = int.MinValue;
            foreach(AIBoardData possibleBoard in GenerateMoves(board))
            {
                value = Math.Max(value, ExpectiMax(possibleBoard, false, depth--));
            }
        }
        else if (!maximizingPlayer)
        {

        }
        
        return 0;
    }
    public List<AIBoardData> GenerateMoves(AIBoardData board)
    {
        Debug.Log("generatemoves called");
        AIBoardData curBoard = board;
        List<AIBoardData> possibleBoards = new List<AIBoardData>();

        for (int index = 0; index < curBoard.boardData.Length; index++)
        {

            if (index + boardX < curBoard.boardData.Length && ValidMove(curBoard.boardData[index], curBoard.boardData[index + boardX]))
            {
                //resolvemove top
                possibleBoards.Add(ResolveMove(index, index + boardX));
            }
            if (index - boardX >= 0 && ValidMove(curBoard.boardData[index], curBoard.boardData[index - boardX]))
            {
                //resolvemove btm
                possibleBoards.Add(ResolveMove(index, index - boardX));
            }
            if (index + 1 < curBoard.boardData.Length && ValidMove(curBoard.boardData[index], curBoard.boardData[index + 1]))
            {
                //resolvemove right
                possibleBoards.Add(ResolveMove(index, index + 1));
            }
            if (index - 1 >= 0 && ValidMove(curBoard.boardData[index], curBoard.boardData[index - 1]))
            {
                //resolvemove left
                possibleBoards.Add(ResolveMove(index, index - 1));
            }
        }

        AIBoardData ResolveMove(int start, int end)
        {
            AIBoardData possibleBoard = new AIBoardData(curBoard.boardData, curBoard.probability);

            if (curBoard.boardData[start].value == curBoard.boardData[end].value)
            {
                possibleBoard.boardData[end].value = 0;
                possibleBoard.boardData[end].player = Player.Empty;
            }

            else if (curBoard.boardData[start].value > curBoard.boardData[end].value)
            {
                possibleBoard.boardData[end].value = curBoard.boardData[start].value;
                possibleBoard.boardData[end].player = curBoard.boardData[start].player;
            }

            else if (curBoard.boardData[end].value == 5)
            {
                possibleBoard.boardData[end].value = curBoard.boardData[start].value;
                possibleBoard.boardData[end].player = curBoard.boardData[start].player;
            }

            possibleBoard.boardData[start].value = 0;
            possibleBoard.boardData[start].player = Player.Empty;

            return possibleBoard;
        }

        return possibleBoards;
    }


    bool ValidMove(AICellData start, AICellData end)
    {
        bool positionCheck = false;

        if (start.player != Player.Blue || start.faceup == false) return false;

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
