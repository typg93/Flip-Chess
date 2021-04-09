using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public Board board;

    //bitboard to store board position
    private UInt64 RedOnes;
    private UInt64 RedTwos;
    private UInt64 RedThrees;
    private UInt64 RedFours;
    private UInt64 RedKing;
    private UInt64 BlueOnes;
    private UInt64 BlueTwos;
    private UInt64 BlueThrees;
    private UInt64 BlueFours;
    private UInt64 BlueKing;
    private UInt64 FaceUp;


    void ScanBoard()
    {
        foreach(Cell cell in board.cells)
        {

        }
    }

    int PieceCount(UInt64 bitboard)
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
