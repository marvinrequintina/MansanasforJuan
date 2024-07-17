using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MansanasForJuan
{
    class LeaderboardGame : Template
    {
        Panel mainPanel;
        private LeaderboardPanel lbp;
        private GameData playerData;
        private LeaderBoardData lbData;


        public LeaderboardGame(GameData playerData,int width, int height)
        {
            Console.WriteLine("Game LeaderBoard running...");
            this.playerData = playerData;
            mainPanel = GeneratePanel();
            mainPanel.Size = new Size(width, height);
            mainPanel.BackColor = Color.Transparent;
            Console.WriteLine("Fetching Game Data...");
            lbData = new LeaderBoardData(@"Data/LeaderBoard/RecordsAttempt.txt");
            MainControls(mainPanel);
            lbp.AddItems(GetData("Easy"));

        }


        private void MainControls(Control parent)
        {
            //LeaderBoard PHOTO
            PictureBox titlelb = new PictureBox();
            titlelb.Size = new Size(370, 72);
            titlelb.Image = Image.FromFile("Images/LeaderBoard.png");
            titlelb.BackColor = Color.Transparent;
            titlelb.Location = new Point(218, 20);
            titlelb.SendToBack();
            titlelb.Parent = parent;


            //Ranking PHOTO
            PictureBox rankLB = new PictureBox();
            rankLB.Size = new Size(65, 53);
            rankLB.BackColor = Color.Transparent;
            rankLB.Location = new Point(120, 93);
            rankLB.Image = Image.FromFile("Images/trp.png");
            rankLB.Parent = parent;

            //Name PHOTO
            PictureBox nameLB = new PictureBox();
            nameLB.Size = new Size(135, 53);
            nameLB.BackColor = Color.Transparent;
            nameLB.Image = Image.FromFile("Images/Name.png");
            nameLB.Location = new Point(190, 93);
            nameLB.Parent = parent;

            //Score PHOTO
            PictureBox scoreLB = new PictureBox();
            scoreLB.Size = new Size(135, 53);
            scoreLB.BackColor = Color.Transparent;
            scoreLB.Image = Image.FromFile("Images/score.png");
            scoreLB.Location = new Point(345, 93);
            scoreLB.Parent = parent;

            //Time PHOTO
            PictureBox timeLB = new PictureBox();
            timeLB.Size = new Size(135, 53);
            timeLB.BackColor = Color.Transparent;
            timeLB.Image = Image.FromFile("Images/time.png");
            timeLB.Location = new Point(500, 93);
            timeLB.Parent = parent;


            //create leaderboard panel
            lbp = new LeaderboardPanel();
            lbp.Parent = parent;

            //create sidebuttons
            Button easyBtn = GenerateButton(parent);
            easyBtn.Size = new Size(40, 100);
            easyBtn.Location = new Point(60, 150);
            easyBtn.BackColor = Color.SaddleBrown;
            easyBtn.Image = Image.FromFile("Images/e.png");
            easyBtn.Click += new EventHandler(EasyBtn_Click);

            Button medBtn = GenerateButton(parent);
            medBtn.Size = new Size(40, 100);
            medBtn.Location = new Point(60, 255);
            medBtn.BackColor = Color.SaddleBrown;
            medBtn.Image = Image.FromFile("Images/m.png");
            medBtn.Click += new EventHandler(NormalBtn_Click);

            Button hardBtn = GenerateButton(parent);
            hardBtn.Size = new Size(40, 100);
            hardBtn.Location = new Point(60, 360);
            hardBtn.BackColor = Color.SaddleBrown;
            hardBtn.Image = Image.FromFile("Images/h.png");
            hardBtn.Click += new EventHandler(HardBtn_Click);

            Button resetBtn = GenerateButton(parent);
            resetBtn.Size = new Size(52, 50);
            resetBtn.Location = new Point(800, 500);
            resetBtn.FlatStyle = FlatStyle.Flat;
            resetBtn.FlatAppearance.BorderSize = 0;
            resetBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            resetBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            resetBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            resetBtn.ImageAlign = ContentAlignment.MiddleCenter;
            resetBtn.Image = Image.FromFile("Images/reset.png");
            resetBtn.BackColor = Color.Transparent;
            resetBtn.Click += new EventHandler(ResetBtn_Click);

            //Back button
            Button backBtn = GenerateButton(parent);
            backBtn.Size = new Size(34, 34);
            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            backBtn.Image = Image.FromFile("Images/return.png");
            backBtn.FlatAppearance.MouseOverBackColor= Color.FromArgb(0, 255, 255, 255);
            backBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            backBtn.Location = new Point(10, 10);
            backBtn.Click += new EventHandler(BackBtn_Click);
        }

        private void EasyBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Fetching Easy Leaderboard data...");
            lbp.ResetItems();
            lbp.AddItems(GetData("Easy"));
        }


        private void NormalBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Fetching Normal Leaderboard data...");
            lbp.ResetItems();
            lbp.AddItems(GetData("Normal"));
        }

        private void HardBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Fetching Normal Leaderboard data...");
            lbp.ResetItems();
            lbp.AddItems(GetData("Hard"));
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Resetting Leaderboard data...");
            lbp.ResetItems();
            lbData.ClearLeaderBoardData();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Returning to Game Difficulty panel...");
            Control parent = mainPanel.Parent;
            parent.Controls[0].Dispose();
            parent.Controls.Clear();
            GameDifficulty gameDifficulty = new GameDifficulty(playerData, parent.Width, parent.Height);
            parent.Controls.Add(gameDifficulty.GetDifficultyInstance());
        }

        public Panel GetInstance()
        {
            return mainPanel;
        }

        public List<PlayerScores> GetData(string difficulty)
        { 
            var sortedList = lbData.SortedLeaderBoard(difficulty);
            var topTen = new List<PlayerScores>();
            var counter = 0;
            foreach (var data in sortedList)
            {
                if (counter == 10)
                {
                    break;
                }
                var player = new PlayerScores(counter+1, data[1], Convert.ToInt32(data[2]), Convert.ToDouble(data[3]));
                topTen.Add(player);
                counter++;
            }
            return topTen;
        }
    }

    public class PlayerScores
    {
        public string playerName;
        public int playerRank;
        public int score;
        public double time;

        public PlayerScores(int playerRank,string playerName, int score, double time)
        {
            this.playerRank = playerRank;
            this.playerName = playerName;
            this.score = score;
            this.time = time;
        }
    }

    class LeaderboardPanel : FlowLayoutPanel
    {
        public LeaderboardPanel()
        {
            //FlowLayout Panel
            this.FlowDirection = FlowDirection.TopDown;
            this.Size = new Size(580, 310);
            this.BackColor = Color.FromArgb(70, Color.GhostWhite);
            this.WrapContents = false;
            this.Location = new Point(120, 150);
            this.SendToBack();
            
        }
        public void AddItems(List<PlayerScores> scores)
        {
            foreach (var item in scores)
            {
                this.Controls.Add(new LeaderboardItem(item.playerRank, item.playerName, item.score, item.time));
            }
        }

        //Use to clear leaderboard before displaying different leaderboard difficulty
        public void ResetItems()
        {
            this.Controls.Clear();
        }
    }
    class LeaderboardItem : Panel
    {
        public LeaderboardItem(int rank, string name, int score, double time)
        {
            //Label Panel
            Label labelRank = new Label();
            labelRank.Text = rank.ToString();
            labelRank.BackColor = Color.Transparent;
            labelRank.Size = new Size(50, 25);
            labelRank.TextAlign = ContentAlignment.MiddleCenter;
            labelRank.Left = 5;
            labelRank.Font = new Font("Helvetica", 15, FontStyle.Bold);

            Label labelName = new Label();
            labelName.Text = name;
            labelName.Left = 76;
            labelName.BackColor = Color.Transparent;
            labelName.Size = new Size(120, 25);
            labelName.TextAlign = ContentAlignment.MiddleCenter;
            labelName.Font = new Font("Times New Roman", 15, FontStyle.Bold);


            Label labelScore = new Label();
            labelScore.Text = score.ToString();
            labelScore.Left = 240;
            labelScore.BackColor = Color.Transparent;
            labelScore.TextAlign = ContentAlignment.MiddleCenter;
            labelScore.Font = new Font("Times New Roman", 15, FontStyle.Bold);

            Label labelTime = new Label();
            TimeSpan timeTaken = TimeSpan.FromSeconds(time);
            string timeStr = "";
            if (timeTaken.Hours > 0)
                timeStr = String.Format("{0:D2} hrs:{1:D2} mins:{2:D2} secs", timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds);
            else if(timeTaken.Minutes>0)
                timeStr = String.Format("{0:D2} mins:{1:D2} secs", timeTaken.Minutes, timeTaken.Seconds);
            else
                timeStr = String.Format("{0:D2} secs", timeTaken.Seconds);
            labelTime.Text = timeStr;
            labelTime.Size = new Size(150, 25);
            labelTime.Left = 373;
            labelTime.BackColor = Color.Transparent;
            labelTime.TextAlign = ContentAlignment.MiddleCenter;
            labelTime.Font = new Font("Times New Roman", 15, FontStyle.Bold);

            this.Height = 25;
            this.Width = 573;
            

            //Add to Forms
            this.Controls.Add(labelRank);
            this.Controls.Add(labelName);
            this.Controls.Add(labelScore);
            this.Controls.Add(labelTime);
        }
    }
}
