using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardData 
{
    public AICellData[] boardData;
    public double probability = 1;
    public bool gameWon = false;

    public AIBoardData(AICellData[] boardData, double probability)
    {
        this.boardData = boardData;
        this.probability = probability;
    }
}

public struct AICellData
{
    public CellValue value;
    public bool faceup;
    public Player player;
    public Vector2 position;
    


    public void Reset()
    {
        (value, faceup, player) = (0, true, Player.Empty);
    }
}
