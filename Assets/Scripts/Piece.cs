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


    public void ChangeSprite(CellValue cellValue, Player player, bool faceUp)
    {
        if (faceUp == false)
        {
            pieceSprite.sprite = faceDown;
        }

        else
        {
            if (cellValue != CellValue.Empty) pieceSprite.enabled = true;
            else if (cellValue == CellValue.Empty) pieceSprite.enabled = false;

            if (player == Player.Red) {
                switch (cellValue)
                {
                    case CellValue.One:
                        pieceSprite.sprite = redOne; break;
                    case CellValue.Two:
                        pieceSprite.sprite = redTwo; break;
                    case CellValue.Three:
                        pieceSprite.sprite = redThree; break;
                    case CellValue.Four:
                        pieceSprite.sprite = redFour; break;
                    case CellValue.King:
                        pieceSprite.sprite = redKing; break;
                    default:
                        Debug.Log("piece sprite out of bounds"); break;
                }
            }
            else if (player == Player.Blue) {
                switch (cellValue)
                {
                    case CellValue.One:
                        pieceSprite.sprite = blueOne; break;
                    case CellValue.Two:
                        pieceSprite.sprite = blueTwo; break;
                    case CellValue.Three:
                        pieceSprite.sprite = blueThree; break;
                    case CellValue.Four:
                        pieceSprite.sprite = blueFour; break;
                    case CellValue.King:
                        pieceSprite.sprite = blueKing; break;
                    default:
                        Debug.Log("piece sprite out of bounds"); break;
                }
            
            }
        }
    }

    
}
