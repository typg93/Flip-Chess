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
        return pieceScore + board.scoreOffset;
    }

    public AIBoardData BestMove(AIBoardData board, int depth, int flipDepth)
        //Check if there is any checkmate from all one move away positions from current board. 
        //Then calculate best move up to #depth of moves using ExpectiMax algorithm
    {
        List<AIBoardData> possibleBoards = new List<AIBoardData>();
        possibleBoards = GenerateMoves(board, Player.Blue, true);
        AIBoardData bestBoard = possibleBoards[0];
        double bestScore = ExpectiMax(possibleBoards[0], true, depth - 1, flipDepth);

        for (int i = 0; i < possibleBoards.Count; i++)
        {
            if (possibleBoards[i].chanceNode)
            {
                double newScore = ExpectiMaxChanceNode(possibleBoards[i], true, depth - 1);
                if (newScore < bestScore)
                {
                    bestBoard = possibleBoards[i];
                    bestScore = newScore;
                }
            }
            else if (!possibleBoards[i].gameWon)
            {
                double newScore = ExpectiMax(possibleBoards[i], true, depth - 1, flipDepth);
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

    public double ExpectiMaxChanceNode(AIBoardData board, bool maximizingPlayer, int depth)
    {
        double value = double.MaxValue;
        List<double> averages = new List<double>();

        averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.One) * ExpectiMax(GenerateFlipMove(board, Player.Blue, CellValue.One), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.Two) * ExpectiMax(GenerateFlipMove(board, Player.Blue, CellValue.Two), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.Three) * ExpectiMax(GenerateFlipMove(board, Player.Blue, CellValue.Three), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.Four) * ExpectiMax(GenerateFlipMove(board, Player.Blue, CellValue.Four), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.King) * ExpectiMax(GenerateFlipMove(board, Player.Blue, CellValue.King), maximizingPlayer, depth - 1, 0));

        averages.Add(ProbabilityOfPieceFlip(board, Player.Red, CellValue.One) * ExpectiMax(GenerateFlipMove(board, Player.Red, CellValue.One), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Red, CellValue.Two) * ExpectiMax(GenerateFlipMove(board, Player.Red, CellValue.Two), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Red, CellValue.Three) * ExpectiMax(GenerateFlipMove(board, Player.Red, CellValue.Three), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Red, CellValue.Four) * ExpectiMax(GenerateFlipMove(board, Player.Red, CellValue.Four), maximizingPlayer, depth - 1, 0));
        averages.Add(ProbabilityOfPieceFlip(board, Player.Red, CellValue.King) * ExpectiMax(GenerateFlipMove(board, Player.Red, CellValue.King), maximizingPlayer, depth - 1, 0));

        value = AverageWithoutZeros(averages);
        return value;
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
                if (possibleBoard.chanceNode)
                {

                }
                else if (possibleBoard.gameWon) value = double.MaxValue;

                else
                {
                    value = Math.Max(value, ExpectiMax(possibleBoard, false, depth - 1, flipDepth));
                }


            }
            return value;
        }
        else if (!maximizingPlayer)
        {
            double value = double.MaxValue;
            foreach (AIBoardData possibleBoard in GenerateMoves(board, Player.Blue, canFlip))
            {
                if (possibleBoard.chanceNode)
                {


                    //List<double> averages = new List<double>();
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Red, CellValue.One), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Red, CellValue.Two), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Red, CellValue.Three), maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Red, CellValue.Four), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Red, CellValue.King), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Blue, CellValue.One), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Blue, CellValue.Two), !maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(0.1 * ExpectiMax(GenerateFlipMove(possibleBoard, Player.Blue, CellValue.Three), maximizingPlayer, depth, flipDepth - 1));
                    //averages.Add(ProbabilityOfPieceFlip(board, Player.Blue, CellValue.Four) * ExpectiMax(GenerateFlipMove(possibleBoard, Player.Blue, CellValue.Four), maximizingPlayer, depth - 1, flipDepth - 1));
                    //averages.Add(ExpectiMax(GenerateFlipMove(possibleBoard, Player.Blue, CellValue.King), !maximizingPlayer, depth, flipDepth - 1));
                    //value = Math.Min(value, AverageWithoutZeros(averages));
                }

                //Stop calculating moves if there is game winning move
                else if (possibleBoard.gameWon) value = double.MinValue;

                else
                {
                    value = Math.Min(value, ExpectiMax(possibleBoard, true, depth - 1, flipDepth));
                }
                
            }
            return value;
        }
        else return 0;
    }

    double AverageWithoutZeros(List<double> list)
    {
        double totalScore = 0;
        int counter = 0;
        foreach (double score in list)
        {
            if (score != 0)
            {
                counter++;
                totalScore += score;
            }
        }
        return totalScore / counter;
    }

    public AIBoardData GenerateFlipMove(AIBoardData board, Player color, CellValue value)
    {
        int index = board.flipIndex;
        AIBoardData possibleBoard = new AIBoardData((AICellData[])board.boardData.Clone(), 1, false);

        possibleBoard.boardData[index].value = value;
        possibleBoard.boardData[index].player = color;
        possibleBoard.boardData[index].faceup = true;
        possibleBoard.chanceNode = false;
        possibleBoard.scoreOffset = board.scoreOffset;
        possibleBoard.flipIndex = index;
        possibleBoard.probability = 1;

        int scoreOffSet = (int)board.boardData[index].player * (int)board.boardData[index].value -
                            (int)possibleBoard.boardData[index].player * (int)possibleBoard.boardData[index].value;

        possibleBoard.scoreOffset += scoreOffSet;
        return possibleBoard;
    }

    public List<AIBoardData> GenerateMoves(AIBoardData board, Player player, bool canFlip)
    {
        AIBoardData curBoard = board;
        List<AIBoardData> possibleBoards = new List<AIBoardData>();

        for (int index = 0; index < curBoard.boardData.Length; index++)
        {
            //generate board with piece index to be flipped
            if ((canFlip) && !curBoard.boardData[index].faceup)
            {
                AIBoardData possibleBoard = new AIBoardData((AICellData[])curBoard.boardData.Clone(), 1, true);
                possibleBoard.flipIndex = index;
                possibleBoard.chanceNode = true;
                possibleBoard.boardData[index].faceup = false;
                possibleBoards.Add(possibleBoard);
            }

            // horizontal and vertical piece movement
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

        AIBoardData ResolveMove(int start, int end)
        {
            //make a clone of a new possibleBoard within one move away from current board
            AIBoardData possibleBoard = new AIBoardData((AICellData[])curBoard.boardData.Clone(), 1, false);

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
            possibleBoard.scoreOffset = curBoard.scoreOffset;
            possibleBoard.flipIndex = curBoard.flipIndex;

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
