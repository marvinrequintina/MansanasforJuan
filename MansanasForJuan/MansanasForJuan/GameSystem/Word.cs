using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class Word
    {
        private string ans;
        private string theme;
        private string difficulty;
        private int letterChoicesNum;

        public Word(string ans, string theme, string difficulty)
        {
            this.ans = ans.ToUpper();
            this.theme = theme;
            this.difficulty = difficulty;
            letterChoicesNum = GetNumOfLetterChoices();
        }

        protected internal List<Label> guessLetters
        {
            get; set;
        }


        public int letterChoicesNumInfo
        {
            get { return letterChoicesNum; }
        }
        public int ansLengthInfo
        {
            get { return ans.Length; }
        }
        public string themeInfo
        {
            get { return theme; }
        }
        public string difficultyInfo
        {
            get { return difficulty; }
        }

        protected internal char GetAnsChar(int index)
        {
            return ans[index];
        }
     
        private List<Char> GetAlphabet()
        {
            List<char> alphabetLetters = new List<char>();
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            foreach (char alpha in alphabets)
            {
                alphabetLetters.Add(alpha);
            }
            return alphabetLetters;
        }

        protected internal Char[] GenerateLetterChoices()
        {
            Console.WriteLine("Generating Letter Choices....");
            List<char> letterChoices = new List<char>();
            List<char> alphabetLetters = GetAlphabet();
            HashSet<char> setLetters = new HashSet<char>();
            int length = letterChoicesNum;
            foreach (char c in ans)
            {
                if (!Char.IsLetter(c))
                {
                    continue;
                }
                if (!setLetters.Contains(c))
                {
                    letterChoices.Add(c);
                    alphabetLetters.Remove(c);
                    setLetters.Add(c);
                }
            }

            length -= letterChoices.Count;
            Random random = new Random();
            for (int i = 0; i < length && alphabetLetters.Count > 0; i++)
            {
                int randomInd = random.Next(0, alphabetLetters.Count);
                char c = alphabetLetters[randomInd];
                alphabetLetters.RemoveAt(randomInd);
                letterChoices.Add(c);
            }
            
            //shuffle
            return letterChoices.OrderBy(x => random.Next()).ToArray();
        }

        private int GetNumOfLetterChoices()
        {
            if (difficultyInfo == "Easy")
            {
                return 10;
            }
            else if(difficultyInfo == "Normal")
            {
                return 18;
            }
            else if(difficultyInfo == "Hard")
            {
                return 24;
            }
           return ans.Length;
        }

        protected internal bool CheckTextGuess(List<Label> guessLetters,  char guess)
        {
            Console.WriteLine("Checking Text Guess....");
            bool hasFound = false;
            SoundSystem.PlaySound("Click");
            for (int i = 0; i < guessLetters.Count; i++)
            {
                if (ans[i] == guess)
                  {
                   
                    if (ans[i] == 'I')
                        {
                            guessLetters[i].Text = " " + guess.ToString();
                        }
                        else
                            guessLetters[i].Text = guess.ToString();
                        guessLetters[i].ForeColor = Color.Green;
                        hasFound = true;
                 }
                
            }
            return hasFound;
        }

    }
}
