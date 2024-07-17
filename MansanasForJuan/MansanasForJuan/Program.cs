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
    class Program
    {
        static void DisplayCredits()
        {
            Console.WriteLine("----Program Credits----");
            Console.WriteLine("James Michael Paz");
            Console.WriteLine("John Arthur Panti");
            Console.WriteLine("David Libutan");
            Console.WriteLine("Marvin Requintina");
            Console.WriteLine("-----------------------");
        }
        // main
        static void Main(string[] args) {

            DisplayCredits();
            Console.WriteLine("======Game Status [Debug]=======");
            SoundSystem.InitializeSounds();
            SoundSystem.PlayLoop("BgMusic");
            Console.WriteLine("Game Initializing.....");
            Form game = new Form();
            int width = 880, height = 600;
            game.Text = "Mansanas for Juan";
            game.Icon = new Icon(@"Images\Apple.ico");
            game.Width = width;
            game.Height = height;
            game.StartPosition = FormStartPosition.CenterParent;
            game.FormBorderStyle = FormBorderStyle.FixedSingle;
            game.BackgroundImage = new Bitmap(@"Images\BackgroundGame.png");
            game.MaximizeBox = false;
            game.MinimizeBox = false;
            game.Controls.Add(new GameHome(width, height).GetHomeInstance());
            game.ShowDialog();

        }

    }
}
