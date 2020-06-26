using System;
using System.Collections.Generic;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        //list for threads that will get A3 method
        static List<Thread> threadList3 = new List<Thread>();
        //list for threads that will get A4 method
        static List<Thread> threadList4 = new List<Thread>();
        static Random rnd = new Random();
        //two countdown events used to signal when lists have finished
        static CountdownEvent countdown1 = new CountdownEvent(1);
        static CountdownEvent countdown2 = new CountdownEvent(1);

        static void Main(string[] args)
        {
            Document doc = new Document();         

            for (int i = 0; i < 10; i++)
            {
                //randomly chosing between A3 and A4 method
                int decideMethod = rnd.Next(3, 5);
                if (decideMethod == 3)
                {
                    string temp = (i + 1).ToString();
                    //giving name to threads
                    Thread t = new Thread(() => doc.CreateDocumentA3(Thread.CurrentThread))
                    {
                        Name = string.Format("Computer {0}", temp)
                    };
                    //adding them to A3 list
                    threadList3.Add(t);
                }
                //same with threads that will randomly get A4 method
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
            //creating threads that will execute list which contains threads who received A3 method
            Thread StartA3Thread = new Thread(() => StartA3(threadList3));
            //same here
            Thread StartA4Thread = new Thread(() => StartA4(threadList4));

            //starting both threads
            StartA3Thread.Start();
            Thread.Sleep(20);
            StartA4Thread.Start();

            Console.ReadLine();
        }
        /// <summary>
        /// Method executes thread from A3 list
        /// </summary>
        /// <param name="list3"></param>
        static void StartA3(List<Thread> list3)
        {
            for (int i = 0; i < list3.Count; i++)
            {
                list3[i].Start();
                //join so that printing goes one by one
                list3[i].Join();
                Thread.Sleep(100);
                //give signal when last thread from list is executed
                if (list3[i]==list3[list3.Count-1])
                {
                    countdown1.Signal();
                }
                //call delegeate when both lists are finished
                if (countdown1.IsSet && countdown2.IsSet)
                {
                    Delegate d = new Delegate();
                    d.Exit();
                }
            }
        }
        /// <summary>
        /// Method executes threads from A4 list
        /// </summary>
        /// <param name="list4"></param>
        static void StartA4(List<Thread> list4)
        {
            //same as in previous method
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
                //notify user that every printer has finished and close application
                Thread.Sleep(500);
                Console.WriteLine("\nEvery printer has printed. Application will stop after 5 seconds.");
                Thread.Sleep(5000);
                //closing console
                Environment.Exit(0);
            };
            OnNotification.Invoke();
        }
    }
}
