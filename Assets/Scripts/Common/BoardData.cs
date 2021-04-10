using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ----BitBoard variables----
// - uses bitboard data structure to store board position
// - flattens data in Cell Class into uint32 to represent pieces locations in binary
// - board position to bitboard uint32 digit index:
//| 24 | 25 | 26 | 27 | 28 | 29 | 30 | 31 |
//| 16 | 17 | 18 | 19 | 20 | 21 | 22 | 23 |
//| 08 | 09 | 10 | 11 | 12 | 13 | 14 | 15 |
//| 00 | 01 | 02 | 03 | 04 | 05 | 06 | 07 |

public struct BitBoard
{
    public uint RedOnes;
    public uint RedTwos;
    public uint RedThrees;
    public uint RedFours;
    public uint RedKing;
    public uint BlueOnes;
    public uint BlueTwos;
    public uint BlueThrees;
    public uint BlueFours;
    public uint BlueKing;
    public uint FaceUp;
}
