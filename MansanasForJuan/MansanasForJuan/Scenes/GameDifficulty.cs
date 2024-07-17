    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MansanasForJuan
{
    class GameDifficulty : Template
    {
        Panel difficultyPanel;
        Button easyBtn;
        Button normalBtn;
        Button hardBtn;
        Button lboardBtn;
        private GameData playerData;
        Button[] accessModeBtn =  new Button[3];

        public GameDifficulty(GameData playerData, int width, int height)
        {
            //Generate New Panel
            Console.WriteLine("Game Difficulty running...");
            this.playerData = playerData;   
            difficultyPanel = GeneratePanel();
            difficultyPanel.Size = new Size(width, height);
            difficultyPanel.BackColor = Color.Transparent;
            DifficultyControls(difficultyPanel);
        }
        private void DifficultyControls(Control parent)
        {
            //Name Label
            Label nameLbl = GenerateLabel(parent);
            nameLbl.Text = "Hello, " + playerData.PlayerNameInfo + "!";
            nameLbl.Font = new Font("Arial", 30, FontStyle.Bold);
            nameLbl.ForeColor = Color.White;
            nameLbl.BackColor = Color.FromArgb(255, 178, 74, 47);
            nameLbl.BringToFront();
            nameLbl.Location = new Point(0, 110);
            CenterControl(nameLbl);

            //Difficulty PictureBox
            PictureBox difficultyImage = new PictureBox();
            difficultyImage.ClientSize = new Size(700, 700);
            difficultyImage.Image = Image.FromFile(@"Images\DifficultyText.png");
            difficultyImage.BackColor = Color.Transparent;
            difficultyImage.SizeMode = PictureBoxSizeMode.Zoom;
            difficultyImage.SendToBack();
            difficultyImage.Location = new Point(90, -140);
            difficultyImage.Parent = parent;

            //Easy Button
            easyBtn = GenerateButton(parent);
            easyBtn.Size = new Size(190, 60);
            easyBtn.TabStop = false;
            easyBtn.FlatStyle = FlatStyle.Flat;
            easyBtn.FlatAppearance.BorderSize = 0;
            easyBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            easyBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            easyBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            easyBtn.Image = Image.FromFile(@"Images\Easy.png");
            easyBtn.BringToFront();
            easyBtn.Location = new Point(90, 280);
            easyBtn.Click += EasyButtonClick;
            easyBtn.MouseEnter += EasyButtonEnter;
            easyBtn.MouseLeave += EasyButtonLeave;
            easyBtn.Enabled = false;
            accessModeBtn[0] = easyBtn;

            //Normal Button
            normalBtn = GenerateButton(parent);
            normalBtn.Size = new Size(190, 60);
            normalBtn.TabStop = false;
            normalBtn.FlatStyle = FlatStyle.Flat;
            normalBtn.FlatAppearance.BorderSize = 0;
            normalBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            normalBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            normalBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            normalBtn.Image = Image.FromFile(@"Images\Normal.png");
            normalBtn.BringToFront();
            normalBtn.Location = new Point(340, 280);
            normalBtn.Click += NormalButtonClick;
            normalBtn.MouseEnter += NormalButtonEnter;
            normalBtn.MouseLeave += NormalButtonLeave;
            normalBtn.Enabled = false;
            accessModeBtn[1] = normalBtn;

            //Hard Button
            hardBtn = GenerateButton(parent);
            hardBtn.Size = new Size(190, 60);
            hardBtn.TabStop = false;
            hardBtn.FlatStyle = FlatStyle.Flat;
            hardBtn.FlatAppearance.BorderSize = 0;
            hardBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            hardBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            hardBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            hardBtn.Image = Image.FromFile(@"Images\Hard.png");
            hardBtn.BringToFront();
            hardBtn.Location = new Point(590, 280);
            hardBtn.Click += HardButtonClick;
            hardBtn.MouseEnter += HardButtonEnter;
            hardBtn.MouseLeave += HardButtonLeave;
            hardBtn.Enabled = false;
            accessModeBtn[2] = hardBtn;
            UpdateAccessButton();

            //Leaderboard Button
            lboardBtn = GenerateButton(parent);
            lboardBtn.Size = new Size(190, 60);
            lboardBtn.TabStop = false;
            lboardBtn.FlatStyle = FlatStyle.Flat;
            lboardBtn.FlatAppearance.BorderSize = 0;
            lboardBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            lboardBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            lboardBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            lboardBtn.Image = Image.FromFile(@"Images\Leaderboard.png");
            lboardBtn.BringToFront();
            lboardBtn.Location = new Point(340, 490);
            lboardBtn.Click += LeaderboardButtonClick;
            lboardBtn.MouseEnter += LeaderboardButtonEnter;
            lboardBtn.MouseLeave += LeaderboardButtonLeave;

            //Back Button
            Button backBtn = GenerateButton(parent);
            backBtn.Size = new Size(32, 32);
            backBtn.TabStop = false;
            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            backBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            backBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            backBtn.Image = Image.FromFile(@"Images\Back.png");
            backBtn.BringToFront();
            backBtn.Location = new Point(5, 5);
            backBtn.Click += BackButtonClick;
        }
        private void UpdateAccessButton()
        {
            for(int i=0; i<playerData.ModeAccessInfo; i++)
            {
                accessModeBtn[i].Enabled = true;
            }
        }

        private void EasyButtonClick(object sender, EventArgs e)
        {
            //Go to the Easy level
            Control parent = difficultyPanel.Parent;
            MainGame mainGame = new MainGame(playerData,parent.Width, parent.Height, "Easy");
            parent.Controls.Clear();
            parent.Controls.Add(mainGame.GetGameInstance());
        }
        private void NormalButtonClick(object sender, EventArgs e)
        {
            //Go to the Normal level
            Control parent = difficultyPanel.Parent;
            MainGame mainGame = new MainGame(playerData, parent.Width, parent.Height, "Normal");
            parent.Controls.Clear();
            parent.Controls.Add(mainGame.GetGameInstance());
        }
        private void HardButtonClick(object sender, EventArgs e)
        {
            //Go to the Hard level
            Control parent = difficultyPanel.Parent;
            MainGame mainGame = new MainGame(playerData, parent.Width, parent.Height, "Hard");
            parent.Controls.Clear();
            parent.Controls.Add(mainGame.GetGameInstance());
        }
        private void LeaderboardButtonClick(object sender, EventArgs e)
        {
            //Go to Leaderboard Panel
            Control parent = difficultyPanel.Parent;
            LeaderboardGame leaderboard = new LeaderboardGame(playerData, parent.Width, parent.Height);
            parent.Controls.Clear();
            parent.Controls.Add(leaderboard.GetInstance());
        }
        private void BackButtonClick(object sender, EventArgs e)
        {
            //Go back to the Main Menu
            Control parent = difficultyPanel.Parent;
            GameHome gameHome = new GameHome(parent.Size.Width, parent.Size.Height);
            parent.Controls[0].Dispose();
            parent.Controls.Clear();
            parent.Controls.Add(gameHome.GetHomeInstance());
        }
        private void EasyButtonEnter(object sender, EventArgs e)
        {
            easyBtn.Image = Image.FromFile(@"Images\EasyGold.png");
        }
        private void EasyButtonLeave(object sender, EventArgs e)
        {
            easyBtn.Image = Image.FromFile(@"Images\Easy.png");
        }
        private void NormalButtonEnter(object sender, EventArgs e)
        {
            normalBtn.Image = Image.FromFile(@"Images\NormalGold.png");
        }
        private void NormalButtonLeave(object sender, EventArgs e)
        {
            normalBtn.Image = Image.FromFile(@"Images\Normal.png");
        }
        private void HardButtonEnter(object sender, EventArgs e)
        {
            hardBtn.Image = Image.FromFile(@"Images\HardGold.png");
        }
        private void HardButtonLeave(object sender, EventArgs e)
        {
            hardBtn.Image = Image.FromFile(@"Images\Hard.png");
        }
        private void LeaderboardButtonEnter(object sender, EventArgs e)
        {
            lboardBtn.Image = Image.FromFile(@"Images\LeaderboardGold.png");
        }
        private void LeaderboardButtonLeave(object sender, EventArgs e)
        {
            lboardBtn.Image = Image.FromFile(@"Images\LeaderBoard.png");
        }
        public Panel GetDifficultyInstance()
        {
            return difficultyPanel;
        }
    }
}
