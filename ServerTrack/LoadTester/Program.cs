using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LoadTester
{
    class Program
    {
        static void Main(string[] args)
        {

            var ls = new LoadSender("test", 1000);
            Thread thread = new Thread(new ThreadStart(ls.Run));
            thread.Start();

            var ls2 = new LoadSender("test2", 1000);
            Thread thread2 = new Thread(new ThreadStart(ls2.Run));
            thread2.Start();

            var ls3 = new LoadSender("test3", 1000);
            Thread thread3 = new Thread(new ThreadStart(ls3.Run));
            thread3.Start();

            while (thread.IsAlive || thread2.IsAlive || thread3.IsAlive) 
 



            Console.ReadLine();

        }


    }
}
