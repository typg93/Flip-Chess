using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{

    public Board board;

    // uses bitboard data structure to store board position.
    // true in flattened array represents piece at location
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
            int cellPos = (int)cell.GetCoordinate().x * 4 +
                (int)cell.GetCoordinate().y;
                
            Debug.Log("Cell val is " + cellValue + " and position is " + cellPos);
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
