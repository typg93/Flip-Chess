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
    private bool faceUp;

    //The children piece script attached to this gameobject.
    private Piece piece;
    private GameObject pieceGO;
    private Vector3 pieceOldPosition;
    private Canvas pieceCanvas;

    private Cell endDragPiece;

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
    public void ChangeValue(int value, bool faceUp)
        //value = value of the piece
        //faceUp = whether the piece is turned up or down
    {
        this.value = value;
        this.faceUp = faceUp;
        piece.ChangeSprite(value, faceUp);
    }

    public void ChangeValue(bool faceUp)
    {
        ChangeValue(value, faceUp);
    }


    public int GetValue()
    {
        return value;
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
            endDragPiece.ChangeValue(value, true);
            ChangeValue(0, true);
        }
        ResetPiece();
    }

    private bool ValidMove(Cell start, Cell end)
    {

        if (start == end)
        {
            return false;
        }

        else if (!faceUp)
        {
            return false;
        }

        return true;
    }

    private void ResetPiece()
        //puts the piece sprite back in place
    {
        pieceGO.transform.position = pieceOldPosition;
        pieceCanvas.sortingOrder = 10;
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
}
