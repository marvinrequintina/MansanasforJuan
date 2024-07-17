using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class WordPool
    {
        //gets the words from the text file then stores it in a list of string array in a randomize way
        public List<string[]> GetWords(string level, int count)
        {
            Console.WriteLine("Fetching words from words pool data");
            var data = new List<string[]>();
            try
            {
                using (var fileContent = new StreamReader(string.Format(@"Data/Words/Levels/{0}.txt", level)))
                {
                    string line;
                    while ((line = fileContent.ReadLine()) != null)
                    {
                        var lineData = line.Trim('\n').Split(';');
                        data.Add(lineData);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            //shuffle
            // 5 
            var dataPicked = new List<string[]>();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                var randInd = rand.Next(0, data.Count);
                dataPicked.Add(data[randInd]);
                data.RemoveAt(randInd);
            }

            return dataPicked;
        }

    }
}
