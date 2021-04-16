using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearch
{
    
    public int EvaluatePosition(AICellData[] board)
    {
        int pieceScore = 0;
        for(int i = 0; i < board.Length; i++)
        {
            pieceScore += board[i].value * (int)board[i].player;
        }

        return pieceScore;
    }
    
}
