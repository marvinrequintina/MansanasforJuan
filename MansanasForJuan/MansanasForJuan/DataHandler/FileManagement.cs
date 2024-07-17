using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class FileManagement
    {
        //Polymorphism
        //for game DATA
        public void SaveData(string file_path,string difficulty,string playerName,int score, double timeSec)
        {
            Console.WriteLine("Saving Data");
            try
            {
                using (var data = File.AppendText(file_path))
                {
                    data.WriteLine($"{difficulty};{playerName};{score};{timeSec}");
                }
            }
            catch (Exception e) //tells that user the error using a msg box
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void SaveData(string file_path, string name, int accessMode)
        {
            Console.WriteLine("Saving Data ....");
            try
            {
                using (var data = File.AppendText(file_path))
                {
                    data.WriteLine($"{name};{accessMode}");
                }
            }
            catch (Exception e) //tells that user the error using a msg box
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        //for player data
        //polymorphism
        public void SaveData(string file_path,Dictionary<string, int> dataList)
        {
            Console.WriteLine("Saving Data .....");
            try
            {
                using (var data = File.CreateText(file_path))
                {
                    foreach(var item in dataList)
                    {
                        data.WriteLine($"{item.Key};{item.Value}");
                    }
                }
            }
            catch (Exception e) //tells that user the error using a msg box
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public Dictionary<String, int> RetrieveData(string filePath, bool needUpper = false)
        {
            Console.WriteLine("Retrieving Data .....");
            Dictionary<string, int> dict = new Dictionary<string, int>(); 
            using (var fileContent = new StreamReader(filePath))
            {
                string line;
                while ((line = fileContent.ReadLine()) != null)
                {
                    var perLine = line.Trim('\n').Split(';');
                    if (perLine.Length < 2)
                    {
                        return dict;
                    }
                    var key = perLine[0];
                    if (needUpper)
                        key = key.ToUpper();
                    dict[key] = Convert.ToInt32(perLine[1]);
                }
                                        
            }
            return dict;
        }
        
    }
}
