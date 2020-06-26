using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Zadatak_1
{
    class Document
    {
        string color { get; set; }
        string orientation { get; set; }
        string format { get; set; }

        static object locker = new object();
        static object locker2 = new object();
        static string[] orientationArray = new string[] { "portrait", "landscape" };
        static string[] formatArray = new string[] { "A3", "A4" };
        public static List<string> colorList = new List<string>();
        static string path = @"../../Paleta.txt";

        public Document()
        {

        }
        public Document(string col, string or, string form)
        {
            color = col;
            orientation = or;
            format=form;
        }
        public void CreateDocumentA3(Thread t)
        {

            lock (locker)
            {
                //calling method to fill list with colors from file
                ReadColors();
                Random rnd = new Random();
                //random index to chose from orientation array
                int orRan = rnd.Next(0, 2);
                string orientation = orientationArray[orRan];
                //picking A3 format because method is for A3 printer
                string format = formatArray[0];
                int colRan = rnd.Next(0, 6);
                //randomly chosing color from list
                string color = colorList[colRan];

                //creating document and displaying messages using methods
                Document document = new Document(color, orientation, format);
                //writing thread name
                Console.Write(t.Name);
                //and then writing message thats connected with that thread=> start application to understand
                document.PrintRequest(document);
                Thread.Sleep(1000);
                //message that says : printing is over
                Console.Write("\t{0}",t.Name);
                document.PrintNotification(document);
            }

        }
        /// <summary>
        /// Same method as previous, but format is different
        /// </summary>
        /// <param name="t"></param>
        public void CreateDocumentA4(Thread t)
        {
            lock (locker2)
            {
                ReadColors();
                Random rnd = new Random();
                int orRan = rnd.Next(0, 2);
                string orientation = orientationArray[orRan];
                string format = formatArray[1];
                int colRan = rnd.Next(0, 6);
                string color = colorList[colRan];

                Document document = new Document(color, orientation, format);
                Console.Write(t.Name);
                document.PrintRequest(document);
                Thread.Sleep(1000);
                Console.Write("\t{0}",t.Name);
                document.PrintNotification(document);
            }
        }
        /// <summary>
        /// Method that reads colors from file
        /// </summary>
        public void ReadColors()
        {
            StreamReader sr = new StreamReader(path);
            string line = "";
            while ((line=sr.ReadLine())!=null)
            {
                colorList.Add(line);
            }
            sr.Close();
        }
        /// <summary>
        /// Method prints request for printing
        /// </summary>
        /// <param name="doc"></param>
        public void PrintRequest(Document doc)
        {
            Console.WriteLine(" has sent request to print document in {0} format. Color:{1}. Orientation:{2}",doc.format,doc.color,doc.orientation);         
        }
        /// <summary>
        /// Method prints notification that printing has finished
        /// </summary>
        /// <param name="doc"></param>
        public void PrintNotification(Document doc)
        {
            Console.WriteLine(" user can take {0} format document. Printing is finished.",doc.format);
        }

    }
}
