using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    public interface IServerStat
    {
        string ServerName { get; set; }

         double CPULoad { get; set; }

         double RAMLoad { get; set; }

         DateTime EventTime { get; set; }
    }

    public class ServerStat: IServerStat
    {
        public ServerStat()
        {
            EventTime = DateTime.Now;
        }

        public string ServerName { get; set; }

        public double CPULoad { get; set; }

        public double RAMLoad { get; set; }

        public DateTime EventTime { get; set; }
    }
}