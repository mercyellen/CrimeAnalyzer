using System;
using System.IO;

namespace CrimeAnalyzer
{
    class Program
    {

        static void Main(string[] args)
        {
            string file;
            string report;

            Console.WriteLine("Please enter in a file path for your csv file");

            file = GetValidDocument();

            Console.WriteLine("Now enter in a file path for the report file");

            report = Console.ReadLine();

            if(report == null) {
                Console.WriteLine("Enter in a report filename");
            }

            CrimeStats crimeStats = new CrimeStats();

            if (File.Exists(file))
            {
                crimeStats.Read(file);
                var fileReader = File.Create(report);
                fileReader.Close();
                crimeStats.linqWriting(report);
            }
            else
            {
                Console.WriteLine("Please enter in a CSV file that can be used with data that's not: {file}");
            }
        }

        static string GetValidDocument()
        {
            Console.Write("Enter the name of a document: ");
            string doc;
            while ((doc = Console.ReadLine()).Length == 0 || !File.Exists(doc))
            {
                Console.Write("Document not found, please enter a valid document name: ");
            }
            return doc;
        }
    }
}