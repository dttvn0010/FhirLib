using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MessageConverter;


namespace FhirEndpoint
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("bundle.txt");
            String json = reader.ReadToEnd();
            reader.Close();

            DichVuKyThuat dvkt = JsonParser.parseDVKTMessage(json);
            Console.WriteLine(dvkt);
        }
    }
}
