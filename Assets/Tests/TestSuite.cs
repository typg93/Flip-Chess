using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    

    [Test]
    public void TestScanBoard()
    {
        //| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 0 | 1rup | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 0 | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        AICellData[] testBoard1 = new AICellData[32];
        testBoard1[9] = new AICellData { value = CellValue.One, player = Player.Red, faceup = true };
        Assert.AreEqual(testBoard1[9].value, 1);
    }


    //[Test]
    //public void TestEvaluateInitialBoard()
    //{
    //    AISearch ai = new AISearch();
    //    AIPlayer aiPlayer = new AIPlayer();
    //    AICellData[] board = aiPlayer.ScanBoard();
    //    Assert.AreEqual(ai.EvaluatePosition(board), 0);
    //}
        
}
