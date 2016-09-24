using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    public class StatAgregationGenerator
    {
        private DateTime currentTime;

        public StatAgregationGenerator() {
            currentTime = DateTime.Now;
        }

        public StatAgregationGenerator(DateTime CurrentTime) {
            currentTime = CurrentTime;
        }

        //a list of average load values by minute for 60 minutes
        //a list of average load values by hour for 24 hours
        public AgregateStatsSet GetAggregateStats(List<IServerStat> ServerStats)
        {
            var result = new AgregateStatsSet();


            //a list of average load values by minute for 60 minutes 
            result.LoadLastHour= Aggregate(ServerStats, TimeSpan.TicksPerMinute, currentTime.AddMinutes(-60));

            //a list of average load values by hour for 24 hours
            result.LoadLastDay = Aggregate(ServerStats, TimeSpan.TicksPerHour, currentTime.AddHours(-24));


            return result;
        }

        private List<AgregateStats> Aggregate(List<IServerStat> ServerStats, long Ticks, DateTime StartTime)
        {
            
            var groups = from p in ServerStats

                         where p.EventTime < currentTime && p.EventTime > StartTime

                         group p by p.EventTime.AddTicks(-(p.EventTime.Ticks % Ticks))
                          

                         into g

                         select new AgregateStats
                         {
                             Epoch = g.Key,

                             AverageCPULoad = g.Average(p => p.CPULoad),

                             AverageRAMLoad = g.Average(p => p.RAMLoad)
                         };

           return groups.ToList();
        }

    }

    public class AgregateStats
    {
        public DateTime Epoch { get; set;}
        public double AverageCPULoad { get; set;}
        public double AverageRAMLoad { get; set; }
    }

    public class AgregateStatsSet
    {
        public List<AgregateStats> LoadLastHour { get; set; }
        public List<AgregateStats> LoadLastDay { get; set; }
    }

}