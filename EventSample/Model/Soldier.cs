using System;
using EventSample.EventMessage;
using EventSample.PubSubInterface;

namespace EventSample.Model
{
    public class Soldier : IPublisher, ISubscriber
    {
        public string Name { get; set; }

        //Only one person as my Commander
        public ISubscriber Commander { get; set; }

        public MapPoint CurrentMapPoint { get; set; }
        private IChangeManager _changeManager;

        public Soldier(string name)
        {
            Name = name;
            CurrentMapPoint = new MapPoint(0, 0);
        }

        public Soldier(string name, IChangeManager manager)
        {
            Name = name;
            CurrentMapPoint = new MapPoint(0, 0);
            _changeManager = manager;
        }

        public MapPoint GetMapPoint()
        {
            return CurrentMapPoint;
        }

        public void Subscribe(ISubscriber subscriber)
        {
            Commander = subscriber;
        }

        public void OnReceived(object message)
        {
            if (message != null && message.GetType() == typeof(CommanderMessage))
            {
                var value = (CommanderMessage)message;
                //When got event
                CurrentMapPoint = value.MapPoint;
                //Report to Commander
                if (this.Commander != null)
                {
                    this.Commander.OnReceived(new SoldierMessage()
                    {
                        Soldier = this
                    });
                }
                if (_changeManager != null)
                {
                    _changeManager.OnPodcastMessage("solider", new SoldierMessage()
                    {
                        Soldier = this
                    }, this);
                }
            }
        }
    }
}