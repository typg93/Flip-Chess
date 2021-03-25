using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IPointerClickHandler
{

    private int value;
    private Piece piece;
    public event EventHandler OnChangeValue;

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
        OnChangeValue?.Invoke(this, EventArgs.Empty);
    }
}
