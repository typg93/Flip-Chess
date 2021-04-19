using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearch
{
    //width of board
    private int boardX = 8;
    private int boardY = 4;
    public int EvaluatePosition(AIBoardData board)
    {
        int pieceScore = 0;
        for(int i = 0; i < board.boardData.Length; i++)
        {
            pieceScore += (int)board.boardData[i].value * (int)board.boardData[i].player;
        }

        return pieceScore;
    }


    public double ExpectiMax(AIBoardData board, bool maximizingPlayer, int depth)
    {
        if(depth == 0)
        {
            return EvaluatePosition(board);
        }
        else if (maximizingPlayer)
        {
            double value = double.MinValue;
            foreach(AIBoardData possibleBoard in GenerateMoves(board))
            {
                value = Math.Max(value, possibleBoard.probability * ExpectiMax(possibleBoard, false, depth--));
            }
            return value;
        }
        else if (!maximizingPlayer)
        {
            double value = double.MaxValue;
            foreach (AIBoardData possibleBoard in GenerateMoves(board))
            {
                value = Math.Min(value, possibleBoard.probability * ExpectiMax(possibleBoard, true, depth--));
            }
            return value;
        }
        else return 0;
    }

    public List<AIBoardData> GenerateMoves(AIBoardData board)
    {
        AIBoardData curBoard = board;
        List<AIBoardData> possibleBoards = new List<AIBoardData>();

        for (int index = 0; index < curBoard.boardData.Length; index++)
        {
            if (!curBoard.boardData[index].faceup)
            {
                //do the flip moves
            }
            else
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
        }

        AIBoardData ResolveMove(int start, int end)
        {
            //make a clone of a new possibleBoard with one move of current board
            AIBoardData possibleBoard = new AIBoardData((AICellData[])curBoard.boardData.Clone(), 1);

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

            else if (curBoard.boardData[end].value == CellValue.King)
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

    public float ProbabilityOfPieceFlip(AIBoardData board, int cellValue)
        //calculate the probability of getting any piece while it is facedown
    {
        float probability = 0;
        int faceDownPieces = 0;
        Dictionary<int, int> counter = new Dictionary<int, int> { { 0, 0 } };

        for(int i = 0; i < board.boardData.Length; i++)
        {
            if (!board.boardData[i].faceup && board.boardData[i].value != 0)
            {
                    faceDownPieces++;
                    int currCellValue = (int)board.boardData[i].player * (int)board.boardData[i].value;
                    if (counter.ContainsKey(currCellValue))
                    {
                        counter[currCellValue]++;
                    }
                    else counter.Add(currCellValue, 1);   
            }
        }
        
        if (counter.ContainsKey(cellValue))
        {
            probability = (float)counter[cellValue] / faceDownPieces;
        }
        
        return probability;
    }
}
