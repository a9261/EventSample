namespace EventSample
{
    public class MapPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public MapPoint(double x, double y)
        {
            Latitude = x;
            Longitude = y;
        }
    }
}