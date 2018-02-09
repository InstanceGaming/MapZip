using System;
using System.Collections.Generic;
using System.Globalization;

namespace mapzip
{
    partial class Installer : LoggableAction
    {
        private ConfigReader m_ConfigParameters = null;
        private string m_DataPath = null;
        private ActionStatus m_Status = ActionStatus.NEUTRAL;

        public ActionStatus Status { get { return m_Status; } }

        /// <summary>
        /// Create new instance of installer
        /// </summary>
        /// <param name="configuration">the installer configuration structure</param>
        /// <param name="dataPath">the path to the profile.zip file</param>
        public Installer(ConfigReader configuration, string dataPath)
        {
            m_ConfigParameters = configuration;
            m_DataPath = dataPath;
        }

        /// <summary>
        /// Install map
        /// </summary>
        public void Install()
        {
            bool prexistingManifestFound = false;
            ManifestStructure.Installedmap duplicateManifestProfile = null;
            string timeNow = DateTime.Now.ToUniversalTime().ToString();
            string profileDirPath = IO.Combine(IO.GameDir, m_ConfigParameters.Config.internalName);
            string manifestPath = IO.Combine(IO.GameDir, "mzmanifest.json");
            string randomProfileId = "mz" + Utils.RandomString(30);

            //ensure user has created .minecraft directory
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.INFO, Properties.Settings.Default.Text_InstVerifyGameDir);
            if (!IO.Exists(IO.GameDir))
            {
                //game dir missing, abort
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_InstGameDirMissing);
                m_Status = ActionStatus.ABORTED;
                return;
            }
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.INFO, Properties.Settings.Default.Text_InstGameDirFound);

            //msg begin install
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.NOTICE, Properties.Settings.Default.Text_InstBegin);

            //LAUNCHER DESERIALIZATION PT1

            string launcherPath = IO.Combine(IO.GameDir, "launcher_profiles.json");

            //check exists launcher profile json
            if (!IO.Exists(launcherPath))
            {
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_LauncherFileMissing);
                m_Status = ActionStatus.ABORTED;
                return;
            }

            LauncherStructure m_Launcher = JsonInterface.DeserializeToStructure<LauncherStructure>(IO.ReadTextFile(launcherPath));

            //check existing launcher file layout version and abort if format not 2
            if (m_Launcher.launcherVersion.profilesFormat != 2)
            {
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_ErrLauncherLayoutMismatch);
                m_Status = ActionStatus.ABORTED;
                return;
            }

            //MANIFEST PROFILE JSON 

            //check if mzmanifest.json exists, and create one if not.
            if (IO.Exists(manifestPath))
                prexistingManifestFound = true;
            else
                prexistingManifestFound = false;

            ManifestStructure m_Manifest = null;

            //get manifest or make new
            if (!prexistingManifestFound)
            {
                m_Manifest = new ManifestStructure() { layoutVersion = 1, timeCreated = timeNow, timeLastUsed = timeNow, installerVersionCreated = Utils.ProductVersion, installedMaps = new List<ManifestStructure.Installedmap>() };

                //create blank manifest
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstManifestMissing);
                IO.WriteTextFile(manifestPath, JsonInterface.CreateBlankManifest(1, timeNow, Utils.ProductVersion));
            }
            else
            {
                m_Manifest = JsonInterface.DeserializeToStructure<ManifestStructure>(IO.ReadTextFile(manifestPath));

                //check existing manifest version and create new if not supported
                if (m_Manifest.layoutVersion != 1)
                {
                    IO.WriteTextFile(manifestPath, JsonInterface.CreateBlankManifest(1, timeNow, Utils.ProductVersion));
                    m_Manifest = JsonInterface.DeserializeToStructure<ManifestStructure>(IO.ReadTextFile(manifestPath));
                }

                //existing manifest found
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstUpdatingManifest);
                //update manifest last used time
                m_Manifest.timeLastUsed = timeNow;
                //query installed maps in manifest for existing installed profile
                foreach (ManifestStructure.Installedmap imap in m_Manifest.installedMaps)
                {
                    if (imap.internalName == m_ConfigParameters.Config.internalName)
                    {
                        //remove existing profile if already installed previously
                        OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstExistingFound);
                        //remove profile folder
                        IO.RemoveFolder(profileDirPath);
                        //if existing manifest found, remove existing profile listing
                        duplicateManifestProfile = imap;
                    }
                }
                m_Manifest.installedMaps.Remove(duplicateManifestProfile);
            }

            //new profile object
            ManifestStructure.Installedmap newMapProfile = new ManifestStructure.Installedmap()
            {
                timeInstalled = timeNow,
                internalName = m_ConfigParameters.Config.internalName,
                profileIdentifier = randomProfileId,
                friendlyName = m_ConfigParameters.Config.friendlyName,
                mapVersion = m_ConfigParameters.Config.mapVersion,
                installerVersion = Utils.ProductVersion,
                showWebpage = m_ConfigParameters.Config.showWebpages,
                webpage = m_ConfigParameters.Config.webpageUninstalled
            };

            //add profile to manifest
            m_Manifest.installedMaps.Add(newMapProfile);
            //rewrite manifest
            IO.WriteTextFile(manifestPath, JsonInterface.SerializeFromStructure(m_Manifest, true));

            //LAUNCHER PROFILE JSON PT 2

            //remove profile of existing installation
            if (duplicateManifestProfile != null)
            {
                KeyValuePair<string, LauncherStructure.Profile> previousLauncherProfile = new KeyValuePair<string, LauncherStructure.Profile>();

                foreach (KeyValuePair<string, LauncherStructure.Profile> prop in m_Launcher.profiles)
                {
                    if (prop.Key == duplicateManifestProfile.profileIdentifier)
                    {
                        previousLauncherProfile = prop;
                    }
                }
                if (previousLauncherProfile.Key != null)
                {
                    m_Launcher.profiles.Remove(previousLauncherProfile.Key);
                    m_Launcher.selectedUser.profile = "";
                }                
            }

            //edit launcher profiles and add new one
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstUpdatingProfiles);
            //formated date time now 
            string formatedTimeNow = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:MM:ss.fffZ", CultureInfo.InvariantCulture);
            //new profile 
            LauncherStructure.Profile newProfile = new LauncherStructure.Profile()
            {
                name = m_ConfigParameters.Config.friendlyName,
                type = "custom",
                created = formatedTimeNow,
                lastUsed = formatedTimeNow,
                icon = m_ConfigParameters.Config.profileIcon,
                gameVersion = m_ConfigParameters.Config.gameVersion,
                gameDirectory = profileDirPath
            };
            //add profile
            m_Launcher.profiles.Add(randomProfileId, newProfile);
            m_Launcher.selectedUser.profile = randomProfileId;
            //rewrite launcher profiles
            IO.WriteTextFile(launcherPath, JsonInterface.SerializeFromStructure(m_Launcher, true));

            //PROFILE DATA FILES

            //create new profile directory
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstCreatingProfile);
            IO.CreateFolder(IO.GameDir, m_ConfigParameters.Config.internalName);
            //unzip profile contents to temp directory
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstUnzippingProfile);
            IO.UnzipFile(m_DataPath, IO.ExecutingDir);
            //copy users options.txt if the zipped profiled does not contain one
            if (IO.Exists(IO.Combine(IO.GameDir, "options.txt")) && !IO.Exists(IO.Combine(IO.Combine(IO.ExecutingDir, "profile"), "options.txt")))
            {
                //copy options.txt from game directory to new profile
                OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstSettingDefaults);
                IO.CopyFile(IO.Combine(IO.GameDir, "options.txt"), IO.Combine(profileDirPath, "options.txt"));
            }
            //take extracted profile to the games new profile directory
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_InstCopying);
            IO.CopyDirectory(IO.Combine(IO.ExecutingDir, "profile"), profileDirPath);
            //completed
            OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.NOTICE, Properties.Settings.Default.Text_InstCompleted);
            m_Status = ActionStatus.SUCCESS;
        }
    }
}
