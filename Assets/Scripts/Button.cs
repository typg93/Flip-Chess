using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour
{
    protected Board board;

    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }

}
