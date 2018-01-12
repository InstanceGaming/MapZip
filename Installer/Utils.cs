using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace mapzip
{
    static class Utils
    {
        //product version string
        public static string ProductVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        //log text tags
        public static string[] TextTags = SeparateStringCommas(Properties.Settings.Default.Text_Tags);

        /// <summary>
        /// Separate string by comma
        /// </summary>
        /// <param name="v">raw string</param>
        /// <returns>array of strings</returns>
        public static string[] SeparateStringCommas(string v)
        {
            return v.Split(',');
        }

        /// <summary>
        /// Open URL in system browser
        /// </summary>
        /// <param name="url">where to go</param>
        public static void OpenWebpage(string url)
        {
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Close the Minecraft launcher/game.
        /// </summary>
        public static void CloseLauncher()
        {
            foreach (Process proccess in Process.GetProcessesByName("Minecraft"))
            {
                proccess.Kill();
            }
        }

        /// <summary>
        /// Take array of strings and add gramar-correct commas and 'and' at end.
        /// </summary>
        /// <param name="text">array of texts</param>
        /// <returns>grammatically correct text</returns>
        public static string StringArrayToFormatted(string[] text)
        {
            StringBuilder b = new StringBuilder();
            for (int a = 0; a < text.Count(); a++)
            {
                if (a == text.Length - 2 || text.Length - 1 < 1)
                {
                    b.Append(text[a]);
                }
                else if (a == text.Length - 1)
                {
                    b.Append(" and " + text[a]);
                }
                else
                {
                    b.Append(text[a] + ", ");
                }
            }
            return b.ToString();
        }

        /// <summary>
        /// Generate random string. Non-crypto.
        /// </summary>
        /// <param name="length">length of the string</param>
        /// <returns>randomness string</returns>
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void HelpText(string errors)
        {
            string[] helpRaw = Properties.Settings.Default.Text_ArgumentsHelpText.Split('@');

            foreach (string line in helpRaw)
            {
                Logger.Out(line, 1, TextTags[6]);
            }

            if (errors != null)
            {
                Logger.Out(errors,1);
            }
        }

        public static bool LoopingReadKeyConfirm(bool bypass)
        {
            //wait for user input to begin install
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.Y && !bypass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
