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

    private Cell endDragPiece;

    //Event for changing image in piece
    #region Events
    public event EventHandler<OnChangeValueEventArgs> OnChangeValue;
    public class OnChangeValueEventArgs : EventArgs
    {
        public int value;
        public bool faceUp;
    }
    public event EventHandler OnDragPiece;
    #endregion

    private void Awake()
    {
        piece = transform.GetComponentInChildren<Piece>();
    }


    public void ChangeValue(int value, bool faceUp)
        //value = value of the piece
        //faceUp = whether the piece is turned up or down
    {
        this.value = value;
        this.faceUp = faceUp;
        OnChangeValueEventArgs args = new OnChangeValueEventArgs();
        args.value = value;
        args.faceUp = faceUp;
        OnChangeValue?.Invoke(this, args);
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
            return false;
        }
        
        return true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDragPiece = eventData.pointerEnter.GetComponent<Cell>();
        if (ValidMove(endDragPiece, this)){
            endDragPiece.ChangeValue(value, true);
            ChangeValue(0, true);
        }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Cell endCell = eventData.pointerClick.GetComponent<Cell>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragPiece?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (faceUp == false) ChangeValue(true);
    }
}
