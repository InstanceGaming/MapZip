using System;
using System.Collections.Generic;
using System.Globalization;

namespace mapzip
{
    class Installer
    {
        private ConfigReader m_ConfigParameters = null;
        private string m_DataPath = null;
        

        public Installer(ConfigReader configuration, string dataPath)
        {
            m_ConfigParameters = configuration;
            m_DataPath = dataPath;
        }

        public void Install()
        {
            string m_ProfileDir = IO.Combine(IO.GameDir, m_ConfigParameters.Config.internalName);
            string m_LauncherProfilesJSON = IO.Combine(IO.GameDir, "launcher_profiles.json");

            Logger.Out("Beginning install proccess...", 0, "INSTALLER");
            //ensuring user has created .minecraft directory
            Logger.Out("Verifiying game directory.", 0, "INSTALLER");
            if (!IO.Exists(IO.GameDir))
            {
                Logger.Out("Game data directory not found. Please try and open the Minecraft launcher before using this installer.", 0, "INSTALLER");
                Console.ReadKey();
            }
            //remove existing profile if already installed previously
            if (IO.Exists(m_ProfileDir))
            {
                Logger.Out("Existing installation found. Removing to reinstall.", 0, "INSTALLER");
                IO.RemoveFolder(m_ProfileDir);
            }
            //check if launcher_profiles.json exists, and create one if not.
            if (!IO.Exists(m_LauncherProfilesJSON))
            {
                Logger.Out("Creating launcher profiles file as one was not found.", 0, "INSTALLER");
                IO.CreateFile(m_LauncherProfilesJSON);
            }
            //edit launcher profiles and add new one
            Logger.Out("Adding new profile to list.", 0, "INSTALLER");
            List<KeyValuePair<string, string>> profileProps = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("name",m_ConfigParameters.Config.friendlyName),
                new KeyValuePair<string, string>("type","custom"),
                new KeyValuePair<string, string>("created",DateTime.UtcNow.ToString("yyyy-MM-ddTHH:MM:ss.fffZ", CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("lastUsed",DateTime.UtcNow.ToString("yyyy-MM-ddTHH:MM:ss.fffZ", CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("icon",m_ConfigParameters.Config.profileIcon),
                new KeyValuePair<string, string>("lastVersionId",m_ConfigParameters.Config.gameVersion),
                new KeyValuePair<string, string>("gameDir",m_ProfileDir),
            }; 
            string editedProfile = JsonInterface.EditLauncherProfiles(
                IO.ReadTextFile(m_LauncherProfilesJSON),
                "mz"+Utils.RandomString(30), 
                profileProps);
            IO.WriteTextFile(m_LauncherProfilesJSON, editedProfile);
            //create new profile directory
            Logger.Out("Creating profile directory.", 0, "INSTALLER");
            IO.CreateFolder(IO.GameDir, m_ConfigParameters.Config.internalName);
            //unzip profile contents to temp directory
            Logger.Out("Unzipping archive.", 0, "INSTALLER");
            IO.UnzipFile(m_DataPath, IO.ExecutingDir);
            //copy users options.txt if the zipped profiled does not contain one
            if (IO.Exists(IO.Combine(IO.GameDir, "options.txt")) && !IO.Exists(IO.Combine(IO.Combine(IO.ExecutingDir, "profile"), "options.txt")))
            {
                //copy options.txt from game directory to new profile
                Logger.Out("Setting defaults for profile from existing.", 0, "INSTALLER");
                IO.CopyFile(IO.Combine(IO.GameDir, "options.txt"), IO.Combine(m_ProfileDir, "options.txt"));
            }
            //take extracted profile to the games new profile directory
            Logger.Out("Copying temporary files to profile folder.", 0, "INSTALLER");
            IO.CopyDirectory(IO.Combine(IO.ExecutingDir, "profile"), m_ProfileDir);
            Logger.Out("Install proccess completed.", 0, "INSTALLER");
        }
    }
}
