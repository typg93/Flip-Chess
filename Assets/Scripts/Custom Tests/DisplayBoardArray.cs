using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayBoardArray : Singleton<DisplayBoardArray>
{

    public GameObject textPrefab;
    private GameObject[] textCells;
    private string[] textBoard;

    void Start() {
        textCells = new GameObject[32];
        textBoard = new string[32];
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int i = 0; i < 32; i++)
        {
            GameObject cellDisplayGO = Instantiate(textPrefab, transform);
            textCells[i] = cellDisplayGO;
        }
        
    }

    public void DisplayBoardValues(AIBoardData aiBoardData)
    {
        for (int i = 0; i < 32; i++)
        {
            textCells[i].GetComponent<TextMeshProUGUI>().text = aiBoardData.boardData[i].value.ToString();
        }
    }

    

}
