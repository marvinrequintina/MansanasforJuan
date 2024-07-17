using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    class GameEvents
    {
        private Dictionary<String, EventHandler> events;

        public GameEvents()
        {
            events = new Dictionary<string, EventHandler>();
            Add("MouseEnterBtnSelection", new EventHandler(BtnSelectionMouseEnter));
            Add("MouseExitBtnSelection", new EventHandler(BtnSelectionMouseExit));
        }



        public void Add(String name, EventHandler e)
        {
            events[name] = e;
        }

        public EventHandler Get(string name)
        {
            return events[name];
        }


        private void BtnSelectionMouseEnter(Object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            currBtn.Location = new Point(currBtn.Location.X + 0, currBtn.Location.Y - 5);
        }

        private void BtnSelectionMouseExit(Object sender, EventArgs e)
        {
            Button currBtn = (Button)sender;
            if (currBtn.Enabled)
                currBtn.Location = new Point(currBtn.Location.X + 0, currBtn.Location.Y + 5);
        }

    }
}
