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
            string m_Time = DateTime.Now.ToUniversalTime().ToString();
            string m_ProfileDir = IO.Combine(IO.GameDir, m_ConfigParameters.Config.internalName);
            string m_LauncherProfilesJSON = IO.Combine(IO.GameDir, "launcher_profiles.json");
            string m_ManifestJSON = IO.Combine(IO.GameDir, "mzmanifest.json");
            string m_ProfileID = "mz" + Utils.RandomString(30);

            //check if mzmanifest.json exists, and create one if not.
            if (!IO.Exists(m_ManifestJSON))
            {
                IO.WriteTextFile(m_ManifestJSON, JsonInterface.CreateBlankManifest(1, m_Time, Utils.ProductVersion));
            }

            ManifestStructure m_Manifest = JsonInterface.DeserializeToStructure<ManifestStructure>(IO.ReadTextFile(m_ManifestJSON));

            Logger.Out(Properties.Settings.Default.Text_InstBegin, 0, Utils.TextTags[3]);
            //ensuring user has created .minecraft directory
            Logger.Out(Properties.Settings.Default.Text_InstVerifyGameDir, 0, Utils.TextTags[3]);
            if (!IO.Exists(IO.GameDir))
            {
                Logger.Out(Properties.Settings.Default.Text_InstGameDirMissing, 0, Utils.TextTags[3]);
                Console.ReadKey();
            }
            //check manifest version
            if (m_Manifest.layoutVersion != 1)
            {
                Logger.Out(Properties.Settings.Default.Text_InstManifestMissing, 0, Utils.TextTags[3]);
                IO.WriteTextFile(m_ManifestJSON, JsonInterface.CreateBlankManifest(1, m_Time, Utils.ProductVersion));
            }
            //remove existing profile if already installed previously
            if (IO.Exists(m_ProfileDir))
            {
                Logger.Out(Properties.Settings.Default.Text_InstExistingFound, 0, Utils.TextTags[3]);
                //remove profile folder
                IO.RemoveFolder(m_ProfileDir);
                //remove instance in manifest
                IO.WriteTextFile(m_ManifestJSON, JsonInterface.RemoveRootArrayChild(IO.ReadTextFile(m_ManifestJSON), JsonSchema.Schema["manf_installed"], m_ConfigParameters.Config.internalName));
            }
            //edit installed map profiles and add new one
            Logger.Out(Properties.Settings.Default.Text_InstUpdatingManifest, 0, Utils.TextTags[3]);
            List<KeyValuePair<string, object>> installedMapProps = new List<KeyValuePair<string, object>>() {
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_time_installed"),m_Time),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_internal_name"),m_ConfigParameters.Config.internalName),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_profile_id"),m_ProfileID),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_friendly_name"),m_ConfigParameters.Config.friendlyName),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_map_version"),m_ConfigParameters.Config.mapVersion),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_installer_version"), Utils.ProductVersion),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_show_webpage"),m_ConfigParameters.Config.showWebpages),
                new KeyValuePair<string, object>(Utils.GetSchemaValue("manf_webpage_uninstalled"),m_ConfigParameters.Config.webpageUninstalled)
            };
            string installedMaps = JsonInterface.AddManifestMap(IO.ReadTextFile(m_ManifestJSON), installedMapProps);
            IO.WriteTextFile(m_ManifestJSON, installedMaps);
            //edit launcher profiles and add new one
            Logger.Out(Properties.Settings.Default.Text_InstUpdatingProfiles, 0, Utils.TextTags[3]);
            List<KeyValuePair<string, string>> profileProps = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_name"),m_ConfigParameters.Config.friendlyName),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_type"),"custom"),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_created"),DateTime.UtcNow.ToString("yyyy-MM-ddTHH:MM:ss.fffZ", CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_last_used"),DateTime.UtcNow.ToString("yyyy-MM-ddTHH:MM:ss.fffZ", CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_icon"),m_ConfigParameters.Config.profileIcon),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_game_version"),m_ConfigParameters.Config.gameVersion),
                new KeyValuePair<string, string>(Utils.GetSchemaValue("prof_game_directory"),m_ProfileDir),
            };
            string editedProfile = JsonInterface.AddLauncherProfile(
                IO.ReadTextFile(m_LauncherProfilesJSON),
                m_ProfileID,
                profileProps);
            IO.WriteTextFile(m_LauncherProfilesJSON, editedProfile);
            //create new profile directory
            Logger.Out(Properties.Settings.Default.Text_InstCreatingProfile, 0, Utils.TextTags[3]);
            IO.CreateFolder(IO.GameDir, m_ConfigParameters.Config.internalName);
            //unzip profile contents to temp directory
            Logger.Out(Properties.Settings.Default.Text_InstUnzippingProfile, 0, Utils.TextTags[3]);
            IO.UnzipFile(m_DataPath, IO.ExecutingDir);
            //copy users options.txt if the zipped profiled does not contain one
            if (IO.Exists(IO.Combine(IO.GameDir, "options.txt")) && !IO.Exists(IO.Combine(IO.Combine(IO.ExecutingDir, "profile"), "options.txt")))
            {
                //copy options.txt from game directory to new profile
                Logger.Out(Properties.Settings.Default.Text_InstSettingDefaults, 0, Utils.TextTags[3]);
                IO.CopyFile(IO.Combine(IO.GameDir, "options.txt"), IO.Combine(m_ProfileDir, "options.txt"));
            }
            //take extracted profile to the games new profile directory
            Logger.Out(Properties.Settings.Default.Text_InstCopying, 0, Utils.TextTags[3]);
            IO.CopyDirectory(IO.Combine(IO.ExecutingDir, "profile"), m_ProfileDir);
            Logger.Out(Properties.Settings.Default.Text_InstCompleted, 0, Utils.TextTags[3]);
        }
    }
}
