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
            textBoard[i] = cellDisplayGO.GetComponent<TextMeshProUGUI>().text;
        }
    }

    void DisplayBoardValues(AICellData[] boardData)
    {
        for (int i = 0; i < 32; i++)
        {
            textBoard[i] = boardData[i].value.ToString();
        }
    }

}
