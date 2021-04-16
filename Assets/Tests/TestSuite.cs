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
        testBoard1[9] = new AICellData { value = 1, player = Player.Red, faceup = true };
        Assert.AreEqual(testBoard1[9].value, 1);
    }

    [Test]
    public void TestEvaluateBoardEmpty()
    {
        AISearch ai = new AISearch();
        AICellData[] testBoard1 = new AICellData[32];
        Assert.AreEqual(ai.EvaluatePosition(testBoard1), 0);
    }

    [Test]
    public void TestEvaluateBoardOneCell()
    {
        AISearch ai = new AISearch();
        AICellData[] testBoard1 = new AICellData[32];
        testBoard1[1].value = 1;
        testBoard1[1].player = Player.Red;
        Assert.AreEqual(ai.EvaluatePosition(testBoard1), 1);
    }

    [Test]
    public void TestEvaluateBoardManyCells()
    {
        AISearch ai = new AISearch();
        AICellData[] testBoard1 = new AICellData[32];
        testBoard1[1].value = 1;
        testBoard1[1].player = Player.Red;
        testBoard1[10].value = 2;
        testBoard1[10].player = Player.Blue;
        testBoard1[11].value = 4;
        testBoard1[11].player = Player.Blue;
        Assert.AreEqual(ai.EvaluatePosition(testBoard1), -5);
    }
}
