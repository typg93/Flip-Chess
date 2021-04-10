using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public Board board;

    // ----BitBoard variables----
    // - uses bitboard data structure to store board position
    // - flattens data in Cell Class into uint32 to represent pieces locations in binary
    // - board position to bitboard uint32 digit index:
    //| 24 | 25 | 26 | 27 | 28 | 29 | 30 | 31 |
    //| 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 |
    //| 08 | 09 | 10 | 11 | 12 | 13 | 14 | 15 |
    //| 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 |
    private struct BitBoardValues
    {
        public uint RedOnes;
        public uint RedTwos;
        public uint RedThrees;
        public uint RedFours;
        public uint RedKing;
        public uint BlueOnes;
        public uint BlueTwos;
        public uint BlueThrees;
        public uint BlueFours;
        public uint BlueKing;
        public uint FaceUp;
    }
    BitBoardValues BitBoard = new BitBoardValues();

    private int boardX;
    private int boardY;

    private void Awake()
    {
        boardX = board.boardX;
        boardY = board.boardY;
    }
    public void ScanBoard()
    {
        
        foreach(Cell cell in board.cells)
        {
            int cellValue = (int)cell.GetValue();
            int cellPos = (int)cell.GetCoordinate().x +
                (int)cell.GetCoordinate().y * boardX;
                
            if (cellValue == 1)
            {
                BitBoard.RedOnes |= SetIndexBitBoard(cellPos);
            }
        }
        LogBoardValue(BitBoard.RedOnes);
    }

    uint SetIndexBitBoard(int i)
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

    void LogBoardValue(uint bitBoard)
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
