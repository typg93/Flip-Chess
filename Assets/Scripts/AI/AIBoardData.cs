using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoardData 
{
    //boardData - flattened array to store the cell's information value/flip state/color
    //
    //****wrapper properties generated by MoveGenerator****
    //probability - used in ExpectiMax method to calculate the average score of a chance node
    //chanceNode - if true, this board will be calculate the score of the average of 
    //flipIndex - the index of the piece to be flipped up during score averaging
    //gameWon - halt the ExpectiMax method once the game has been won

    public AICellData[] boardData;
    public double probability = 1;
    public bool chanceNode = false;
    public int flipIndex = -1;
    public bool gameWon = false;
    public int scoreOffset = 0;

    public AIBoardData(AICellData[] boardData, double probability, bool chanceNode)
    {
        this.boardData = boardData;
        this.probability = probability;
        this.chanceNode = chanceNode;
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
