using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace LoadTester
{
    public class LoadSender
    {
        private const string endPoint = "http://localhost/api/server";
        private string serverName;
        private int iterations;

        public LoadSender(string ServerName, int Iterations)
        {
            serverName = ServerName;
            iterations = Iterations;
        }

        public void Run()
        {
            Random rnd = new Random();

            for (int i = 0; i < iterations; i++) {

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(endPoint);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
               
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = new JavaScriptSerializer().Serialize(new ServerStat
                    {
                        CPULoad = rnd.Next(1, 99),
                        RAMLoad = rnd.Next(1, 99) + (((double)rnd.Next(1, 99)) /100),
                        ServerName = serverName
                    });

                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Console.WriteLine("response written");
                }

            }

        }

    }

    public class ServerStat 
    {
        public string ServerName { get; set; }

        public double CPULoad { get; set; }

        public double RAMLoad { get; set; }
    }
}
