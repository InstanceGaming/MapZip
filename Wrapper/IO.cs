using System;
using System.IO;
using System.IO.Compression;

namespace mapzip
{
    class IO
    {
        /// <summary>
        /// Create a folder.
        /// </summary>
        /// <param name="path">where to make the folder</param>
        /// <param name="name">what to name it</param>
        public static void CreateFolder(string path, string name)
        {
            Directory.CreateDirectory(Path.Combine(path, name));
        }
        /// <summary>
        /// Create a folder
        /// </summary>
        /// <param name="path">the folders location and name</param>
        public static void CreateFolder(string path)
        {
            Directory.CreateDirectory(path);
        }
        /// <summary>
        /// Delete a folder.
        /// </summary>
        /// <param name="path">where to delete the folder</param>
        public static void RemoveFolder(string path)
        {
            Directory.Delete(path, true);
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
        /// Combine to paths.
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
            ZipFile.ExtractToDirectory(start, end);
        }
        /// <summary>
        /// Copy the directory
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
        /// Reat UTF-8 encoded text file
        /// </summary>
        /// <param name="path">where to find the text file</param>
        /// <returns>everything in the file in text form</returns>
        public static string ReadTextFile(string path)
        {
            return File.ReadAllText(path);
        }
        /// <summary>
        /// Write UTF-8 encoded text into a file
        /// </summary>
        /// <param name="path">where to put it</param>
        /// <param name="text">what to put into it</param>
        public static void WriteTextFile(string path, string text)
        {
            File.WriteAllText(path,text);
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
        /// Add things to a file, text or bytes.
        /// </summary>
        /// <param name="path">where to find the file</param>
        /// <returns>file stream to allow writing and reading</returns>
        public static FileStream WriteFile(string path)
        {
            return new FileStream(path,FileMode.OpenOrCreate,FileAccess.Write,FileShare.None);
        }
    }
}
