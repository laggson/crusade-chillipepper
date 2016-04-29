using System;
using System.IO;
using Excel;
using System.Data;
using System.Collections.Generic;
using System.Windows;
using FWA.Logic.Storage;
using System.Windows.Controls;

namespace FWA.Logic
{
    /// <summary>
    /// This class is temporary as long as the data is not copied to database
    /// </summary>
    class ExcelImporter
    {
        private const string path = "D:\\Eigene Dateien\\Downloads\\Projects Backup\\Feuerwehr\\Prüfkarten";
        private DataSet set;
        private Window window;

        public ExcelImporter()
        {
            set = new DataSet();
            window = new Window();
            set.MergeFailed += Set_MergeFailed;
            this.SearchDir(path);
        }

        private void Set_MergeFailed(object sender, MergeFailedEventArgs e)
        {
            Console.WriteLine(e.Conflict);
        }

        private void SearchDir(string directory)
        {
            foreach (string subDirectory in Directory.GetDirectories(directory))
            {
                SearchDir(subDirectory);
                foreach (string file in Directory.GetFiles(subDirectory))
                    this.AddFile(file);
            }

            if (directory.Equals(path))
            {
                DataGrid dg = new DataGrid();
                Window w = window;
                w.Closing += W_Closing;
                w.Content = dg;
                w.Show();
                dg.ItemsSource = this.FilterDataSet();
            }
        }

        private void W_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DataGrid g = (DataGrid)window.Content;
            //Rows in Objekte umwandeln und speichern. Änderung muss per Hand
        }

        private List<Device> FilterDataSet()
        {
            List<Device> temp = new List<Device>();

            foreach (DataTable t in set.Tables)
            {
                foreach (DataRow r in t.Rows)
                {
                    if (r[2].ToString().Length == 16)
                    {
                        Device device = new Device()
                        {
                            Name = r[1].ToString(),
                            InvNumber = r[2].ToString()
                        };
                        temp.Add(device);
                    }
                }
            }

            return temp;

        }

        private void AddFile(string file)
        {
            FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = null;
            if (file.EndsWith(".xlsx"))
            {
                //Datei Format ist xlsx - OpenXml-Excel Datei auslesen
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (file.EndsWith(".xls"))
            {
                //Datei Format ist xls - Binäre Excel Datei auslesen
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }

            if (set == null)
            {
                set = excelReader?.AsDataSet();
            }
            else set.Merge(excelReader?.AsDataSet());

            excelReader?.Close();
        }
    }
}
}
