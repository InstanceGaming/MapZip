using System;
using System.IO;
using System.IO.Compression;

namespace mapzip
{
    class IO
    {
        /// <summary>
        /// Environment path to %appdata%.
        /// </summary>
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public static string GameDir = Combine(IO.AppData, ".minecraft");
#pragma warning disable 0414
        private static string TempFolderName = ".mztemp";
#pragma warning restore 0414
#if !DEBUG
        public static string ExecutingDir = Combine(Environment.CurrentDirectory, TempFolderName);
#else 
        public static string ExecutingDir = Environment.CurrentDirectory;
#endif

        /// <summary>
        /// Create a folder.
        /// </summary>
        /// <param name="path">where to make the folder</param>
        /// <param name="name">what to name it</param>
        public static void CreateFolder(string path, string name)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(path, name));
            }
            catch (IOException e)
            {
                //Logger.Out("Creating Folder; " + e.Message, 0, "ERROR", "IO");
            }
            catch (UnauthorizedAccessException e)
            {
                //Logger.Out("Creating Folder; " + e.Message, 0, "ERROR", "IO", "AUTHORIZATION");
            }
        }

        /// <summary>
        /// Delete a folder.
        /// </summary>
        /// <param name="path">where to delete the folder</param>
        public static void RemoveFolder(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException e)
            {
                //Logger.Out("Removing Folder; " + e.Message, 0, "ERROR", "IO");
            }
            catch (UnauthorizedAccessException e)
            {
                //Logger.Out("Removing Folder; " + e.Message, 0, "ERROR", "IO", "AUTHORIZATION");
            }
        }

        /// <summary>
        /// Check existance at path
        /// </summary>
        /// <param name="path">where to look</param>
        /// <returns>if it exists or not</returns>
        public static bool Exists(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Combine two paths.
        /// </summary>
        /// <param name="first">path 1</param>
        /// <param name="second">path 2</param>
        /// <returns>combined path</returns>
        public static string Combine(string first, string second)
        {
            return Path.Combine(first, second);
        }

        /// <summary>
        /// Take zipped file and return extracted file.
        /// </summary>
        /// <param name="start">where to find the .zip</param>
        /// <param name="end">where to put the file</param>
        public static void UnzipFile(string start, string end)
        {
            try
            {
                ZipFile.ExtractToDirectory(start, end);
            }
            catch (IOException e)
            {
                //Logger.Out("Unzipping file; " + e.Message, 0, "ERROR", "IO");
            }
            catch (UnauthorizedAccessException e)
            {
                //Logger.Out("Unzipping file; " + e.Message, 0, "ERROR", "IO", "AUTHORIZATION");
            }
        }

        /// <summary>
        /// Copy the directory.
        /// </summary>
        /// <param name="start">where to find the directory</param>
        /// <param name="end">where to put a copy</param>
        public static void CopyDirectory(string start, string end)
        {
            DirectoryInfo diSource = new DirectoryInfo(start);
            DirectoryInfo diTarget = new DirectoryInfo(end);

            CopyDirectoryRecurssive(diSource, diTarget);
        }

        /// <summary>
        /// Recurssivly copy subdirectories and files of directory
        /// </summary>
        /// <param name="source">start path</param>
        /// <param name="target">where to put them</param>
        internal static void CopyDirectoryRecurssive(DirectoryInfo source, DirectoryInfo target)
        {
            try
            {
                Directory.CreateDirectory(target.FullName);

                foreach (FileInfo fi in source.GetFiles())
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);
                    CopyDirectoryRecurssive(diSourceSubDir, nextTargetSubDir);
                }
            }
            catch (IOException e)
            {
                //Logger.Out("Recrs. Folder Copying; " + e.Message, 0, "ERROR", "IO");
            }
            catch (UnauthorizedAccessException e)
            {
                //Logger.Out("Recrs. Folder Copying; " + e.Message, 0, "ERROR", "IO", "AUTHORIZATION");
            }
        }

        /// <summary>
        /// Make a copy of a file.
        /// </summary>
        /// <param name="start">where to look for it</param>
        /// <param name="end">where to put it</param>
        public static void CopyFile(string start, string end)
        {
            File.Copy(start, end, true);
        }

        /// <summary>
        /// Read UTF-8 encoded text file
        /// </summary>
        /// <param name="path">where to find the text file</param>
        /// <returns>everything in the file in text form</returns>
        public static string ReadTextFile(string path)
        {
            if (!Exists(path))
            {
                return null;
            }
            return File.ReadAllText(path);
            
        }

        /// <summary>
        /// Write UTF-8 encoded text into a file
        /// </summary>
        /// <param name="path">where to put it</param>
        /// <param name="text">what to put into it</param>
        public static void WriteTextFile(string path, string text)
        {
            try
            {
                File.WriteAllText(path,text);
            }
            catch (IOException e)
            {
                //Logger.Out("Reading text file; " + e.Message, 0, "ERROR", "IO");
            }
            catch (UnauthorizedAccessException e)
            {
                //Logger.Out("Reading text file; " + e.Message, 0, "ERROR", "IO", "AUTHORIZATION");
            }
        }

        /// <summary>
        /// Create blank file.
        /// </summary>
        /// <param name="path">where to put it</param>
        public static void CreateFile(string path)
        {
            File.Create(path);
        }

        /// <summary>
        /// Delete file.
        /// </summary>
        /// <param name="path">where to find it</param>
        public static void RemoveFile(string path)
        {
            File.Delete(path);
        }
    }
}
