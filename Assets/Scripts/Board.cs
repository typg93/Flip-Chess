using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Board : MonoBehaviour
{
    public GameObject CellPrefab;
    public GameObject PiecePrefab;
    public GameObject[,] cells;
    public GameObject[,] pieces;
    public int boardX = 8;
    public int boardY = 5;

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
        ShufflePieces();
        GenerateCells();
    }

    private void ShufflePieces()
    {
        allGamePiecesandEmpty = new int[boardX * boardY];
        for (int i = 0; i < allGamePieces.Length; i++)
        {
            allGamePiecesandEmpty[i] = allGamePieces[i];
        }
        ShuffleArray(allGamePiecesandEmpty);
    }

    public void GenerateCells()
    {
        #region GenerateCells
        cellWidth = CellPrefab.GetComponent<RectTransform>().rect.width;
        cellHeight = CellPrefab.GetComponent<RectTransform>().rect.height;
        cells = new GameObject[boardX, boardY];
        pieces = new GameObject[boardX, boardY];
        int pieceIndex = 0;
        for (int y = 0; y < boardY; y++)
        {
            for (int x = 0; x < boardX; x++)
            {
                // Create the cell
                GameObject newCell = Instantiate(CellPrefab, transform);

                //Generate pieces
                newCell.GetComponent<Cell>().ChangeValue(allGamePiecesandEmpty[pieceIndex], true);
                pieceIndex++;

                // Position
                RectTransform cellTransform = newCell.GetComponent<RectTransform>();
                cellTransform.anchoredPosition = new Vector2((x * cellWidth) + cellWidth/2, (y * cellHeight) + cellHeight/2);
                // Setup
                
                cells[x, y] = newCell;
                cells[x, y].GetComponent<Image>().color = gridPaint? gridColorOdd : gridColorEven;
                

                gridPaint = !gridPaint;
            }
            gridPaint = !gridPaint;
        }
        #endregion
    }

    //to do change util class
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

    public void RandomizePieces()
    {

    }
}
