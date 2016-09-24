using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServerTrack.Models;

namespace ServerTrack.Controllers
{
    public class ServerController : ApiController
    {
        private IMemmoryManager memmoryManager;

        public ServerController(IMemmoryManager memmoryManager)
        {
            this.memmoryManager = memmoryManager;
        }

        public AgregateStatsSet Get(string id)
        {

            var statAgregationGenerator = new StatAgregationGenerator();

            return statAgregationGenerator.GetAggregateStats(this.memmoryManager.GetList()
                                                    .Where(s => s.ServerName == id)
                                                    .ToList());
        }

        [System.Web.Http.HttpPost]
        public void Post(ServerStat serverLoad)
        {
            this.memmoryManager.AddServerStat(serverLoad);
        }

    }
}
