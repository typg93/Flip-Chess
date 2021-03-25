using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IPointerClickHandler
{
    
    private int value;

    //The children piece logic attached to this gameobject.
    private Piece piece;

    //Event for changing image in piece
    public event EventHandler<OnChangeValueEventArgs> OnChangeValue;
    public class OnChangeValueEventArgs : EventArgs
    {
        public int value;
    }

    private void Awake()
    {
        piece = transform.GetComponentInChildren<Piece>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cell clicked");
    }

    public void ChangeValue(int value)
    {
        this.value = value;
        OnChangeValue?.Invoke(this, new OnChangeValueEventArgs { value = value });
    }
    
}
