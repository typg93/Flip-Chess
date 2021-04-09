using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public Board board;

    // ----BitBoard variables----
    // - uses bitboard data structure to store board position
    // - flattens data in Cell Class into uint32 in series of 1's and 0's
    // - left bottom corner will be index 0
    // - used with bitwise operations
    // - board position to bitboard uint32 digit index:
    //| 24 | 25 | 26 | 27 | 28 | 29 | 30 | 31 |
    //| 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 |
    //| 08 | 09 | 10 | 11 | 12 | 13 | 14 | 15 |
    //| 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 |
    private uint RedOnes;
    private uint RedTwos;
    private uint RedThrees;
    private uint RedFours;
    private uint RedKing;
    private uint BlueOnes;
    private uint BlueTwos;
    private uint BlueThrees;
    private uint BlueFours;
    private uint BlueKing;
    private uint FaceUp;


    public void ScanBoard()
    {
        foreach(Cell cell in board.cells)
        {
            int cellValue = (int)cell.GetValue();
            int cellPos = (int)cell.GetCoordinate().x +
                (int)cell.GetCoordinate().y * board.boardX;
                
            if (cellValue == 1)
            {
                RedOnes |= SetBitBoard(cellPos);
            }
        }
        Debug.Log(Convert.ToString(RedOnes, toBase: 2));
    }

    uint SetBitBoard(int i)
    {
        uint bitBoard = 1;
        bitBoard = bitBoard << i;
        Debug.Log(Convert.ToString(bitBoard, toBase: 2));
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

    void LogBoardValue()
    {

    }
}
