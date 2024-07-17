using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace MansanasForJuan
{
    class GameInput : Template
    {
        Panel inputNamePanel;
        TextBox inputTxtBox;
        Button submitBtn;
        Validation validation;

        public GameInput(int width, int height)
        {
            //Generate Panel for input name
            Console.WriteLine("Game Input running...");
            validation = new Validation();
            inputNamePanel = GeneratePanel();
            inputNamePanel.Size = new Size(width, height);
            inputNamePanel.BackColor = Color.Transparent;
            InputNameControls(inputNamePanel);
        }
        private void InputNameControls(Control parent)
        {
            //Label
            Label guideLbl = GenerateLabel(parent);
            guideLbl.Text = "Enter your name";
            guideLbl.Font = new Font("Arial", 34, FontStyle.Bold);
            guideLbl.ForeColor = Color.White;
            guideLbl.BringToFront();
            guideLbl.Location = new Point(0, 220);
            CenterControl(guideLbl);

            //TextBox
            inputTxtBox = new TextBox();
            inputTxtBox.Size = new Size(220, 100);
            inputTxtBox.MaxLength = 12;
            inputTxtBox.TextAlign = HorizontalAlignment.Center;
            inputTxtBox.Font = new Font("Arial", 20, FontStyle.Bold);
            inputTxtBox.Location = new Point(0, 280);
            inputTxtBox.Parent = parent;
            CenterControl(inputTxtBox);

            //Button
            submitBtn = GenerateButton(parent);
            submitBtn.Size = new Size(190, 60);
            submitBtn.TabStop = false;
            submitBtn.FlatStyle = FlatStyle.Flat;
            submitBtn.FlatAppearance.BorderSize = 0;
            submitBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            submitBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            submitBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            submitBtn.Image = Image.FromFile(@"Images\Submit.png");
            submitBtn.Location = new Point(0, 400);
            submitBtn.Click += SubmitButtonClick;
            submitBtn.MouseEnter += SubmitButtonEnter;
            submitBtn.MouseLeave += SubmitButtonLeave;
            CenterControl(submitBtn);

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
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            //Validates user input
            string input = inputTxtBox.Text.Trim();
            if (!validation.ValidateInput(input))
            {
                inputTxtBox.Text = "";
                return;
            }
            //Checks if the user has saved data
            GameData playerData = new GameData(input);
            FileManagement fileManagement = new FileManagement();  ;
            Dictionary<string, int> data = fileManagement.RetrieveData("Data/Player/Player.txt",true);
            if (data.ContainsKey(playerData.PlayerNameInfo.ToUpper()))
            {
                playerData.ModeAccessInfo = data[playerData.PlayerNameInfo.ToUpper()];
            }
            else
            {
                fileManagement.SaveData("Data/Player/Player.txt", playerData.PlayerNameInfo, playerData.ModeAccessInfo);
            }
            //Go to the next panel
            Control parent = inputNamePanel.Parent;
            GameDifficulty nextPanel = new GameDifficulty(playerData, parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(nextPanel.GetDifficultyInstance());
        }
        private void BackButtonClick(object sender, EventArgs e)
        {
            //Go back to the Main Menu
            Control parent = inputNamePanel.Parent;
            GameHome gameHome = new GameHome(parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(gameHome.GetHomeInstance());
        }
        private void SubmitButtonEnter(object sender, EventArgs e)
        {
            submitBtn.Image = Image.FromFile(@"Images\SubmitGold.png");
        }
        private void SubmitButtonLeave(object sender, EventArgs e)
        {
            submitBtn.Image = Image.FromFile(@"Images\Submit.png");
        }
        public Panel GetInputNameInstance()
        {
            return inputNamePanel;
        }
    }
}
