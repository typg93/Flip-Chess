using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    
    private int value;
    private bool faceUp;

    //The children piece script attached to this gameobject.
    private Piece piece;
    private GameObject pieceGO;
    private Vector3 pieceOldPosition;

    private Cell endDragPiece;

    

    private void Awake()
    {
        piece = transform.GetComponentInChildren<Piece>();
        pieceGO = piece.gameObject;
        
    }

    private void Start()
    {
        pieceOldPosition = pieceGO.transform.position;
        Debug.Log(pieceOldPosition.x + " " + pieceOldPosition.y);
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

    private bool ValidMove(Cell start, Cell end)
    {
        if (start == end)
        {
            Debug.Log("same piece");
            return false;
        }
        
        return true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDragPiece = eventData.pointerEnter.GetComponent<Cell>();
        if (ValidMove(endDragPiece, this))
        {
            endDragPiece.ChangeValue(value, true);
            ChangeValue(0, true);
        }
        else ResetPiece();
    }

    private void ResetPiece()
        //puts the sprite back in place
    {
        pieceGO.transform.position = pieceOldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cell endCell = eventData.pointerClick.GetComponent<Cell>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        pieceGO.transform.position = Input.mousePosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (faceUp == false) ChangeValue(true);
    }
}
