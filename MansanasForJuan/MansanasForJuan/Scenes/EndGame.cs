using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class EndGame:Template
    {
        private Panel endGame;
        private string status;
        private GameData playerData;
        private int appleEaten;
        private int days;
        private int totalScore;
        private TimeSpan timeTaken;
        private string difficulty;

        public EndGame(int width, int height, string status,int days, int appleEaten, TimeSpan timeTaken, string difficulty,
            GameData playerData)
        {
            Console.WriteLine("End Game is running");
            Console.WriteLine("Tabulating Scores.....");
            SoundSystem.PlayLoop("BgMusic");
            this.playerData = playerData;
            this.appleEaten = appleEaten;
            this.days = days;
            this.totalScore = CalculateScore();
            //store
            this.playerData.ScoreInfo = totalScore;
            this.playerData.TimeDurationInfo = timeTaken;
            this.timeTaken = timeTaken;
            this.status = status;
            this.difficulty = difficulty;

            FileManagement fileManagement = new FileManagement();
            if (playerData.ModeAccessInfo < 3 && status == "Survived!")
            {
                playerData.ModeAccessInfo += 1;
                Dictionary<string, int> dict = fileManagement.RetrieveData("Data/Player/Player.txt");
                dict[playerData.PlayerNameInfo] = playerData.ModeAccessInfo;
                fileManagement.SaveData("Data/Player/Player.txt", dict);
            }

            //update access and saved to file
            double totalSecs = this.playerData.TimeDurationInfo.TotalSeconds;

            //saved data for game data
            fileManagement.SaveData(@"Data/LeaderBoard/RecordsAttempt.txt", difficulty,playerData.PlayerNameInfo, playerData.ScoreInfo, totalSecs);


            endGame = GeneratePanel();
            endGame.Size = new Size(width, height);
            endGame.BackColor = Color.Transparent;
            InitializeControls();
        }
        
        private void InitializeControls()
        {
            Panel header = GeneratePanel(endGame);
            header.Size = new Size(240, 50);
            header.BackColor = Color.Brown;
            header.Location = new Point(0, 90);
            CenterControl(header);


            //label
            Label mainStatus = GenerateLabel(header,this.status,Color.White,new Font("Arial", 30f,FontStyle.Bold));
            mainStatus.AutoSize = true;
            CenterControl(mainStatus);

            //Panel
            Panel status = GeneratePanel(endGame);
            status.Size = new Size(500, 250);
            status.BackColor = Color.White;
            status.Location = new Point(0, 125);
            CenterControl(status);
            Label apple = GenerateLabel(status, String.Format("Apple Eaten: {0}",appleEaten), Color.Black, new Font("Arial", 20f));
            apple.Location = new Point(3, 30);
            CenterControl(apple);
            Label day = GenerateLabel(status, String.Format("No. of Days: {0}",days), Color.Black, new Font("Arial", 20f));
            day.Location = new Point(3, 80);
            CenterControl(day);
            Label total = GenerateLabel(status, String.Format("Total Score: {0}",totalScore), Color.Black, new Font("Arial", 25f, FontStyle.Bold));
            total.Location = new Point(3, 130);
            CenterControl(total);
            String timeStr = "";

            if (timeTaken.Hours > 0)
                timeStr = String.Format("{0:D2} hrs:{1:D2} mins:{2:D2} secs", timeTaken.Hours, timeTaken.Minutes, timeTaken.Seconds);
            else if (timeTaken.Minutes > 0)
                timeStr = String.Format("{0:D2} mins:{1:D2} secs", timeTaken.Minutes, timeTaken.Seconds);
            else
                timeStr = String.Format("{0:D2} secs", timeTaken.Seconds);

            Label time  = GenerateLabel(status, timeStr, 
                Color.Black, new Font("Arial", 13f, FontStyle.Italic));
            time.Location = new Point(3, 140+total.Size.Height);
            CenterControl(time);
            mainStatus.AutoSize = true;;

            // retry btn
            Panel buttons = GeneratePanel(endGame);
            Button retry = GenerateButton(buttons);
            retry.Size = new Size(80, 80);
            retry.Image = Image.FromFile("Images/retry-btn.png");
            retry.Location = new Point(0, 5);
            retry.FlatStyle = FlatStyle.Flat;
            retry.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            retry.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            retry.BackColor = Color.Transparent;
            retry.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            //- > MainGame
            retry.Click += RetryButtonClick;

            // level btn
            Button levelBtn = GenerateButton(buttons);
            levelBtn.Size = new Size(80, 80);
            levelBtn.Image = Image.FromFile("Images/level-btn.png");
            levelBtn.Location = new Point(retry.Location.X+retry.Width+40, 5);
            levelBtn.BackColor = Color.Transparent;
            levelBtn.FlatStyle = FlatStyle.Flat;
            levelBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            levelBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            levelBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            // - > difficult
            levelBtn.Click += LevelButtonClick;

            // home btn
            Button homeBtn = GenerateButton(buttons);
            homeBtn.Size = new Size(80, 80);
            homeBtn.Image = Image.FromFile("Images/home-btn.png");
            homeBtn.Location = new Point(levelBtn.Location.X + levelBtn.Width + 40, 5);
            homeBtn.BackColor = Color.Transparent;
            homeBtn.FlatStyle = FlatStyle.Flat;
            homeBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            homeBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            homeBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            //-> Home
            homeBtn.Click += HomeButtonClick;

            buttons.AutoSize = true;
            buttons.Location = new Point(0, status.Location.Y + status.Height + 30);
            CenterControl(buttons);
        }
        private void RetryButtonClick(object sender, EventArgs e)
        {
            Control parent = endGame.Parent;
            MainGame mainGame = new MainGame(playerData, parent.Width, parent.Height, difficulty);
            parent.Controls.Clear();
            parent.Controls.Add(mainGame.GetGameInstance());
        }
        private void LevelButtonClick(object sender, EventArgs e)
        {
            Control parent = endGame.Parent;
            GameDifficulty nextPanel = new GameDifficulty(playerData, parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(nextPanel.GetDifficultyInstance());
        }
        private void HomeButtonClick(object sender, EventArgs e)
        {
            Control parent = endGame.Parent;
            GameHome nextPanel = new GameHome(parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(nextPanel.GetHomeInstance());
        }
        public Panel GetPanelInstance()
        {
            return endGame;
        }

        private int CalculateScore()
        {
            return days * appleEaten;
        }
    }
}
