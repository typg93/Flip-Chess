using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public Board board;
    private int boardX;
    private int boardY;

    BitBoard bitBoard = new BitBoard();

    private void Awake()
    {
        boardX = board.boardX;
        boardY = board.boardY;
    }
    public void ScanBoardToBitBoard()
    {
        foreach(Cell cell in board.cells)
        {
            Player cellColor = cell.GetColor();
            bool flipState = cell.GetFlipState();
            int cellValue = (int)cell.GetValue();
            int cellPos = (int)cell.GetCoordinate().x + (int)cell.GetCoordinate().y * boardX;

            if(flipState == true)
            {
                bitBoard.FaceUps |= SetOneAtIndex(cellPos);
            }

            if (cellColor == Player.Red)
            {
               switch (cellValue)
                {
                    case 1:
                        bitBoard.RedOnes |= SetOneAtIndex(cellPos); break;
                    case 2:
                        bitBoard.RedTwos |= SetOneAtIndex(cellPos); break;
                    case 3:
                        bitBoard.RedThrees |= SetOneAtIndex(cellPos); break;
                    case 4:
                        bitBoard.RedFours |= SetOneAtIndex(cellPos); break;
                    case 5:
                        bitBoard.RedKing |= SetOneAtIndex(cellPos); break;
                    default:
                        break;
                }
            }

            else if (cellColor == Player.Blue)
            {
                switch (cellValue)
                {
                    case 1:
                        bitBoard.BlueOnes |= SetOneAtIndex(cellPos); break;
                    case 2:
                        bitBoard.BlueTwos |= SetOneAtIndex(cellPos); break;
                    case 3:
                        bitBoard.BlueThrees |= SetOneAtIndex(cellPos); break;
                    case 4:
                        bitBoard.BlueFours |= SetOneAtIndex(cellPos); break;
                    case 5:
                        bitBoard.BlueKing |= SetOneAtIndex(cellPos); break;
                    default:
                        break;
                }
            }
        }
        PrintBoardValue(bitBoard.FaceUps);
    }

    uint SetOneAtIndex(int i)
        //set a 1 on a bitboard at index i with the rest as 0's
    {
        uint bitBoard = 1;
        bitBoard = bitBoard << i;
        return bitBoard;
    }

    int PieceCount(uint bitboard)
    //counts the number of 1 bits in a bitboard
    {
        int count = 0;

        while (bitboard != 0)
        {
            bitboard &= (bitboard - 1);
            count++;
        }
        return count;
    }

    void PrintBoardValue(uint bitBoard)
        //debug.log a bitboard value to match location of game board
    {
        string bitString = Convert.ToString(bitBoard, toBase: 2);
        string remainingEmptyBoard = new string('0', boardX * boardY - bitString.Length);
        bitString = remainingEmptyBoard + bitString;

        for (int i = 0; i < boardY; i++)
        {
            string printBitString = "";
            for (int j = i*boardX; j < i*boardX + boardX; j++)
            {
                printBitString = bitString[j] + " | " + printBitString;
            }
            Debug.Log(printBitString);
        }   
    }
}
