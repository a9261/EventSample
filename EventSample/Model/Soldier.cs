namespace EventSample.Model
{
    public class Soldier
    {
        public string Name { get; set; }

        public Commander Commander { get; set; }
        public MapPoint CurrentMapPoint { get; set; }

        public Soldier(string name)
        {
            Name = name;
            CurrentMapPoint = new MapPoint(0, 0);
        }

        public void Join(Commander commander)
        {
            Commander = commander;
            commander.Soldiers.Add(this);
        }

        public MapPoint GetMapPoint()
        {
            return CurrentMapPoint;
        }

        public MapPoint MoveTo(MapPoint mapPoint)
        {
            CurrentMapPoint = mapPoint;
            return CurrentMapPoint;
        }
    }
}