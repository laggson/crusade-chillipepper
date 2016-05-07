using System;
using System.IO;
using Excel;
using System.Collections.Generic;
using FWA.Logic.Storage;
using System.Data;
using System.Text;

namespace FWA.Logic
{
    /// <summary>
    /// This class is temporary as long as the data is not copied to database
    /// This is probably the most sloppy implementation I have ever made...
    /// </summary>
    class ExcelImporter
    {
        private String[] filesToSearch;
        private List<Device> items;

        public ExcelImporter()
        {
            string path = @"D:\Eigene Dateien\Dokumente\Programmiertes\crusade-chillipepper\FWAdministraion\CSV\";
            items = new List<Device>();
            filesToSearch = new string[]
            {
                path + "TLF 3000.csv",
                path + "LF 10.csv",
                //path + "MTF.xlsx", //Not needed yet
                path + "Halle.csv"
            };
            //this.Import2();
        }

        //public List<Device> Import()
        //{
        //    List<Device> temp = new List<Device>();

        //    foreach (string file in filesToSearch)
        //    {
        //        FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
        //        IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(fs);

        //        //excelReader.IsFirstRowAsColumnNames = true;

        //        while (excelReader.Read())
        //        {
        //            string s = excelReader.GetString(0);
        //            Console.WriteLine(s);
        //        }
        //    }

        //    return temp;
        //}

        public List<Device> ImportLocalCSV()
        {
            List<Device> temp = new List<Device>();

            foreach(string file in filesToSearch)
            {
                StreamReader sr = new StreamReader(file, Encoding.UTF8);

                while (!sr.EndOfStream)
                {
                    string[] text = sr.ReadLine().Split(';');

                    if (text[0] != string.Empty)

                        temp.Add(new Device
                        {
                            ID = Convert.ToInt32(text[0]),
                            Name = text[1],
                            InvNumber = text[2],
                            NeedsCheckcard = Convert.ToBoolean(text[3]),
                            AnnualChecks = Convert.ToInt16(text[4]),
                            KindOfCheck = text[5],
                            Comment = text[6]
                        });
                }
            }

            return temp;
        }
    }
}
