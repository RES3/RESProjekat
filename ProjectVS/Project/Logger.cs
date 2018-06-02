using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Logger
    {
        public static void Log(string text)
        {
            string path = @"..\Logger.txt";
            StringBuilder sb = new StringBuilder();

            sb.Append(text + "\n");

            try
            {
                // Add text to the file.
                if (!File.Exists(path))
                    File.WriteAllText(path, sb.ToString());
                else
                    File.AppendAllText(path, sb.ToString());

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
