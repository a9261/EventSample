using System;
using EventSample.EventMessage;

namespace EventSample.Model
{
    public class Soldier : IObserver<CommanderMessage>
    {
        public string Name { get; set; }

        public Commander Commander { get; set; }
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
            //When got event
            CurrentMapPoint = value.MapPoint;
        }
    }
}