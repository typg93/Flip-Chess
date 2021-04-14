using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ----BitBoard variables----
// - uses bitboard data structure to store board position for AI search
// - flattens data in Cell Class into uint32 to represent pieces locations in binary
// - board position to bitboard uint32 digit index:
//| 24 | 25 | 26 | 27 | 28 | 29 | 30 | 31 |
//| 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 |
//| 08 | 09 | 10 | 11 | 12 | 13 | 14 | 15 |
//| 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 |


public enum Player
{
    Red = 1,
    Empty = 0,
    Blue = -1
}

public enum CellValue
{
    Empty = 0,
    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    King = 5
}
