using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerTrack.Models
{
    public interface IMemmoryManager
    {
        void AddServerStat(IServerStat stat);

        List<IServerStat> GetList();
    }

    public class MemmoryManager : IMemmoryManager
    {
        private object _lock = new object();

        private List<IServerStat> serverStats;

        public MemmoryManager()
        {
            serverStats = new List<IServerStat>();
        }

        public void AddServerStat(IServerStat stat)
        {
            lock (_lock)
            {
                serverStats.Add(stat);
            }
        }

        public List<IServerStat> GetList()
        {
            lock (_lock)
            {
                return serverStats.ToList();
            }
        }

    }
}