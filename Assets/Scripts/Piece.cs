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




    public void ChangeSprite(int value, bool faceUp)
    {
        if (faceUp == false)
        {
            pieceSprite.sprite = faceDown;
        }

        else
        {
            if (value != 0) pieceSprite.enabled = true;

            switch (value)
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
                default:
                    Debug.Log("piece sprite out of bounds"); break;

            }
        }
    }


private void Cell_OnDrag(object sender, EventArgs e)
        //not working
    {
        pieceSprite.transform.position = (Vector2)Input.mousePosition;
    }

    
}
