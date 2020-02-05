using System;
using System.Collections.Generic;
using System.Data;
using EventSample.EventMessage;
using EventSample.PubSubInterface;

namespace EventSample.Model
{
    public class Commander : IPublisher, ISubscriber
    {
        public string Name { get; set; }

        public List<Soldier> Soldiers { get; set; }
        public Dictionary<string, MapPoint> ReportResult { get; set; }

        private List<ISubscriber> observers_soldier;

        private IChangeManager _changeManager;

        public Commander(string name)
        {
            Name = name;
            Soldiers = new List<Soldier>();
            ReportResult = new Dictionary<string, MapPoint>();
            observers_soldier = new List<ISubscriber>();
        }

        public Commander(string name, IChangeManager manager)
        {
            Name = name;
            Soldiers = new List<Soldier>();
            ReportResult = new Dictionary<string, MapPoint>();
            observers_soldier = new List<ISubscriber>();
            _changeManager = manager;
        }

        public void SendCmd(MapPoint mapPoint)
        {
            if (_changeManager != null)
            {
                _changeManager.OnPodcastMessage("commander", new CommanderMessage()
                {
                    MapPoint = mapPoint,
                    Commander = this
                }, this);
            }
            else
            {
                foreach (var observer in observers_soldier)
                {
                    observer.OnReceived(new CommanderMessage()
                    {
                        MapPoint = mapPoint,
                        Commander = this
                    });
                }
            }
        }

        public void Subscribe(ISubscriber subscriber)
        {
            if (!observers_soldier.Contains(subscriber))
            {
                observers_soldier.Add(subscriber);
            }
        }

        public void OnReceived(object message)
        {
            if (message.GetType() == typeof(SoldierMessage))
            {
                var value = (SoldierMessage)message;
                //When Commander received Soldier Reported
                if (value != null)
                {
                    //New Soldier
                    if (!this.Soldiers.Contains(value.Soldier))
                    {
                        this.Soldiers.Add(value.Soldier);
                    }
                    //Record Soldier Report Message
                    if (!this.ReportResult.ContainsKey(value.Soldier.Name))
                    {
                        this.ReportResult.Add(value.Soldier.Name, value.Soldier.CurrentMapPoint);
                    }
                    else
                    {
                        this.ReportResult[value.Soldier.Name] = value.Soldier.CurrentMapPoint;
                    }
                }
            }
        }
    }
}