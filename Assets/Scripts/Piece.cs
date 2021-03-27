using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour
{
    private bool visible;
    private Image pieceSprite;
    private Cell cell;

    public Sprite faceDown;
    public Sprite redOne, redTwo, redThree, redFour, redKing;
    public Sprite blueOne, blueTwo, blueThree, blueFour, blueKing;
    

    private void Awake()
    {
        pieceSprite = GetComponent<Image>();
        cell = GetComponentInParent<Cell>();
    }

    #region Subscribing Events
    private void OnEnable()
    {
        cell.OnChangeValue += Cell_OnChangeValue;
    }

    private void OnDisable()
    {
        cell.OnChangeValue -= Cell_OnChangeValue;
    }

    private void OnDestroy()
    {
        cell.OnChangeValue -= Cell_OnChangeValue;
    }
    #endregion

    public void ChangeVisibility(bool val)
    {
        if (val == true)
        {
            //To do
        }
        else
        {

        }
    }
    private void Cell_OnChangeValue(object sender, Cell.OnChangeValueEventArgs e)
    {
        //first check if piece is to be flipped up or down
        if (e.faceUp == false)
        {
            pieceSprite.sprite = faceDown;
        }

        else
        {
            if (e.value != 0) pieceSprite.enabled = true;

            switch (e.value)
            {
                case 0:
                    pieceSprite.enabled = false; break;
                case 1:
                    pieceSprite.sprite = redOne; break;
                case 2:
                    pieceSprite.sprite = redTwo; break;
                case 3:
                    pieceSprite.sprite = redThree; break;
                case 4:
                    pieceSprite.sprite = redFour; break;
                case 5:
                    pieceSprite.sprite = redKing; break;


                case -1:
                    pieceSprite.sprite = blueOne; break;
                case -2:
                    pieceSprite.sprite = blueTwo; break;
                case -3:
                    pieceSprite.sprite = blueThree; break;
                case -4:
                    pieceSprite.sprite = blueFour; break;
                case -5:
                    pieceSprite.sprite = blueKing; break;
            }
        }
        
    }

    private void Cell_OnDrag(object sender, EventArgs e)
    {

    }

    
}
