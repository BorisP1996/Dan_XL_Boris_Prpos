using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                ReadColors();
                Random rnd = new Random();
                int orRan = rnd.Next(0, 2);
                string orientation = orientationArray[orRan];
                string format = formatArray[0];
                int colRan = rnd.Next(0, 6);
                string color = colorList[colRan];

                Document document = new Document(color, orientation, format);
                Console.Write(t.Name);
                document.PrintRequest(document);
                Thread.Sleep(3000);
                Console.Write(t.Name);
                document.PrintNotification(document);
            }

        }
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
                Thread.Sleep(3000);
                Console.Write(t.Name);
                document.PrintNotification(document);
            }
        }
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
        public void PrintRequest(Document doc)
        {
            //Console.WriteLine(" has sent request to print document in {0} format. Color:{1}. Orientation:{2}",doc.format,doc.color,doc.orientation);
            Console.WriteLine(" {0} format Started",doc.format);
        }
        public void PrintNotification(Document doc)
        {
            Console.WriteLine(" {0} format finished.\n",doc.format);
        }

    }
}
