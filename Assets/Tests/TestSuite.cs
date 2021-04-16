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
        Assert.AreEqual(testBoard1[0].value, 1);
    }

}
