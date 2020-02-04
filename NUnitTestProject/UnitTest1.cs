using System;
using EventSample;
using EventSample.EventMessage;
using EventSample.Model;
using ExpectedObjects;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CommanderSendCmdToSoldierMove()
        {
            var expectedPoint = new MapPoint(100.3, 20.5).ToExpectedObject();

            var commander = new Commander("Golden");
            var soldierOne = new Soldier("Harry");
            var soldierTwo = new Soldier("Paul");

            commander.Subscribe(soldierOne);
            commander.Subscribe(soldierTwo);

            soldierOne.Subscribe(commander);
            soldierTwo.Subscribe(commander);

            commander.SendCmd(new MapPoint(100.3, 20.5));

            expectedPoint.ShouldEqual(soldierOne.GetMapPoint());
            expectedPoint.ShouldEqual(soldierTwo.GetMapPoint());
        }

        [Test]
        public void CommanderSoldierShoudbeTwo()
        {
            var commander = new Commander("Golden");
            var soldierOne = new Soldier("Harry");
            var soldierTwo = new Soldier("Paul");

            commander.Subscribe(soldierOne);
            commander.Subscribe(soldierTwo);

            soldierOne.Subscribe(commander);
            soldierTwo.Subscribe(commander);

            commander.SendCmd(new MapPoint(100.3, 20.5));
            Assert.AreEqual(2, commander.Soldiers.Count);
        }

        [Test]
        public void ChangeManagerTest()
        {
            var commander = new Commander("Golden");
            var soldierOne = new Soldier("Harry");
            var soldierTwo = new Soldier("Paul");

            var manager = new ChangeManager();
            manager.RegisterPublisher("commander", commander);
            manager.RegisterPublisher("solider", soldierTwo);

            manager.RegisterClient("commander", commander);

            commander.SendCmd(new MapPoint(100.3, 20.5));
            Assert.AreEqual(2, commander.Soldiers.Count);
        }
    }
}