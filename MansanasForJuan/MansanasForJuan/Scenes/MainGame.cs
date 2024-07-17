using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace MansanasForJuan
{
    class MainGame:Template
    {
        Panel mainGamePanel;
        List<String[]> words;
        Word word;
        Level level;
        GameEvents events;

        //score
        private Label day;
        private Label appleScore;
        private DateTime startTime;
        private String difficulty;
        int dayLevel = 1;
        int appleScoreNum = 0;
        const int LIMITDAY = 5;
        private GameData playerData;


        //main game is Waiting
        bool isWaiting = false;

        public MainGame(GameData playerData,int width, int height, string difficulty)
        {
            Console.WriteLine("Main Game running...");
            mainGamePanel = GeneratePanel();
            mainGamePanel.Size = new Size(width, height);
            mainGamePanel.BackColor = Color.Transparent;
            this.playerData = playerData;


            //Header
            Panel Header = GeneratePanel(mainGamePanel);
            Header.Size = GetSize(1 * width, .1f * height);
            Header.BackColor = Color.FromArgb(70, Color.White);
            HeaderControls(Header);

            //level
            Size levelSize = GetSize(1*width,.9f*height);
            this.events = new GameEvents();
            events.Add("ClickBtnSelection", BtnSelectionClick);
            WordPool wordPool = new WordPool();
            this.words = wordPool.GetWords(difficulty,LIMITDAY);
            this.difficulty = difficulty;
            this.word = GetCurrWord();
            level = new Level(levelSize.Width, levelSize.Height,this.word,events,Header.Size.Height);
            mainGamePanel.Controls.Add(level.getInstance());
            startTime = DateTime.Now;
        }

        private void HeaderControls(Control parent)
        {
            parent.Margin = new Padding(10, 0, 10, 0);

            //back button
            Button backBtn = GenerateButton(parent);
            backBtn.Size = new Size(32, 32);
            backBtn.FlatStyle = FlatStyle.Flat;
            backBtn.FlatAppearance.BorderSize = 0;
            backBtn.FlatAppearance.BorderColor = Color.Brown;
            backBtn.BackColor = Color.Transparent;
            backBtn.BackgroundImage = new Bitmap("Images/return-btn.png");
            backBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 255, 255, 255);
            backBtn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 255, 255, 255);
            backBtn.Location = new Point((int)(0.02f * parent.Width), 5);
            backBtn.Margin = new Padding(10, 0, 10, 0);
            backBtn.Click += new EventHandler(Back);


            //sun Icon
            PictureBox sunnyImage = new PictureBox();
            sunnyImage.Image = new Bitmap("Images/Sunny.png");
            sunnyImage.BorderStyle = BorderStyle.None;
            sunnyImage.BackColor = Color.Transparent;
            sunnyImage.Size = new Size(45, 45);
            sunnyImage.Location = new Point((int)(0.7f*parent.Width),5);
            sunnyImage.Parent = parent;

            // label day 1
            day = GenerateLabel(parent, String.Format("Day {0}",dayLevel), Color.Orange, new Font("Arial",17f,FontStyle.Bold), new Size(45,45));
            day.BackColor = Color.Transparent;
            day.Location = new Point((int)(0.75f * parent.Width), 14);

            //apples
            PictureBox appleImage = new PictureBox();
            appleImage.Image =(Image) new Bitmap("Images/appleMany.png");
            appleImage.BorderStyle = BorderStyle.None;
            appleImage.BackColor = Color.Transparent;
            appleImage.Size = new Size(45, 45);
            appleImage.Location = new Point((int)(0.87f * parent.Width), 5);
            appleImage.Parent = parent;

            appleScore = GenerateLabel(parent,String.Format("{0}",appleScoreNum), Color.OrangeRed, new Font("Arial", 17f, FontStyle.Bold), new Size(45, 45));
            appleScore.BackColor = Color.Transparent;
            appleScore.Location = new Point((int)(0.92f * parent.Width), 14);

        }


        // back button
        private void Back(Object sender,EventArgs e)
        {
            Control parent = mainGamePanel.Parent;
            parent.Controls[0].Dispose();
            parent.Controls.Clear();
            GameDifficulty gameDifficulty = new GameDifficulty(playerData, parent.Width, parent.Height);
            parent.Controls.Add(gameDifficulty.GetDifficultyInstance());
        }

        private Word GetCurrWord()
        {
            //always remove at the end
            int lastInd = words.Count - 1;
            String[] data = words[lastInd];
            Word currWord = new Word(data[1],data[0],difficulty);
            words.RemoveAt(lastInd);
            return currWord;
        }
        private void UpdateStatusCounter()
        {
            dayLevel += 1;
            appleScoreNum += level.noOfTries;
            //label change
            appleScore.Text = String.Format("{0}", appleScoreNum);
            day.Text = String.Format("Day {0}", dayLevel);
        }


        private void NextLevel()
        {
            UpdateStatusCounter();
            this.word = GetCurrWord();
            level.Next(word);
        }

        //events
        private void BtnSelectionClick(Object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            if (isWaiting||!currBtn.Enabled || level.noOfTries <= 0)
                return;

            currBtn.Location = new Point(currBtn.Location.X + 0, currBtn.Location.Y + 5);
            char guess = currBtn.Text[0];
            bool res = word.CheckTextGuess(word.guessLetters, guess);
            if (!res)
            {
                Console.WriteLine("Game Over!");
                SoundSystem.PlaySound("Wrong");
                level.ApplesLivesDeduct();
            }
            else if (isComplete(word.guessLetters))
            {
                //iscomplete
                currBtn.Enabled = false;
                isWaiting = true;
                SoundSystem.PlaySound("Correct");
                if (dayLevel == LIMITDAY)
                {
                    Console.WriteLine("Survived! {0} Won", playerData.PlayerNameInfo);
                    appleScoreNum += level.noOfTries;
                    WaitForSeconds(1000, Win);
                    return;
                }

                WaitForSeconds(1000,NextLevel);
                return;
            }
            currBtn.Enabled = false;

            if (level.noOfTries == 0)
            {
                WaitForSeconds(700, Lose);
            }

           
        }

        private void Win()
        {
            TransferToEndGame("Survived!");
        }
        private void Lose()
        {
            TransferToEndGame("Game Over!");
        }

        private void TransferToEndGame(string status)
        {
            DateTime endTime = DateTime.Now;
            double totalSeconds = (endTime - startTime).TotalSeconds;
            TimeSpan timeTaken = TimeSpan.FromSeconds(totalSeconds);

            EndGame endGame = new EndGame(mainGamePanel.Width, mainGamePanel.Height, status, dayLevel, appleScoreNum, timeTaken,difficulty
                ,playerData);
            Panel instanceEndGame = endGame.GetPanelInstance();
            instanceEndGame.BringToFront();
            Control parent = mainGamePanel.Parent;
            parent.Controls.Clear();
            parent.Controls.Add(instanceEndGame);
        }
        private void TimeProcessor(Object myObject,
                                            EventArgs myEventArgs, Action func)
        {
            Timer timer = (Timer)myObject;
            timer.Stop();
            func();
            isWaiting = false;
        }

        private void WaitForSeconds(int miliseconds, Action func)
        {
            Timer myTimer = new Timer();
            myTimer.Interval = miliseconds;
            myTimer.Start();
            myTimer.Tick += new EventHandler((sender,e)=>TimeProcessor(sender,e,func));

        }


        private bool isComplete(List<Label> guessLetters)
        {
            for (int i = 0; i < guessLetters.Count; i++)
            {
                string val = guessLetters[i].Text;
                if (val == "_")
                {
                    return false;
                }
            }
            return true;
        }


        public Panel GetGameInstance()
        {
            return mainGamePanel;
        }
    }
}
