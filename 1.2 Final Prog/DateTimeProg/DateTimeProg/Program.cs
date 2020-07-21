using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DateTimeProg
{
    class Program
    {
        static void Main(string[] args)
        {

          // csv csv = new csv(".\\DateTime.csv");
          // csv.Write();
          csv readcsv = new csv(".\\Output.csv");
          readcsv.Read();          
        
            Console.ReadKey();

        }
    }
}
