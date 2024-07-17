using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

//getters and setters for the player data (time score and name)
namespace MansanasForJuan
{
    class GameData : Player
    {
        private TimeSpan _timeTaken;
        private int _score;


        public GameData(string name) : base()
        {
            this.PlayerNameInfo = name;
        }

        public TimeSpan TimeDurationInfo
        {
            get { return _timeTaken; }
            set { _timeTaken = value;  }
        }
        public int ScoreInfo
        {
            get { return _score; }
            set { _score = value;  }
        }       
    }
}
