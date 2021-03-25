using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Piece : MonoBehaviour, IPointerClickHandler
{
    private int value = 0;
    private bool visible;
    private Image pieceSprite;

    public Sprite redOne, redTwo, redThree, redFour, redKing;
    public Sprite blueOne, blueTwo, blueThree, blueFour, blueKing;
    

    private void Awake()
    {
        pieceSprite = GetComponent<Image>();
    }

    void Start()
    {

    }
    

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

    public void ChangeValue(int val)
    {
        value = val;
        switch (val)
        {
            case 0:
                //transparent piece to be implemented later
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("piece cliked");
    }
}
