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
    class GameHome : Template
    {
        Panel homePanel;
        Button playBtn;
        Button helpBtn;
        Button exitBtn;
        Button unmuteBtn;
        Button muteBtn;

        public GameHome(int width, int height)
        {
            //Generate Main Menu Panel
            Console.WriteLine("Home Scene loading...");
            homePanel = GeneratePanel();
            homePanel.Size = new Size(width, height);
            homePanel.BackColor = Color.Transparent;
            HomeControls(homePanel);

        }
        private void HomeControls(Control parent)
        {
            //Title PictureBox
            PictureBox titleImage = new PictureBox();
            titleImage.ClientSize = new Size(700, 700);
            titleImage.Image = Image.FromFile(@"Images\TitleMenu.png");
            titleImage.BackColor = Color.Transparent;
            titleImage.SizeMode = PictureBoxSizeMode.Zoom;
            titleImage.SendToBack();
            titleImage.Location = new Point(-100, -210);
            titleImage.Parent = parent;

            //Start Button
            playBtn = GenerateButton(parent);
            playBtn.Size = new Size(190, 60);
            playBtn.TabStop = false;
            playBtn.FlatStyle = FlatStyle.Flat;
            playBtn.FlatAppearance.BorderSize = 0;
            playBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            playBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            playBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            playBtn.Image = Image.FromFile(@"Images\Play.png");
            playBtn.BringToFront();
            playBtn.Location = new Point(340, 300);
            playBtn.Click += PlayButtonClick;
            playBtn.MouseEnter += PlayButtonEnter;
            playBtn.MouseLeave += PlayButtonLeave;

            //Help Button
            helpBtn = GenerateButton(parent);
            helpBtn.Size = new Size(190, 60);
            helpBtn.TabStop = false;
            helpBtn.FlatStyle = FlatStyle.Flat;
            helpBtn.FlatAppearance.BorderSize = 0;
            helpBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            helpBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            helpBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            helpBtn.Image = Image.FromFile(@"Images\Help.png");
            helpBtn.BringToFront();
            helpBtn.Location = new Point(340, 380);
            helpBtn.Click += HelpButtonClick;
            helpBtn.MouseEnter += HelpButtonEnter;
            helpBtn.MouseLeave += HelpButtonLeave;

            //Exit Button
            exitBtn = GenerateButton(parent);
            exitBtn.Size = new Size(190, 60);
            exitBtn.TabStop = false;
            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.FlatAppearance.BorderSize = 0;
            exitBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            exitBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            exitBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            exitBtn.Image = Image.FromFile(@"Images\Exit.png");
            exitBtn.BringToFront();
            exitBtn.Location = new Point(340, 460);
            exitBtn.Click += ExitButtonClick;
            exitBtn.MouseEnter += ExitButtonEnter;
            exitBtn.MouseLeave += ExitButtonLeave;

            //Mute Button
            muteBtn = GenerateButton(parent);
            muteBtn.Size = new Size(32, 32);
            muteBtn.TabStop = false;
            muteBtn.FlatStyle = FlatStyle.Flat;
            muteBtn.FlatAppearance.BorderSize = 0;
            muteBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            muteBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            muteBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            muteBtn.Image = Image.FromFile(@"Images\Mute.png");
            muteBtn.BringToFront();
            muteBtn.Location = new Point(830, 525);
            muteBtn.Click += MuteButtonClick;

            //Unmute Button
            unmuteBtn = GenerateButton(parent);
            unmuteBtn.Size = new Size(32, 32);
            unmuteBtn.TabStop = false;
            unmuteBtn.FlatStyle = FlatStyle.Flat;
            unmuteBtn.FlatAppearance.BorderSize = 0;
            unmuteBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            unmuteBtn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            unmuteBtn.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            unmuteBtn.Image = Image.FromFile(@"Images\Unmute.png");
            unmuteBtn.SendToBack();
            unmuteBtn.Location = new Point(830, 525);
            unmuteBtn.Click += UnmuteButtonClick;
        }
        private void PlayButtonClick(object sender, EventArgs e)
        {
            //Go to the next panel
            Control parent = homePanel.Parent;
            GameInput nextPanel = new GameInput(parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(nextPanel.GetInputNameInstance());
        }
        private void HelpButtonClick(object sender, EventArgs e)
        {
            //Go to the next panel
            Control parent = homePanel.Parent;
            GameHelp nextPanel = new GameHelp(parent.Size.Width, parent.Size.Height);
            parent.Controls.Clear();
            parent.Controls.Add(nextPanel.GetHelpInstance());
        }
        private void ExitButtonClick(object sender, EventArgs e)
        {
            //Exit the application
            Console.WriteLine("Exit Application...");
            Application.Exit();
        }
        private void PlayButtonEnter(object sender, EventArgs e)
        {
            playBtn.Image = Image.FromFile(@"Images\PlayGold.png");
        }
        private void PlayButtonLeave(object sender, EventArgs e)
        {
            playBtn.Image = Image.FromFile(@"Images\Play.png");
        }
        private void HelpButtonEnter(object sender, EventArgs e)
        {
            helpBtn.Image = Image.FromFile(@"Images\HelpGold.png");
        }
        private void HelpButtonLeave(object sender, EventArgs e)
        {
            helpBtn.Image = Image.FromFile(@"Images\Help.png");
        }
        private void ExitButtonEnter(object sender, EventArgs e)
        {
            exitBtn.Image = Image.FromFile(@"Images\ExitGold.png");
        }
        private void ExitButtonLeave(object sender, EventArgs e)
        {
            exitBtn.Image = Image.FromFile(@"Images\Exit.png");
        }
        private void MuteButtonClick(object sender, EventArgs e)
        {
            //Mutes the background music
            muteBtn.Visible = false;
            unmuteBtn.Visible = true;
            SoundPlayer bgmusic = new SoundPlayer();
            bgmusic.SoundLocation = @"Sounds\BgMusic.wav";
            bgmusic.Stop();
;       }
        private void UnmuteButtonClick(object sender, EventArgs e)
        {
            //Unmutes the background music
            muteBtn.Visible = true;
            unmuteBtn.Visible = false;
            SoundPlayer bgmusic = new SoundPlayer();
            bgmusic.SoundLocation = @"Sounds\BgMusic.wav";
            bgmusic.PlayLooping();
;       }
        public Panel GetHomeInstance()
        {   
            return homePanel;
        }
    }
}

