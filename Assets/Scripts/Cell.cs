using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    private Vector2 cellCoordinate;
    private int value;
    private Player valueColor;
    private bool faceUp;

    //The children piece script attached to this gameobject.
    private Piece piece;
    private GameObject pieceGO;
    private Vector3 pieceOldPosition;
    private Canvas pieceCanvas;

    private Cell endDragPiece;

    public Cell(int value, Player valueColor, bool faceUp, Vector2 cellCoordinate)
    {
        (this.value, this.valueColor, this.faceUp, this.cellCoordinate) = (value, valueColor, faceUp, cellCoordinate);
    }

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
    public void ChangeValue(int value, Player valueColor, bool faceUp)
        //value = value of the piece
        //faceUp = whether the piece is turned up or down
    {
        (this.value, this.valueColor, this.faceUp) = (value, valueColor, faceUp);
        piece.ChangeSprite(value, valueColor, faceUp);
    }

    public void ChangeValue(bool faceUp)
    {
        ChangeValue(value, valueColor, faceUp);
    }

    public int GetValue()
    {
        return value;
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
            endDragPiece = eventData.pointerEnter.GetComponent<Cell>();
            MoveTo(endDragPiece);
        }
        else ResetPiece();
    }

    private void MoveTo(Cell endDragPiece)
    {
        if (ValidMove(this, endDragPiece))
        {
            endDragPiece.ChangeValue(value, valueColor, true);
            ChangeValue(0, Player.Empty, true);
            GameManager.instance.EndTurn();
        }
        ResetPiece();
    }

    private bool ValidMove(Cell start, Cell end)
        //check if destination cell is within 1 square
    {

        if (start == end)
        {
            return false;
        }

        else if (!faceUp)
        {
            return false;
        }

        else if (GameManager.instance.PlayerTurn() != valueColor)
        {
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

        else return false;
    }

    private void ResetPiece()
        //puts the piece sprite back in place
    {
        pieceGO.transform.position = pieceOldPosition;
        pieceCanvas.sortingOrder = 10;
    }


}
