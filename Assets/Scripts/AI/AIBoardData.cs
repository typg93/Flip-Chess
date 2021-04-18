using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardData 
{
    public AICellData[] boardData;
    public double probability = 1;

    public AIBoardData(AICellData[] boardData, double probability)
    {
        this.boardData = boardData;
        this.probability = probability;
    }
}
