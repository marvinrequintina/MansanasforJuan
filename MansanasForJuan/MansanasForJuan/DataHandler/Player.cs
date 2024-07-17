using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    //getters and setters for a player's saved data
    //Encapsulation
    class Player
    {
        private string _playerName;
        private int _modeAccess = 1;

        public string PlayerNameInfo
        {
            get { return _playerName; }
            set { _playerName = value; }
        }
  
        public int ModeAccessInfo
        {
            get { return _modeAccess; }
            set { _modeAccess = value;}
        }
    }
}
