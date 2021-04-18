using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardData 
{
    public AICellData[] boardData;
    public int probability = 1;

    public AIBoardData(AICellData[] boardData, int probability)
    {
        this.boardData = boardData;
        this.probability = probability;
    }
}
