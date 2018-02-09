using System;
using System.Diagnostics;
using System.IO;

namespace mapzip
{
    class Program
    {
        static void Main(string[] args)
        {
            string executingDir = Environment.CurrentDirectory;
            string tempFolder = IO.Combine(executingDir,".mztemp");
            string installerArgs = "";
            if (!IO.Exists(tempFolder))
            {
                // pass arguments directly to sub proccess
                foreach (string arg in args)
                {
                    installerArgs += " " + arg;
                }
                // create temp folder
                DirectoryInfo tempinfo = IO.CreateFolder(tempFolder);
                // make it hidden
                IO.MakeFolderHidden(tempinfo);
                // write the zip data to a file
                FileStream stream = IO.WriteFile(IO.Combine(tempFolder, "~installer.bin"));
                stream.Write(Properties.Resources.mzinstaller, 0, Properties.Resources.mzinstaller.Length);
                stream.Close();
                // extract the file
                IO.UnzipFile(IO.Combine(tempFolder, "~installer.bin"), tempFolder);
                // start installer
                try
                {
                    Process instprc = Process.Start(IO.Combine(tempFolder, "mzinstaller.exe"), installerArgs);
                    instprc.WaitForExit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: Unpacked installer executable was not found. Raw message: " + e.Message);
                }
                // remove temp folder
                IO.RemoveFolder(tempFolder);
            }
        }
    }
}
