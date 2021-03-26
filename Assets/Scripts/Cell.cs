using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    private int value;

    //The children piece logic attached to this gameobject.
    private Piece piece;

    //Event for changing image in piece
    #region Events
    public event EventHandler<OnChangeValueEventArgs> OnChangeValue;
    public class OnChangeValueEventArgs : EventArgs
    {
        public int value;
    }
    #endregion

    private void Awake()
    {
        piece = transform.GetComponentInChildren<Piece>();
    }


    public void ChangeValue(int value)
    {
        this.value = value;
        OnChangeValue?.Invoke(this, new OnChangeValueEventArgs { value = value });
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
