using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{

    public GameObject CellPrefab;
    public GameObject PiecePrefab;
    public GameObject[,] cells;
    public GameObject[,] pieces;
    public int boardX = 8;
    public int boardY = 5;

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
        GenerateCells();
    }


    public void GenerateCells()
    {
        #region GenerateCells
        cellWidth = CellPrefab.GetComponent<RectTransform>().rect.width;
        cellHeight = CellPrefab.GetComponent<RectTransform>().rect.height;
        cells = new GameObject[boardX, boardY];
        pieces = new GameObject[boardX, boardY];

        for (int y = 0; y < boardY; y++)
        {
            for (int x = 0; x < boardX; x++)
            {
                // Create the cell
                GameObject newCell = Instantiate(CellPrefab, transform);
                GameObject newPiece = Instantiate(PiecePrefab, transform);

                // Position
                RectTransform cellTransform = newCell.GetComponent<RectTransform>();
                RectTransform pieceTransform = newPiece.GetComponent<RectTransform>();
                cellTransform.anchoredPosition = new Vector2((x * cellWidth) + cellWidth/2, (y * cellHeight) + cellHeight/2);
                pieceTransform.anchoredPosition = cellTransform.anchoredPosition;
                // Setup
                
                cells[x, y] = newCell;
                cells[x, y].GetComponent<Image>().color = gridPaint? gridColorOdd : gridColorEven;

                pieces[x, y] = newPiece;
                pieces[x, y].GetComponent<Piece>().value = 5;
                pieces[x, y].GetComponent<Piece>().ChangeValue(4);


                gridPaint = !gridPaint;
            }
            gridPaint = !gridPaint;
        }
        #endregion
    }

}
