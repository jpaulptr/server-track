using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTrack.Models;
using System.Collections.Generic;

namespace ServerTrack.Tests
{
    [TestClass]
    public class StatAgregationGeneratorFixturet
    {
        [TestMethod]
        public void TestAggregatedMinutes()
        {

            var statAgregationGenerator = new StatAgregationGenerator(new DateTime(2016, 9, 24, 11, 15, 0));

            var stats = new List<IServerStat>();

            //EdgeCase: 1 minute Too Early
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 10, 14, 13)
            });

            //EdgeCase: 1 minute Too Late
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 11, 16, 13)
            });


            //Stat at 9/24/2016T10:15:13
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 10, 15, 13)
            });

            //Stat at 9/24/2016T10:15:34
            stats.Add(new ServerStat
            {
                CPULoad = 5.1,
                RAMLoad = 15.1,
                EventTime = new DateTime(2016, 9, 24, 10, 15, 34)
            });

            //Stat at 9/24/2016T10:16:13
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 10, 16, 13)
            });

            //Stat at 9/24/2016T10:16:34
            stats.Add(new ServerStat
            {
                CPULoad = 5.1,
                RAMLoad = 15.1,
                EventTime = new DateTime(2016, 9, 24, 10, 16, 34)
            });

            var result = statAgregationGenerator.GetAggregateStats(stats);

            Assert.AreEqual(result.LoadLastHour.Count, 2);
            Assert.AreEqual(result.LoadLastHour[0].AverageCPULoad, 7.6);
            Assert.AreEqual(result.LoadLastHour[0].AverageRAMLoad, 17.6);

            Assert.AreEqual(result.LoadLastDay.Count, 1);
            Assert.AreEqual(result.LoadLastDay[0].AverageCPULoad, 8.1);
            Assert.AreEqual(result.LoadLastDay[0].AverageRAMLoad, 18.1);
        }

        [TestMethod]
        public void TestAggregatedHours()
        {

            var statAgregationGenerator = new StatAgregationGenerator(new DateTime(2016, 9, 24, 11, 15, 0));

            var stats = new List<IServerStat>();

            //EdgeCase: 1 hour Too Early
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 23, 10, 14, 13)
            });

            //EdgeCase: 1 hour Too Late
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 12, 16, 13)
            });

            //Hour 10
            //Stat at 9/24/2016T10:15:13
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 10, 15, 13)
            });

            //Stat at 9/24/2016T10:15:34
            stats.Add(new ServerStat
            {
                CPULoad = 5.1,
                RAMLoad = 15.1,
                EventTime = new DateTime(2016, 9, 24, 10, 15, 34)
            });

            //Hour 9
            //Stat at 9/24/2016T9:16:13
            stats.Add(new ServerStat
            {
                CPULoad = 10.1,
                RAMLoad = 20.1,
                EventTime = new DateTime(2016, 9, 24, 9, 16, 13)
            });

            //Hour 12, 
            //Stat at 9/23/2016T12:16:34
            stats.Add(new ServerStat
            {
                CPULoad = 5.1,
                RAMLoad = 15.1,
                EventTime = new DateTime(2016, 9, 23, 12, 14, 34)
            });

            var result = statAgregationGenerator.GetAggregateStats(stats);

            Assert.AreEqual(result.LoadLastDay.Count, 3);
            Assert.AreEqual(result.LoadLastDay[0].AverageCPULoad, 7.6);
            Assert.AreEqual(result.LoadLastDay[0].AverageRAMLoad, 17.6);
        }

    }
}
