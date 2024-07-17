using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MansanasForJuan
{
    class GameHelp : Template
    {
        Panel helpPanel;

        public GameHelp(int width, int height)
        {
            //Generate Help Panel
            Console.WriteLine("Game Help running...");
            helpPanel = GeneratePanel();
            helpPanel.Size = new Size(width, height);
            helpPanel.BackColor = Color.Transparent;
            HelpControls(helpPanel);
        }
        private void HelpControls(Control parent)
        {
            //Label
            int PosX = 20;
            Label titleLbl = GenerateLabel(parent);
            titleLbl.Text = "How to Play";
            titleLbl.Font = new Font("Arial", 30, FontStyle.Bold);
            titleLbl.ForeColor = Color.White;
            titleLbl.BackColor = Color.SaddleBrown;
            titleLbl.Location = new Point(PosX, 105);
            CenterControl(titleLbl);

            //Generate Panel
            Panel panel = GeneratePanel(parent);
            panel.AutoSize = true;
            panel.Location = new Point(0, 160);
            panel.BackColor = Color.White;

            //Label
            Label stepOneLbl = GenerateLabel(panel);
            stepOneLbl.Text = "- Each level you are given 5 lives";
            stepOneLbl.Font = new Font("Arial", 18, FontStyle.Bold);
            stepOneLbl.ForeColor = Color.Black;
            stepOneLbl.Location = new Point(PosX, 10);

            //Label
            Label stepTwoLbl = GenerateLabel(panel);
            stepTwoLbl.Text = "- Guess the hidden word by clicking the letters";
            stepTwoLbl.Font = new Font("Arial", 18, FontStyle.Bold);
            stepTwoLbl.ForeColor = Color.Black;
            stepTwoLbl.Location = new Point(PosX, 60);

            //Label
            Label stepThreeLbl = GenerateLabel(panel);
            stepThreeLbl.Text = "- If you guessed the word you will advance to the next level";
            stepThreeLbl.Font = new Font("Arial", 18, FontStyle.Bold);
            stepThreeLbl.ForeColor = Color.Black;
            stepThreeLbl.Location = new Point(PosX, 110);

            //Label
            Label stepFourLbl = GenerateLabel(panel);
            stepFourLbl.Text ="- Health is reduced by 1 each time you choose an incorrect letter";
            stepFourLbl.Font = new Font("Arial", 18, FontStyle.Bold);
            stepFourLbl.ForeColor = Color.Black;
            stepFourLbl.Location = new Point(PosX, 160);

            //Label
            Label stepFiveLbl = GenerateLabel(panel);
            stepFiveLbl.Text = "- Each remaining apple accumulates to the total score of the game";
            stepFiveLbl.Font = new Font("Arial", 18, FontStyle.Bold);
            stepFiveLbl.ForeColor = Color.Black;
            stepFiveLbl.Location = new Point(PosX, 210);

            //Label
            Label goodLuck = GenerateLabel(panel);
            goodLuck.Text = "Good Luck!";
            goodLuck.Font = new Font("Arial", 25, FontStyle.Bold);
            goodLuck.ForeColor = Color.Black;
            goodLuck.Location = new Point(PosX, 260);
            CenterControl(goodLuck);
            CenterControl(panel);

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
        private void BackButtonClick(object sender, EventArgs e)
        {
            //Go back to the Main Menu
            Control parent = helpPanel.Parent;
            GameHome gameHome = new GameHome(parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(gameHome.GetHomeInstance());
        }
        public Panel GetHelpInstance()
        {
            return helpPanel;
        }
    }
}
