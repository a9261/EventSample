using System.Collections.Generic;

namespace EventSample.Model
{
    public class Commander
    {
        public string Name { get; set; }

        public List<Soldier> Soldiers { get; set; }
        public Dictionary<string, MapPoint> ReportResult { get; set; }

        public Commander(string name)
        {
            Name = name;
            Soldiers = new List<Soldier>();
            ReportResult = new Dictionary<string, MapPoint>();
        }

        public void SendCmd(MapPoint mapPoint)
        {
            foreach (var soldier in Soldiers)
            {
                var moveResult = soldier.MoveTo(mapPoint);
                if (!ReportResult.ContainsKey(soldier.Name))
                {
                    ReportResult.Add(soldier.Name, moveResult);
                }
                else
                {
                    ReportResult[soldier.Name] = moveResult;
                }
            }
        }
    }
}