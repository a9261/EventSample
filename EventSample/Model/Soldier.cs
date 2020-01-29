using System;
using EventSample.EventMessage;

namespace EventSample.Model
{
    public class Soldier : IObserver<CommanderMessage>, IObservable<SoldierMessage>
    {
        public string Name { get; set; }

        //Only one person as my Commander
        public IObserver<SoldierMessage> Commander { get; set; }

        public MapPoint CurrentMapPoint { get; set; }

        public Soldier(string name)
        {
            Name = name;
            CurrentMapPoint = new MapPoint(0, 0);
        }

        public MapPoint GetMapPoint()
        {
            return CurrentMapPoint;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(CommanderMessage value)
        {
            if (value != null)
            {
                //When got event
                CurrentMapPoint = value.MapPoint;
                //Report to Commander
                if (this.Commander != null)
                    this.Commander.OnNext(new SoldierMessage()
                    {
                        Soldier = this
                    });
            }
        }

        public IDisposable Subscribe(IObserver<SoldierMessage> observer)
        {
            Commander = observer;
            return new UnSubscribe<SoldierMessage>(observer);
        }
    }
}