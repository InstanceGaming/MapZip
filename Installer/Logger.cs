using System;
using System.Text;

namespace mapzip
{
    class Logger
    {
        /// <summary>
        /// Write to log.
        /// </summary>
        /// <param name="message">Displayed message</param>
        /// <param name="mode">0=Both file and console, 1=Console only, 2=File only</param>
        public static void Out(string message, int mode)
        {
            if (mode <= 1)
            {
                Console.WriteLine(message);
            }
        }
        /// <summary>
        /// Write to log.
        /// </summary>
        /// <param name="message">Displayed message</param>
        /// <param name="mode">0=Both file and console, 1=Console only, 2=File only</param>
        /// <param name="tags">Bracket enclosed tags</param>
        public static void Out(string message, int mode, params string[] tags)
        {
            StringBuilder b = new StringBuilder();
            b.Append(FormatTags(tags));
            b.Append(" ");
            b.Append(message);
            Console.WriteLine(b.ToString());
        }
        /// <summary>
        /// Format tag array into one string.
        /// </summary>
        /// <param name="tags">string of tag names</param>
        /// <returns>formatted string of tags</returns>
        protected static string FormatTags(string[] tags)
        {
            StringBuilder b = new StringBuilder();
            foreach (string tag in tags)
            {
                b.Append(String.Format("[{0}]", tag));
            }
            return b.ToString();
        }
    }
}
