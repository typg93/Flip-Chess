using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public int value;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeValue(int val)
    {
        switch (val)
        {
            case 0:
                //transparent piece to be implemented later
                break;
            case 1:
                pieceSprite.sprite = redOne;
                break;
            case 2:
                pieceSprite.sprite = redTwo;
                break;
            case 3:
                pieceSprite.sprite = redThree;
                break;
            case 4:
                pieceSprite.sprite = redFour;
                break;
            case 5:
                pieceSprite.sprite = redKing;
                break;

            case -1:
                pieceSprite.sprite = blueOne;
                break;
            case -2:
                pieceSprite.sprite = blueTwo;
                break;
            case -3:
                pieceSprite.sprite = blueThree;
                break;
            case -4:
                pieceSprite.sprite = blueFour;
                break;
            case -5:
                pieceSprite.sprite = blueKing;
                break;
        }
    }
}
