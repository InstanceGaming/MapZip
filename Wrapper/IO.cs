using System.IO;
using System.IO.Compression;

namespace mapzip
{
    class IO
    {

        /// <summary>
        /// Create a folder
        /// </summary>
        /// <param name="path">the folders location and name</param>
        public static DirectoryInfo CreateFolder(string path)
        {
            return Directory.CreateDirectory(path);
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
        /// Make a directory a hidden folder.
        /// </summary>
        /// <param name="folder">the folder to hide</param>
        public static void MakeFolderHidden(DirectoryInfo folder)
        {
            folder.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
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
