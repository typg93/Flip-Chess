using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBoardArray : Singleton<DisplayBoardArray>
    //this class is used for displaying AIBoardData in game time for debugging
{

    public GameObject textPrefab;
    private GameObject[] textCells;

    void Start() {
        textCells = new GameObject[35];
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int i = 0; i < 35; i++)
        {
            GameObject cellDisplayGO = Instantiate(textPrefab, transform);
            textCells[i] = cellDisplayGO;
        }
    }

    public void DisplayBoardValues(AIBoardData aiBoardData)
    {
        for (int i = 0; i < 32; i++)
        {
            string cellText = "";
            if (aiBoardData.boardData[i].faceup) cellText = "^";
            else cellText = "v";

            if (aiBoardData.boardData[i].value == CellValue.King) cellText += "K";
            else cellText += ((int)aiBoardData.boardData[i].value).ToString();
            textCells[i].GetComponent<TextMeshProUGUI>().text = cellText;
        }

        textCells[32].GetComponent<TextMeshProUGUI>().text = aiBoardData.chanceNode.ToString();
        textCells[33].GetComponent<TextMeshProUGUI>().text = aiBoardData.flipIndex.ToString();
        textCells[34].GetComponent<TextMeshProUGUI>().text = aiBoardData.scoreOffset.ToString();
    }

    

    

}
