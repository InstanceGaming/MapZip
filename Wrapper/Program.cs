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
            foreach (string arg in args)
            {
                installerArgs += " " + arg;
            }
            if (!IO.Exists(tempFolder))
            {
                IO.CreateFolder(tempFolder);
            }
            File.SetAttributes(tempFolder, FileAttributes.Normal);
            FileStream stream = IO.WriteFile(IO.Combine(tempFolder,"~installer.bin"));
            stream.Write(Properties.Resources.mzinstaller, 0, Properties.Resources.mzinstaller.Length);
            stream.Close();
            IO.UnzipFile(IO.Combine(tempFolder, "~installer.bin"), tempFolder);

            Process instprc = Process.Start(IO.Combine(tempFolder, "mzinstaller.exe"), installerArgs);
            instprc.WaitForExit();

            IO.RemoveFolder(tempFolder);
        }
    }
}
