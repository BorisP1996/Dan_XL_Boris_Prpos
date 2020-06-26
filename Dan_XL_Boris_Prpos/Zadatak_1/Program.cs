using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static List<Thread> threadList = new List<Thread>();
        static void Main(string[] args)
        {
            Document doc = new Document();

            for (int i = 0; i < 10; i++)
            {
                string temp = (i + 1).ToString();
                Thread t = new Thread(() => doc.CreateDocument(Thread.CurrentThread))
                {
                    Name=string.Format("Computer {0}",temp)
                };
                threadList.Add(t);
            }
            for (int i = 0; i < threadList.Count; i++)
            {
                threadList[i].Start();
                Thread.Sleep(100);
                threadList[i].Join();
            }
            Console.ReadLine();
        }
    }
}
