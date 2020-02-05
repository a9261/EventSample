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
            //Refer : https://github.com/csparpa/gof-design-patterns/blob/master/java/src/tk/csparpa/gofdp/observer/variants/Demo.java
            var manager = new ChangeManager();
            var commander = new Commander("Golden", manager);
            var soldierOne = new Soldier("Harry", manager);
            var soldierTwo = new Soldier("Paul", manager);

            manager.RegisterPublisher("solider", soldierOne);
            manager.RegisterPublisher("solider", soldierTwo);
            manager.RegisterSubscriber("solider", commander);

            manager.RegisterPublisher("commander", commander);
            manager.RegisterSubscriber("commander", soldierOne);
            manager.RegisterSubscriber("commander", soldierTwo);

            commander.SendCmd(new MapPoint(100, 100));

            Assert.AreEqual(2, commander.Soldiers.Count);
        }
    }
}