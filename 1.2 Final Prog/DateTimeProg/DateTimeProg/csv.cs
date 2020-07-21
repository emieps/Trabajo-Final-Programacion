using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace DateTimeProg
{
    class csv
    {
        public string Path { get;  }

        public csv(string path)
           => (Path) = (path);

        public void Write()
        {
            long dateTotal = 0;
            Int32 tempTotal;
            String[] lines = File.ReadAllLines(this.Path);
            foreach (string line in lines)
            {
                String[] parts = line.Split(',');

                #region 
                string[] date = parts[0].Split('T', ':', '.'); /// '-'
                string[] date_1 = date[0].Split('-'); // year, month, day
                var tzone = Regex.Matches(date[4], @"-?\d+"); // mil, tzhour 

                #region
                long year = Int64.Parse(date_1[0].ToString());
                long month = Int64.Parse(date_1[1].ToString());
                long day = Int64.Parse(date_1[2].ToString());
                long hour = Int64.Parse(date[1].ToString());
                long min = Int64.Parse(date[2].ToString());
                long sec = Int64.Parse(date[3].ToString());
                long mil = Int64.Parse(tzone[0].ToString());
                long tzhour = Int64.Parse(tzone[1].ToString());
                long tzmin = Int64.Parse(date[5].ToString());
                #endregion

                
                dateTotal =  year;
                dateTotal = (dateTotal << 4) | month;
                dateTotal = (dateTotal << 5) | day;
                dateTotal = (dateTotal << 5) | hour;
                dateTotal = (dateTotal << 6) | min;
                dateTotal = (dateTotal << 6) | sec;
                dateTotal = (dateTotal << 10) | mil;

                if (tzhour < 0)
                {
                  
                    tzhour = (-1 * tzhour); 
                    dateTotal = (dateTotal << 1) | 0;
                    dateTotal = (dateTotal << 5) | (tzhour);
                   dateTotal = (dateTotal << 6) | tzmin;
                  
                }
                else
                {                    
                    dateTotal = (dateTotal << 1) | 1;
                    dateTotal = (dateTotal << 5) | tzhour;
                    dateTotal = (dateTotal << 6) | tzmin;
                }
                #endregion


                Int32 mintemp = Int32.Parse(parts[1].ToString());
                Int32 maxtemp = Int32.Parse(parts[2].ToString());
                Int32 prestemp = Int32.Parse(parts[3].ToString());

                tempTotal = prestemp;
                tempTotal = (tempTotal << 7) | maxtemp;
                tempTotal = (tempTotal << 7) | mintemp;


                using (StreamWriter file = File.AppendText(".\\Output.csv"))
                {
                    file.WriteLine($"{dateTotal},{tempTotal}");
                    file.Close();
                }
                

            }



        }

        public void Read()
        {
            string sigv = string.Empty;
            String[] lines = File.ReadAllLines(this.Path);
            foreach (string line in lines)
            {
                String[] parts = line.Split(',');

                long date = Convert.ToInt64(parts[0].ToString());
                
                string fecha = Convert.ToString(date, 2);

                int tzmin = Convert.ToInt32(fecha.Substring(fecha.Length - 6), 2);
                int tzhour = Convert.ToInt32(fecha.Substring(fecha.Length - 11, 5), 2);
                string sig = fecha.Substring(fecha.Length - 12, 1);
                if (sig == "0")
                {
                    sigv = "-";
                }
                else
                {
                    sigv = "+";
                }
                int milis = Convert.ToInt32(fecha.Substring(fecha.Length - 22, 10), 2);
                int seg = Convert.ToInt32(fecha.Substring(fecha.Length - 28, 6), 2);
                int min = Convert.ToInt32(fecha.Substring(fecha.Length - 34, 6), 2);
                int hour = Convert.ToInt32(fecha.Substring(fecha.Length - 39, 5), 2);
                int day = Convert.ToInt32(fecha.Substring(fecha.Length - 44, 5), 2);
                int month = Convert.ToInt32(fecha.Substring(fecha.Length - 48, 4), 2);
                int year = Convert.ToInt32(fecha.Substring(0, 11), 2);

                long temperatura = Convert.ToInt64(parts[1].ToString());
                string temp  = Convert.ToString(temperatura, 2);

                int mintemp = Convert.ToInt32(temp.Substring(temp.Length - 7, 7), 2);
                int maxtemp = Convert.ToInt32(temp.Substring(temp.Length - 14, 7), 2);
                int preci;

                if (temp.Length >= 21)
                {
                    preci = Convert.ToInt32(temp.Substring(0, 7), 2);
           
                }
                else
                {      
                    preci = Convert.ToInt32(temp.Substring(0, 5), 2);               
                }
                Console.WriteLine($"{year}-{month.ToString("00")}-" +
                    $"{day.ToString("00")}T" +
                    $"{hour.ToString("00")}" +
                    $":{min.ToString("00")}:" +
                    $"{seg.ToString("00")}." +
                    $"{milis.ToString("00")}" +
                    $"{sigv + tzhour.ToString("00")}" +
                    $":{tzmin.ToString("00")} ," +
                    $"{mintemp},{maxtemp}," +
                    $"{preci}");

            }
        }
    }
}
