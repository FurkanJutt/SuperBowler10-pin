using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[TestFixture]
public class ActionMasterTest : MonoBehaviour
{
    //ActionMaster _actionMaster;
    private List<int> pinfalls;
    ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    ActionMaster.Action reset = ActionMaster.Action.Reset;
    ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    [SetUp]
    public void Setup()
    {
        //_actionMaster = new ActionMaster();
        pinfalls = new List<int>();
    }

    [Test]
    public void T00_PassingTest()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01_OneStrikeReturnEndTurn()
    {
        //Assert.AreEqual(endTurn, _actionMaster.Bowl(10));
        pinfalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinfalls));
    }

    [Test]
    public void T02_Bowl8ReturnsTidy()
    {
        pinfalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinfalls));
    }

    [Test]
    public void T03_8SpareTimes2ReturnsEndTurn()
    {
        //Assert.AreEqual(tidy, _actionMaster.Bowl(8));
        int[] rolls = { 8, 2};
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T04_CheckResetAtStrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T05_CheckResetAtStrikeInLastFrame()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9 };
        Assert.AreEqual(reset, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T06_RollsEndInEndGame() // Youtube Configuration
    {
        int[] rolls = { 8, 2, 7, 3, 3, 4, 10, 2, 8, 10, 10, 8, 0, 10, 8, 2, 9 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T07_GameEndsAtBowl20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T08_5AtBowl20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 5 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T09_0AtBowl20()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 0 };
        Assert.AreEqual(tidy, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T10_SpareAt2ndBowlReturnEndTurn()
    {
        int[] rolls = { 0, 10, 5, 1 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T11_SpareAt2ndBowlReturnEndTurn()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T12_ZeroOneGivesEndturn()
    {
        int[] rolls = { 0, 1 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }
}
