using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestSuite
{
    public AIBoardData CreateTestBoard1()
    {
        //| 0   | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 0   | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 0   | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1r^ | 0 | 0 | 0 | 0 | 0 | 0 | 0 |
        AICellData[] boardData = new AICellData[32];
        boardData[0] = new AICellData { value = CellValue.One, player = Player.Red, faceup = true };
        AIBoardData board = new AIBoardData(boardData, 1);
        return board;
    }

    public AIBoardData CreateTestBoardFaceDown1()
    {
        //| 0   | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1b. | 1b^ | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 2r. | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1r. | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        AICellData[] boardData = new AICellData[32];
        boardData[0] = new AICellData { value = CellValue.One, player = Player.Red, faceup = false };
        boardData[8] = new AICellData { value = CellValue.Two, player = Player.Red, faceup = false };
        boardData[16] = new AICellData { value = CellValue.One, player = Player.Blue, faceup = false };
        boardData[17] = new AICellData { value = CellValue.One, player = Player.Blue, faceup = true };
        AIBoardData board = new AIBoardData(boardData, 1);
        return board;
    }

    public AIBoardData CreateTestBoardFaceDownOnly1()
    {
        //| 0   | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1r. | 1r. | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1r. | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        //| 1r. | 0   | 0 | 0 | 0 | 0 | 0 | 0 |
        AICellData[] boardData = new AICellData[32];
        boardData[0] = new AICellData { value = CellValue.One, player = Player.Red, faceup = false };
        boardData[8] = new AICellData { value = CellValue.One, player = Player.Red, faceup = false };
        boardData[16] = new AICellData { value = CellValue.One, player = Player.Red, faceup = false };
        boardData[17] = new AICellData { value = CellValue.One, player = Player.Red, faceup = false };
        AIBoardData board = new AIBoardData(boardData, 1);
        return board;
    }

    [Test]
    public void TestScanBoard()
    {
        AIBoardData testBoard = CreateTestBoard1();
        Assert.AreEqual(CellValue.One, testBoard.boardData[0].value);
    }

    [Test]
    public void CalculateFaceDownProbabilityZero()
    {
        AISearch ai = new AISearch();
        AIBoardData testBoard = CreateTestBoard1();
        Assert.AreEqual(0, ai.ProbabilityOfPieceFlip(testBoard,Player.Red, CellValue.One));
    }

    [Test]
    public void CalculateFaceDownProbability()
    {
        AISearch ai = new AISearch();
        AIBoardData testBoard = CreateTestBoardFaceDown1();
        Assert.AreEqual(1.0f/3, ai.ProbabilityOfPieceFlip(testBoard, Player.Red, CellValue.One));
    }

    [Test]
    public void CalculateFaceDownProbability100Percent()
    {
        AISearch ai = new AISearch();
        AIBoardData testBoard = CreateTestBoardFaceDownOnly1();
        Assert.AreEqual(1, ai.ProbabilityOfPieceFlip(testBoard, Player.Red, CellValue.One));
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
