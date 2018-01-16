using Mono.Options;
using System;
using System.Collections.Generic;
using System.Threading;

namespace mapzip
{
    class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //parse command line arguments
            bool showHelp = false;
            bool uninstallMode = false;
            bool silent = false;
            string customConfigPath = null;
            string customDataPath = null;

            OptionSet argSet = new OptionSet() {
                { "c|config=", "Specify custom configuration file path.",
                    (string v) => customConfigPath = v },
                { "d|data=", "Specify custom profile data file path.",
                    (string v) => customDataPath = v },
                { "s|silent", "the number of times to repeat the greeting.",
                    v => silent = true},
                { "u|uninstall", "Uninstall the map this installer unpacks.",
                    v => uninstallMode = true},
                { "h|help",  "Show argument usage and information.",
                    v => showHelp = true},
            };

            List<string> parseErrors = new List<string>();
            try
            {
                parseErrors = argSet.Parse(args);
            }
            catch (OptionException e)
            {
                Utils.HelpText(String.Format(Properties.Settings.Default.Text_ErrArgumentErrors + e.Message));
                CloseFormal(1);
            }

            if (showHelp)
            {
                Utils.HelpText(null);
                CloseFormal(0);
            }

            //resources
            string configName = customConfigPath ?? "config.json";
            string configPath = IO.Combine(IO.ExecutingDir, configName);
            string res_config = IO.ReadTextFile(configPath) ?? "";

            //ensure config file exists
            if (!IO.Exists(configPath))
            {
                Logger.Out(String.Format(Properties.Settings.Default.Text_ErrConfigMissing,configPath), 0, Utils.TextTags[0], Utils.TextTags[1], Utils.TextTags[2]);
                CloseFormal(0);
            }

            //starting text comments
            Console.Title = Properties.Settings.Default.ProductFriendlyName + " v" + Utils.ProductVersion;

            Logger.Out(Properties.Settings.Default.Text_Separator, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Logger.Out(String.Format(Properties.Settings.Default.Text_Welcome, Properties.Settings.Default.ProductFriendlyName, Utils.ProductVersion), 0);
            Logger.Out(Properties.Settings.Default.Text_Licence, 0);
            Console.ForegroundColor = ConsoleColor.Gray;
            Logger.Out(Properties.Settings.Default.Text_Separator, 0);
            Logger.Out(Properties.Settings.Default.Text_ReadingConfig, 0, Utils.TextTags[0]);

            //configuration settings
            ConfigReader cfg = new ConfigReader(res_config);

            //populate config list with pairs from config file
            try
            {
                cfg.Parse();
            }
            catch (Exception e)
            {
                Logger.Out(Properties.Settings.Default.Text_ErrConfigSyntax + e.Message, 0, Utils.TextTags[0], Utils.TextTags[1], Utils.TextTags[2]);
                CloseFormal(0);
            }

            if (cfg.Config.version != Utils.ProductVersion)
            {
                Logger.Out(String.Format(Properties.Settings.Default.Text_ErrConfigVersionMismatch, Utils.ProductVersion, cfg.Config.version), 0, Utils.TextTags[0], Utils.TextTags[1], Utils.TextTags[2]);
                CloseFormal(0);
            }

            Logger.Out(Properties.Settings.Default.Text_ConfigReadSuccess, 0, Utils.TextTags[0]);

            if (uninstallMode)
            {
                Logger.Out(Properties.Settings.Default.Text_NotImplemented, 0);
                CloseFormal(0);
            }
            else
            {
                string res_profile = IO.Combine(IO.ExecutingDir, customDataPath ?? "profile.zip");

                //ensure profile.zip exists
                if (!IO.Exists(res_profile))
                {
                    Logger.Out(Properties.Settings.Default.Text_ErrProfileMissing, 0, Utils.TextTags[0], Utils.TextTags[4]);
                    CloseFormal(0);
                }

                //confim installation
                Logger.Out(Properties.Settings.Default.Text_Separator, 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Logger.Out(String.Format(Properties.Settings.Default.Text_InstallConfirmation, cfg.Config.friendlyName, Utils.StringArrayToFormatted(cfg.Config.authors), cfg.Config.mapVersion), 1);
                Console.ForegroundColor = ConsoleColor.White;
                Logger.Out(Properties.Settings.Default.Text_InstallNoticeGameSave, 1);
                Console.ForegroundColor = ConsoleColor.Gray;
                Logger.Out(Properties.Settings.Default.Text_Separator, 1);

                if (Utils.LoopingReadKeyConfirm(silent) == true)
                {
                    BeginInstallation(cfg, res_profile);
                }
            }
            CloseFormal(0);
        }

        private static void BeginInstallation(ConfigReader cfg, string dataPath)
        {
            //close the game launcher if open
            Utils.CloseLauncher();
            //space out user input with new line
            Logger.Out(Environment.NewLine, 1);

            //create new installer intance
            Installer installer = new Installer(cfg, dataPath);
            //have the thread take on the work of the installer
            Thread install = new Thread(() => installer.Install());
            install.Start();
            install.Join();
            //when completed, show webpage
            if (cfg.Config.showWebpages)
            {
                Utils.OpenWebpage(cfg.Config.webpageInstalled);
            }
        }

        private static void CloseFormal(int code)
        {
            //thanks message
            Logger.Out(String.Format(Environment.NewLine + Properties.Settings.Default.Text_Thanks, Properties.Settings.Default.ProductFriendlyName, Utils.ProductVersion), 1);
            Console.ReadKey();
            Environment.Exit(code);
        }
    }
}
