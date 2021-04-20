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

    public AIBoardData BestMove(AIBoardData board, int depth, int flipDepth)
        //Check if there is any checkmate from all one move away positions from current board. 
        //Then calculate best move up to #depth of moves using ExpectiMax algorithm
    {
        List<AIBoardData> possibleBoards = new List<AIBoardData>();
        possibleBoards = GenerateMoves(board, Player.Blue, true);
        AIBoardData bestBoard = possibleBoards[0];
        double bestScore = ExpectiMax(possibleBoards[0], true, depth - 1, flipDepth - 1);

        for (int i = 0; i < possibleBoards.Count; i++)
        {
            if (!possibleBoards[i].gameWon)
            {
                double newScore = ExpectiMax(possibleBoards[i], true, depth - 1, flipDepth - 1);
                if (newScore < bestScore)
                {
                    bestBoard = possibleBoards[i];
                    bestScore = newScore;
                }
            }
            else return possibleBoards[i];
        }

        Debug.Log(bestScore);
        Debug.Log(bestBoard.gameWon);
        
        return bestBoard;
    }

    public double ExpectiMax(AIBoardData board, bool maximizingPlayer, int depth, int flipDepth)
    {
        bool canFlip = true;
        if (flipDepth <= 0) canFlip = false;

        if(depth <= 0)
        {
            return EvaluatePosition(board);
        }
        else if (maximizingPlayer)
        {
            double value = double.MinValue;
            foreach(AIBoardData possibleBoard in GenerateMoves(board, Player.Red, canFlip))
            {

                //Stop calculating moves if there is game winning move
                if (!possibleBoard.gameWon)
                {
                    value = Math.Max(value, possibleBoard.probability * ExpectiMax(possibleBoard, false, depth - 1, flipDepth));
                }
                else if (possibleBoard.gameWon) value = double.MaxValue;
            }
            return value;
        }
        else if (!maximizingPlayer)
        {
            double value = double.MaxValue;
            foreach (AIBoardData possibleBoard in GenerateMoves(board, Player.Blue, canFlip))
            {

                //Stop calculating moves if there is game winning move
                if (!possibleBoard.gameWon)
                {
                    value = Math.Min(value, possibleBoard.probability * ExpectiMax(possibleBoard, true, depth - 1, flipDepth));
                }
                else if (possibleBoard.gameWon) value = double.MinValue;
            }
            return value;
        }
        else return 0;
    }

    public List<AIBoardData> GenerateMoves(AIBoardData board, Player player, bool canFlip)
    {
        AIBoardData curBoard = board;
        List<AIBoardData> possibleBoards = new List<AIBoardData>();

        for (int index = 0; index < curBoard.boardData.Length; index++)
        {
            if ((canFlip) && !curBoard.boardData[index].faceup)
            {
                //Debug.Log("flip move");
                possibleBoards.Add(FlipMove(index, Player.Red, CellValue.One));
                possibleBoards.Add(FlipMove(index, Player.Red, CellValue.Two));
                possibleBoards.Add(FlipMove(index, Player.Red, CellValue.Three));
                possibleBoards.Add(FlipMove(index, Player.Red, CellValue.Four));
                possibleBoards.Add(FlipMove(index, Player.Red, CellValue.King));
                possibleBoards.Add(FlipMove(index, Player.Blue, CellValue.King));
                //do the flip moves
            }
            else
            {
                if (index + boardX < curBoard.boardData.Length && ValidMove(curBoard.boardData[index], curBoard.boardData[index + boardX], player))
                {
                    //resolvemove top
                    possibleBoards.Add(ResolveMove(index, index + boardX));
                }
                if (index - boardX >= 0 && ValidMove(curBoard.boardData[index], curBoard.boardData[index - boardX], player))
                {
                    //resolvemove btm
                    possibleBoards.Add(ResolveMove(index, index - boardX));
                }
                if (index + 1 < curBoard.boardData.Length && ValidMove(curBoard.boardData[index], curBoard.boardData[index + 1], player))
                {
                    //resolvemove right
                    possibleBoards.Add(ResolveMove(index, index + 1));
                }
                if (index - 1 >= 0 && ValidMove(curBoard.boardData[index], curBoard.boardData[index - 1], player))
                {
                    //resolvemove left
                    possibleBoards.Add(ResolveMove(index, index - 1));
                }
            }
        }

        AIBoardData FlipMove(int index, Player color, CellValue value)
        {
            float prob = ProbabilityOfPieceFlip(curBoard, color, value);
            AIBoardData possibleBoard = new AIBoardData((AICellData[])curBoard.boardData.Clone(), prob);
            possibleBoard.boardData[index].value = value;
            possibleBoard.boardData[index].player = color;
            possibleBoard.boardData[index].faceup = true;

            return possibleBoard;
        }

        AIBoardData ResolveMove(int start, int end)
        {
            //make a clone of a new possibleBoard within one move away from current board
            AIBoardData possibleBoard = new AIBoardData((AICellData[])curBoard.boardData.Clone(), 1);

            //Resolve if both pieces are equal (remove both pieces)
            if (curBoard.boardData[start].value == curBoard.boardData[end].value && curBoard.boardData[start].value != CellValue.King)
            {
                possibleBoard.boardData[end].value = 0;
                possibleBoard.boardData[end].player = Player.Empty;
            }

            //Resolve if king has been taken (game over)
            else if (curBoard.boardData[end].value == CellValue.King)
            {
                possibleBoard.boardData[end].value = curBoard.boardData[start].value;
                possibleBoard.boardData[end].player = curBoard.boardData[start].player;
                possibleBoard.gameWon = true;
            }

            //Resolve if taking piece
            else if (curBoard.boardData[start].value > curBoard.boardData[end].value || curBoard.boardData[start].value == CellValue.King)
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

    bool ValidMove(AICellData start, AICellData end, Player player)
    {
        bool positionCheck = false;

        if (start.player != player || start.faceup == false) return false;

        if (start.position.x == end.position.x && Math.Abs(start.position.y - end.position.y) == 1)
        {
            positionCheck = true;
        }

        else if (start.position.y == end.position.y && Math.Abs(start.position.x - end.position.x) == 1)
        {
            positionCheck = true;
        }

        if (!positionCheck) return false;

        else if (end.faceup == false || end.player == player) return false;

        else
        {
            return true;
        }
    }

    public float ProbabilityOfPieceFlip(AIBoardData board, Player player, CellValue cellValue)
        //calculate the probability of getting a certain piece while it is facedown
    {
        int value = (int)player * (int)cellValue;
        float probability = 0;
        int totalFaceDownPieces = 0;
        int targetPieceCounter = 0;
        
        for(int i = 0; i < board.boardData.Length; i++)
        {
            if (!board.boardData[i].faceup && board.boardData[i].value != 0)
            {
                totalFaceDownPieces++;
                
                int currCellValue = (int)board.boardData[i].player * (int)board.boardData[i].value;
                if (currCellValue == value)
                {
                    targetPieceCounter++;
                }
            }
        }
        if (totalFaceDownPieces > 0) probability = (float)targetPieceCounter / totalFaceDownPieces;
        
        return probability;
    }
}
