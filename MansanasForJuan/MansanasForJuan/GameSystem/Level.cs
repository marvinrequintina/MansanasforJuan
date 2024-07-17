using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class Level:Template
    {
        private Panel level;
        private Word word;
        private Panel guessPanel;
        private GameEvents events;

        private Label tag;
        // lists
        private List<PictureBox> applesPics;
        private List<Label> guessLetters;
        private List<Button> selectionBtn;
        protected internal int noOfTries
        {
            get; set;
        }


        public Level(int width, int height, Word word,GameEvents events, int lastHeight=70){
            Console.WriteLine("Generating level data");
            noOfTries = 5;
            this.word = word;
            this.events = events;
            level = new Panel();
            level.Size =GetSize(width, height);
            level.Location = new Point(0,lastHeight);
            level.AutoSize = true;
            level.Padding = new Padding(20);
            level.BackColor = Color.Transparent;

            
            InitializeControls();
            
        }
        private void InitializeControls()
        {
            // life status
            Panel status = GeneratePanel(level);
            status.Size = new Size(287, 54);
            GenerateLifeStatus(status, 5, 7);
            status.Location = new Point(0, 8);
            status.BackgroundImage = Image.FromFile("Images/StatusBgNoBorder.png");
            CenterControl(status);

            //tag
            tag = GenerateLabel(level, word.themeInfo, Color.White, new Font("Arial", 20, FontStyle.Bold));
            tag.BackColor = Color.Brown;
            tag.Padding = new Padding(4, 4, 4, 1);
            tag.Location = new Point(0, status.Location.Y+status.Size.Height+5);
            CenterControl(tag);

            //guess Panel
            guessPanel = GeneratePanel(level);
            guessPanel.AutoSize = true;
            guessPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            guessPanel.MinimumSize = new Size(400, 105);
            guessPanel.MaximumSize = new Size(600, 200);
            guessPanel.BackColor = Color.White;
            GenerateGuessLetters(guessPanel, word.ansLengthInfo, 7);
            guessPanel.Location = new Point(0, tag.Location.Y+tag.Size.Height-10);
            CenterControl(guessPanel);

            //selection button
            Panel selectionPanel = GeneratePanel(level);
            selectionPanel.AutoSize = true;
            selectionPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            GenerateSelectionBtn(selectionPanel, word.letterChoicesNumInfo, 7, 65, 65, 10, 10);
            selectionPanel.Location = new Point(0, guessPanel.Location.Y+guessPanel.Size.Height-60);
            CenterControl(selectionPanel);
        }

        public Panel getInstance()
        {
            return level;
        }

        private void GenerateLifeStatus(Control parent, int length, int space)
        {
            Console.WriteLine("Generating Life Letters....");
            applesPics = new List<PictureBox>();
            int PosX = 25;
            Image applePic = Image.FromFile("Images/apple.png");
            for(int i=0; i<length; i++)
            {
                PictureBox apple = GeneratePictureBox(parent,applePic, new Size(40,40));
                applesPics.Add(apple);
                apple.Location =  new Point(PosX, 4);
                PosX += 45+space;
            }
        }

        private void GenerateGuessLetters(Control parent, int length, int space)
        {
            Console.WriteLine("Generating Guess Letters....");
            guessLetters = new List<Label>();
            word.guessLetters = guessLetters;

            Panel currPanel = GeneratePanel(parent);
            currPanel.AutoSize = true;
            int PosX = 4, PosY= 0;
            int commonHeight= 0;
            int commonWidth = 0;
            int maxWidth = currPanel.Parent.MaximumSize.Width;
            bool isOrigRow = true;
            float fontSize = 35f;
            if(word.difficultyInfo == "Hard")
            {
                fontSize = 30f;
            }
            for (int i=0; i<length; i++)
            {
                // auto wrap

                if (currPanel.Size.Width+commonWidth+space > maxWidth)
                {
                    isOrigRow = false;
                    currPanel.Location = new Point(0, PosY);
                    currPanel.Size = new Size(currPanel.Size.Width, commonHeight);
                    CenterControl(currPanel);
                    PosY += currPanel.Height + 6;
                    PosX = 4;
                    currPanel = GeneratePanel(parent);
                    currPanel.AutoSize = true;

                }
                string val = "_";
                if (!Char.IsLetter(word.GetAnsChar(i)))
                {
                    val = " ";
                }

                Label label = GenerateLabel(currPanel, val,Color.Black,new Font("Arial",fontSize,FontStyle.Bold));
                label.Location = new Point(PosX, 2);
                label.TextAlign = ContentAlignment.MiddleCenter;
                guessLetters.Add(label);
                commonHeight = label.Height;
                commonWidth = label.Width;
                PosX += label.Width + space;
           
            }
            currPanel.Location = new Point(0, PosY);
            currPanel.Size = new Size(currPanel.Size.Width, commonHeight+10);
            CenterControl(currPanel,isOrigRow);
        }
      

        private void GenerateSelectionBtn(Control parent,int length=10,int spaces=3, int sizeX = 40, int sizeY = 40,int PosX = 0, int PosY = 0)
        {
            Console.WriteLine("Generating Button....");
            int rowNum = 3;
            length = (length/3)+2;
            int letInd = 0;
            Char[] letterChoices = word.GenerateLetterChoices();
            selectionBtn = new List<Button>();
            Image box = Image.FromFile("Images/Box4.png");
            for (int j = 0; j < rowNum; j++)
            {
                Panel currPanel = GeneratePanel(parent);
                currPanel.AutoSize = true;
                currPanel.Size = new Size(0, sizeY);
                PosX = 0;
                for (int i = 0; i < length; i++)
                {
                    string letText = letterChoices[letInd++].ToString();
                    Button button = GenerateButton(currPanel, letText, Color.Black, new Size(65, 65));
                    button.Font = new Font("Arial", 30,FontStyle.Bold);
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.BackColor = Color.Transparent;
                    button.FlatStyle = FlatStyle.Flat; ;
                    button.Image = box;
                    button.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
                    //button.FlatAppearance.BorderSize = 10;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
                    button.FlatAppearance.MouseDownBackColor =  Color.FromArgb(0, 255, 255, 255);
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.Location = new Point(PosX, 0);
                    PosX = PosX + button.Width + spaces;
                    selectionBtn.Add(button);

                    //event functions
                    button.Click += events.Get("ClickBtnSelection");
                    button.MouseEnter += events.Get("MouseEnterBtnSelection");
                    button.MouseLeave += events.Get("MouseExitBtnSelection");
                }
                PosY = PosY + currPanel.Height + spaces-2;

                length -= 2;
                if (length == 1)
                {
                    length = 2;
                }
            
                currPanel.Location = new Point(0, PosY);
                CenterControl(currPanel);
            }

        }

        public void ApplesLivesDeduct()
        {
            applesPics[noOfTries-1].Visible = false;
            noOfTries--;
        }
        private void ResetApplesLives() { 
        
            noOfTries = 5;
            foreach(PictureBox pic in applesPics)
            {
                pic.Visible = true;
            }
        }

        public void Next(Word word)
        {
            //next level
            this.word = word;

            //reset
            ResetApplesLives();

            //dispose guess letter
            guessPanel.Controls.Clear();
            guessLetters.Clear();
            //generate new
            GenerateGuessLetters(guessPanel, word.ansLengthInfo, 7);
            tag.Text = word.themeInfo;
            CenterControl(tag);
            CenterControl(guessPanel);

            //new value of buttons
            Char[] letterChoices = word.GenerateLetterChoices();
            for (int i=0; i<letterChoices.Length; i++)
            {
                selectionBtn[i].Enabled = true;
                selectionBtn[i].Text = letterChoices[i].ToString();
            }

       
        }

  
    }
}
