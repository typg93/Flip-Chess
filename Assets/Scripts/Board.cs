using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject CellPrefab;
    [SerializeField]
    private GameObject PiecePrefab;

    public GameObject[,] cellsGO;
    public Cell[,] cells;
    [SerializeField]
    private int boardX = 8, boardY = 5;
    
    

    private static readonly System.Random _random = new System.Random();
    public int[] allGamePieces;
    private int[] allGamePiecesandEmpty;
    public Board(int boardX, int boardY)
    {
        this.boardX = boardX;
        this.boardY = boardY;
    }

    public Color32 gridColorOdd = new Color32(10, 10, 10, 255);
    public Color32 gridColorEven = new Color32(230, 220, 187, 255);
    private Boolean gridPaint = true;

    private float cellWidth;
    private float cellHeight;

    void Start()
    {
        InitializeBoard();
        RandomizeCellValues();
    }

    private void ShufflePieces()
        //adds empty slots (value = 0) onto the shufflearray array
    {
        allGamePiecesandEmpty = new int[boardX * boardY];
        for (int i = 0; i < allGamePieces.Length; i++)
        {
            allGamePiecesandEmpty[i] = allGamePieces[i];
        }
        ShuffleArray(allGamePiecesandEmpty);
    }

    public void InitializeBoard()
        //initializes cell transform, base color, and sets all cell value to 0
    {
        #region GenerateCells
        cellWidth = CellPrefab.GetComponent<RectTransform>().rect.width;
        cellHeight = CellPrefab.GetComponent<RectTransform>().rect.height;
        cellsGO = new GameObject[boardX, boardY];
        cells = new Cell[boardX, boardY];

        for (int y = 0; y < boardY; y++)
        {
            for (int x = 0; x < boardX; x++)
            {
                // Create the cell
                GameObject newCellGO = Instantiate(CellPrefab, transform);

                //initializing pieces
                Cell newCell = newCellGO.GetComponent<Cell>();
                newCell.ChangeValue(CellValue.Empty, Player.Empty, true);
                newCellGO.GetComponent<Cell>().SetCoordinate(new Vector2(x, y));

                // Position
                RectTransform cellTransform = newCellGO.GetComponent<RectTransform>();
                cellTransform.anchoredPosition = new Vector2((x * cellWidth) + cellWidth/2, (y * cellHeight) + cellHeight/2);

                // Setup and color
                cells[x, y] = newCell;
                cellsGO[x, y] = newCellGO;
                cellsGO[x, y].GetComponent<Image>().color = gridPaint? gridColorOdd : gridColorEven;
                gridPaint = !gridPaint;
            }
            gridPaint = !gridPaint;
        }
        #endregion
    }

    public void RandomizeCellValues()
    {
        ShufflePieces();
        int pieceIndex = 0;
        foreach(Cell cell in cells)
        {
            Player valueColor = (Player)Math.Sign(allGamePiecesandEmpty[pieceIndex]);
            cell.ChangeValue((CellValue)Math.Abs(allGamePiecesandEmpty[pieceIndex]), valueColor, true);
            pieceIndex++;
        }
    }

    public void ClearBoard()
    {
        foreach(GameObject cellGM in cellsGO)
        {
            Cell cell = cellGM.GetComponent<Cell>();
            cell.ChangeValue(CellValue.Empty, Player.Empty, true);
        }
    }

    //to do move to util class
    static void ShuffleArray(int[] array)
        //Shuffles an array using Fischer-Yale algorithm. O(n) time complexity.
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            int t = array[r];
            array[r] = array[i];
            array[i] = t;
        }
    }

}
