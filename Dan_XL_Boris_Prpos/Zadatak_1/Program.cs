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
        static List<Thread> threadList3 = new List<Thread>();
        static List<Thread> threadList4 = new List<Thread>();
        static Random rnd = new Random();
        static CountdownEvent countdown1 = new CountdownEvent(1);
        static CountdownEvent countdown2 = new CountdownEvent(1);

        static void Main(string[] args)
        {
            Document doc = new Document();         

            for (int i = 0; i < 10; i++)
            {
                int decideMethod = rnd.Next(3, 5);
                if (decideMethod == 3)
                {
                    string temp = (i + 1).ToString();
                    Thread t = new Thread(() => doc.CreateDocumentA3(Thread.CurrentThread))
                    {
                        Name = string.Format("Computer {0}", temp)
                    };
                    threadList3.Add(t);
                }
                if (decideMethod == 4)
                {
                    string temp = (i + 1).ToString();
                    Thread t = new Thread(() => doc.CreateDocumentA4(Thread.CurrentThread))
                    {
                        Name = string.Format("Computer {0}", temp)
                    };
                    threadList4.Add(t);
                }
            }

            Thread StartA3Thread = new Thread(() => StartA3(threadList3));
            Thread StartA4Thread = new Thread(() => StartA4(threadList4));

            StartA3Thread.Start();
            Thread.Sleep(20);
            StartA4Thread.Start();

            Console.ReadLine();
        }
      
        static void StartA3(List<Thread> list3)
        {
            for (int i = 0; i < list3.Count; i++)
            {
                list3[i].Start();
                list3[i].Join();
                Thread.Sleep(100);
                if (list3[i]==list3[list3.Count-1])
                {
                    countdown1.Signal();
                }
                if (countdown1.IsSet && countdown2.IsSet)
                {
                    Delegate d = new Delegate();
                    d.Exit();
                }
            }
        }
        static void StartA4(List<Thread> list4)
        {
            for (int i = 0; i < list4.Count; i++)
            {
                list4[i].Start();
                list4[i].Join();
                Thread.Sleep(100);
                if (list4[i]==list4[list4.Count-1])
                {
                    countdown2.Signal();
                }
                if (countdown1.IsSet && countdown2.IsSet)
                {
                    Delegate d = new Delegate();
                    d.Exit();
                }
            }
        }
    }
    class Delegate
    {
        public delegate void Notification();

        public event Notification OnNotification;

        public void Exit()
        {
            OnNotification += () =>
            {
                Thread.Sleep(500);
                Console.WriteLine("\nEvery printer has printed.Application will stop after 5 seconds.");
                Thread.Sleep(5000);
                Environment.Exit(0);
            };
            OnNotification.Invoke();
        }
    }
}
