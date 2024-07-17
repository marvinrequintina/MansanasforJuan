using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MansanasForJuan
{
    // For Sound System 
   class SoundSystem
    {
        private static Dictionary<string, System.Media.SoundPlayer> soundDict = new Dictionary<string, System.Media.SoundPlayer>();

        // add sound player
        public static void AddSoundPlayer(string name,string soundPath)
        {
            soundDict[name] = new System.Media.SoundPlayer(soundPath);
        }

        // play sound
        public static void PlaySound(string nameSound)
        {
            Console.WriteLine("Playing sound {0}", nameSound);
            soundDict[nameSound].Play();
        }

        // play loop
        public static void PlayLoop(string nameSound)
        {
            Console.WriteLine("Playing sound {0}",nameSound);
            soundDict[nameSound].PlayLooping();
        }


        // initialize all sounds need
        public static void InitializeSounds()
        {
            Console.WriteLine("Preparing Sound System");
            Console.WriteLine("Loading Sounds....");

            AddSoundPlayer("BgMusic", @"Sounds\BgMusic.wav");
            AddSoundPlayer("Correct", "Sounds/CorrectSound.wav");
            AddSoundPlayer("Wrong", "Sounds/WrongSound.wav");
            AddSoundPlayer("Click", "Sounds/KnockWood.wav");
            Console.WriteLine("Loaded sounds....");
        }
        
    }
}
