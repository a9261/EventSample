using EventSample.Model;

namespace EventSample.EventMessage
{
    public class CommanderMessage
    {
        public MapPoint MapPoint { get; set; }
        public Commander Commander { get; set; }
    }
}