using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    //cell Data
    private Vector2 cellCoordinate;
    private Player cellColor;
    private bool faceUp;
    private CellValue cellValue;

    //The children piece script attached to this gameobject.
    private Piece piece;
    private GameObject pieceGO;
    private Vector3 pieceOldPosition;
    private Canvas pieceCanvas;
    
    private void Awake()
    {
        piece = transform.GetComponentInChildren<Piece>();
        pieceGO = piece.gameObject;
        pieceCanvas = pieceGO.GetComponent<Canvas>();
    }

    private void Start()
    {
        pieceOldPosition = pieceGO.transform.position;
    }

    public void SetCoordinate(Vector2 coordinate)
    {
        cellCoordinate = coordinate;
    }

    public bool GetFace()
    {
        return faceUp;
    }
    public CellValue GetValue()
    {
        return cellValue;
    }
    public void ChangeValue(CellValue cellValue, Player cellColor, bool faceUp)
        //value = value of the piece
        //faceUp = whether the piece is turned up or down
    {
        (this.cellValue, this.cellColor, this.faceUp) = (cellValue, cellColor, faceUp);
        piece.ChangeSprite(cellValue, cellColor, faceUp);
    }

    public void ChangeValue(bool faceUp)
    {
        ChangeValue(cellValue, cellColor, faceUp);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        pieceCanvas.sortingOrder = 11;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (faceUp) pieceGO.transform.position = Input.mousePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!faceUp) ChangeValue(true);
        GameManager.instance.EndTurn();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null &&
            eventData.pointerEnter.CompareTag("Cell"))
        {
            Cell endDragPiece = eventData.pointerEnter.GetComponent<Cell>();
            MoveTo(endDragPiece);
        }
        else
        {
            Debug.Log("OnEnddrag failed");
            ResetPiece();
        }
            
    }

    #region Move Logic
    public void MoveTo(Cell target)
    {
        if (ValidMove(this, target))
        {
            ResolveMove(target);
            GameManager.instance.EndTurn();
        }
        ResetPiece();
    }

    private void ResolveMove(Cell target)
    {
        if (target.cellValue == CellValue.King)
        {
            target.ChangeValue(cellValue, cellColor, true);
            ChangeValue(CellValue.Empty, Player.Empty, true);
            //win Game
            GameManager.instance.WinGame(GameManager.instance.PlayerTurn());
        }

        else if (this.cellValue == target.cellValue)
        {
            ChangeValue(CellValue.Empty, Player.Empty, true);
            target.ChangeValue(CellValue.Empty, Player.Empty, true);
        }

        else if (cellValue == CellValue.King)
        {
            target.ChangeValue(cellValue, cellColor, true);
            ChangeValue(CellValue.Empty, Player.Empty, true);
        }

        else if ((int)this.cellValue > (int)target.cellValue)
        {
            target.ChangeValue(cellValue, cellColor, true);
            ChangeValue(CellValue.Empty, Player.Empty, true);
        }

        else if ((int)this.cellValue < (int)target.cellValue)
        {
            ChangeValue(CellValue.Empty, Player.Empty, true);
        }

    }

    private bool ValidMove(Cell start, Cell end)
        //check if destination cell is within 1 square
    {

        if (start == end)
        {
            Debug.Log("start=end");
            return false;
        }

        else if (!start.faceUp || !end.faceUp)
        {
            Debug.Log("faceup");
            return false;
        }

        else if (start.cellColor == end.cellColor)
        {
            Debug.Log("same color");
            return false;
        }

        else if (GameManager.instance.PlayerTurn() != cellColor)
        {
            Debug.Log("not same turn");
            return false;
        }

        else if (start.cellCoordinate.x == end.cellCoordinate.x)
        {
            if (Math.Abs(start.cellCoordinate.y - end.cellCoordinate.y) == 1)
            {
                return true;
            }

            else return false;
        }

        else if (start.cellCoordinate.y == end.cellCoordinate.y)
        {
            if (Math.Abs(start.cellCoordinate.x - end.cellCoordinate.x) == 1)
            {
                return true;
            }
            else return false;
        }

        else
        {
            Debug.Log("all passed");
            return false;
        }
                
    }

    private void ResetPiece()
        //puts the piece sprite back in place
    {
        pieceGO.transform.position = pieceOldPosition;
        pieceCanvas.sortingOrder = 10;
    }
    #endregion

}
public enum CellValue{
    Empty = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    King = 5
}
