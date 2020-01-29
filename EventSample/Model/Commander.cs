using System;
using System.Collections.Generic;
using EventSample.EventMessage;

namespace EventSample.Model
{
    public class Commander : IObservable<CommanderMessage>
    {
        public string Name { get; set; }

        public List<Soldier> Soldiers { get; set; }
        public Dictionary<string, MapPoint> ReportResult { get; set; }

        private List<IObserver<CommanderMessage>> observers_sodier;
        private IDisposable cancellation;

        public Commander(string name)
        {
            Name = name;
            Soldiers = new List<Soldier>();
            ReportResult = new Dictionary<string, MapPoint>();
            observers_sodier = new List<IObserver<CommanderMessage>>();
        }

        public void SendCmd(MapPoint mapPoint)
        {
            foreach (var observer in observers_sodier)
            {
                observer.OnNext(new CommanderMessage()
                {
                    MapPoint = mapPoint
                });
            }
        }

        public IDisposable Subscribe(IObserver<CommanderMessage> observer)
        {
            if (!observers_sodier.Contains(observer))
            {
                observers_sodier.Add(observer);
            }
            return new UnSubscribe<CommanderMessage>(observer);
        }
    }
}