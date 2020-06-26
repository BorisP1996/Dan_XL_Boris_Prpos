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
        public void CreateDocument(Thread t)
        {
            ReadColors();
            Random rnd = new Random();
            int orRan = rnd.Next(0, 2);
            string orientation = orientationArray[orRan];
            int forRan = rnd.Next(0, 2);
            string format = formatArray[forRan];
            int colRan = rnd.Next(0, 6);
            string color = colorList[colRan];

            Document document = new Document(color, orientation, format);
            Console.Write(t.Name);
            document.PrintRequest(document);
            Thread.Sleep(1000);
            Console.Write(t.Name);
            document.PrintNotification(document);

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
            Console.WriteLine(" has sent request to print document in {0} format. Color:{1}. Orientation:{2}",doc.format,doc.color,doc.orientation);
        }
        public void PrintNotification(Document doc)
        {
            Console.WriteLine(" user can take document in {0} format. Printing is finished.",doc.format);
        }

    }
}
