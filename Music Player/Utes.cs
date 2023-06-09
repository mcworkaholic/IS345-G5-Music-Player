﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// This is a general class for utilities that could be useful to any other class or form. 

namespace Music_Player
{
    internal class Utes
    {
        // Arrays of formats for different media
        public static string[] videoFormats = { ".avi", ".mp4", ".mkv", ".mov", ".wmv", ".flv", ".webm", ".mpeg", ".mpg", ".m4v" };
        public static string[] audioFormats = { ".mp3", ".wav", ".flac", ".aac", ".wma", ".m4a", ".ogg", ".opus", ".alac", ".aiff" };
        public static string[] imageFormats = { ".png", ".jpg" };

        // Mouse events
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        public void OpenLink(string link)
        {
            string target;
            switch (link)
            {
                case "githublinkBox":
                    target = "https://github.com/mcworkaholic/IS345-G5-Music-Player";
                    break;
                case "SoundCloud":
                    target = "https://soundcloud.com/";
                    break;
                case "Spotify":
                    target = "https://open.spotify.com/";
                    break;
                default:
                    target = "https://music.youtube.com/";
                    break;
            }
            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
        }
        /*
         * SearchForAudioDirectories searches for directories that contain audio files in a given path and its subdirectories and returns a list of directory paths. 
         * The code is efficient because it uses the Directory.GetFiles method to search for audio files in each directory, in parallel, and 
         * Any LINQ extension method to check if any audio files are present in the directory.
         *   The code recursively calls itself on each subdirectory found in the given path, effectively searching through all subdirectories.
        */
        public List<string> SearchForAudioDirectories(string path)
        {
            List<string> audioDirectories = new List<string>();
            try
            {
                Parallel.ForEach(Directory.GetDirectories(path), directory =>
                {
                    if (Directory.GetFiles(directory).Any(file => audioFormats.Contains(Path.GetExtension(file))))
                    {
                        lock (audioDirectories)
                        {
                            audioDirectories.Add(directory);
                        }
                    }
                    audioDirectories.AddRange(SearchForAudioDirectories(directory));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error from Utes.cs: {ex.StackTrace} \n \n {ex.Message}");
            }
            return audioDirectories;
        }
        // Helps toggle the state of anything given a boolean and a counter 
        public bool ToggleState(bool currentState, ref int counter)
        {
            counter++;
            currentState = (counter % 2 == 1);
            return currentState;
        }
        public void DoMouseClick()
        {
            Cursor.Hide(); // Hide the cursor before the mouse click

            // Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);

            Cursor.Show(); // Show the cursor again after the mouse click
        }
    }
}