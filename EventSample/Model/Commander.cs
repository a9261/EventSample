using System;
using System.Collections.Generic;
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

        public Commander(string name)
        {
            Name = name;
            Soldiers = new List<Soldier>();
            ReportResult = new Dictionary<string, MapPoint>();
            observers_soldier = new List<ISubscriber>();
        }

        public void SendCmd(MapPoint mapPoint)
        {
            foreach (var observer in observers_sodier)
            {
                observer.OnNext(new CommanderMessage()
                {
                    MapPoint = mapPoint,
                    Commander = this
                });
            }
        }

        public void OnNext(SoldierMessage value)
        {
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

        public void Subscribe(ISubscriber subscriber)
        {
            if (!observers_soldier.Contains(subscriber))
            {
                observers_soldier.Add(subscriber);
            }
        }

        public void Received(object message)
        {
            throw new NotImplementedException();
        }
    }
}