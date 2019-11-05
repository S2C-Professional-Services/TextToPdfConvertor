using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextToPDFPlugin
{
    class Program
    {
        private static Thread MainThread { get; set; }
        static void Main(string[] args)
        {
            MainThread = new Thread(MainConvertion);
            //MainConvertion();
            MainThread.Start();
        }

        private static void MainConvertion()
        {
            DirectoryInfo dinfo = new DirectoryInfo(ConfigurationManager.AppSettings["DirectoryPath"]);
            FileInfo[] Files = dinfo.GetFiles("*" + ConfigurationManager.AppSettings["InputFileExtension"]);
            foreach (FileInfo file in Files)
            {
                GeneratePdfFile(file.FullName, file.Name);
            }
            MainThread.Abort();
        }

        private static void GeneratePdfFile(string path, string fileName)
        {
            try
            {
                using (StreamReader rdr = new StreamReader(path))
                {
                    Document doc = new Document();
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["OutputDirectoryPath"]);
                    PdfWriter.GetInstance(doc, new FileStream(ConfigurationManager.AppSettings["OutputDirectoryPath"] + @"\" + fileName.Replace(ConfigurationManager.AppSettings["InputFileExtension"], ".pdf"), FileMode.Create));

                    doc.Open();
                    doc.Add(new Paragraph(rdr.ReadToEnd()));
                    doc.Close();
                    rdr.Close();
                }
                string message = "Success-> DateTime: " + DateTime.Now + " Converted File: " + path;
                Console.WriteLine(message);
                LogService.LogDetails(message);
            }
            catch (Exception ex)
            {
                string message = "Fail-> DateTime: " + DateTime.Now + " Exception: " + ex.Message;
                Console.WriteLine(message);
                LogService.LogDetails(message);
            }
        }
    }
}
