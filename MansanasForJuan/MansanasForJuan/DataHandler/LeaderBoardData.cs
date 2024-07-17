using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace MansanasForJuan
{
    public class LeaderBoardData
    {
        private Dictionary<string, List<string[]>> leaderboardData;
        private string filepath;
        public LeaderBoardData(string filepath)
        {
            this.filepath = filepath;
            leaderboardData = new Dictionary<string, List<string[]>>();
            InitializeData(filepath);

        }

        //adds new player data if name is not existing in the current game data
        public void InitializeData(string filepath)
        {
            try
            {
                using (var fileContent = new StreamReader(filepath))
                {
                    string line;
                    while ((line = fileContent.ReadLine()) != null)
                    {
                        var lineData = line.Trim('\n').Split(';');
                        if (!leaderboardData.ContainsKey(lineData[0]))
                        {
                            leaderboardData[lineData[0]] = new List<String[]>();
                        }
                        leaderboardData[lineData[0]].Add(lineData);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        //returns leaderboard data base on difficulty
        private List<string[]> GetLeaderboardData(string difficulty)
        {
            if (!leaderboardData.ContainsKey(difficulty))
            {
                MessageBox.Show(string.Format("{0} Leaderboard data is empty!",difficulty), "Inform System!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return new List<string[]>();
            }
            
            return leaderboardData[difficulty];
        }

        //resets the leaderboard
        public void ResetData()
        {
            leaderboardData.Clear();
        }

        //returns a sorted version of the leaderboard base on the players scores (Descending order) then by the players time  (Ascending order)
        public List<string[]> SortedLeaderBoard(string difficulty)
        {
            var listToSort = this.GetLeaderboardData(difficulty);
            //sort by player score then player time
            var sortedList = listToSort.OrderByDescending(arr => Convert.ToInt32(arr[2])).ThenBy(arr => Convert.ToDouble(arr[3])).ToList<string[]>();

            return sortedList;
        }

        //clears leaderboard data
        public void ClearLeaderBoardData()
        {
            try
            {
                using (var data = File.CreateText(filepath)) //will overwrite the file to clear data
                {
                    //clears buffers
                    data.Flush();
                }
            }
            catch (Exception e) //tells that user the error using a msg box
            {
                MessageBox.Show(e.Message, "ERROR OCCURED!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            ResetData();
        }

    }
}
