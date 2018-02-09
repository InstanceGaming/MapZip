using Mono.Options;
using System;
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
            LoggableAction logger = new LoggableAction();

            //attach console service to logger
            LogService lserv = new LogService();
            logger.Output += lserv.OnLoggerLog;

            //new parser
            ArgumentParser argParser = new ArgumentParser(args);
            
            //parse arguments
            try
            {
                argParser.Parse();
            }
            catch (OptionException)
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.CONSOLE, LoggerEventArgs.LogFlags.INFO | LoggerEventArgs.LogFlags.APP | LoggerEventArgs.LogFlags.ARGUMENTS, argParser.GetHelpDescriptions());
                foreach (string error in argParser.Errors)
                {
                    logger.OnLogAction(LoggerEventArgs.LogPriority.CONSOLE, LoggerEventArgs.LogFlags.ERROR, error);
                }
                CloseFormal(logger, argParser.Silent, 1);
            }

            //show help message if set
            if (argParser.Help)
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.CONSOLE,LoggerEventArgs.LogFlags.INFO, argParser.GetHelpDescriptions());
                CloseFormal(logger, argParser.Silent, 0);
            }

            //set logging settings
            lserv.Debug = argParser.Debug;
            lserv.Silent = argParser.Silent;

            //starting text comments
            Console.Title = Properties.Settings.Default.ProductFriendlyName + " v" + Utils.ProductVersion;

            //config path
            string configPath = IO.Combine(IO.ExecutingDir, "config.json");
            string configText = IO.ReadTextFile(configPath);

            //msg welcome
            logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.INFO, String.Format(Properties.Settings.Default.Text_Welcome, Properties.Settings.Default.ProductFriendlyName, Utils.ProductVersion));
            logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.INFO, Properties.Settings.Default.Text_Licence);
            logger.OnLogAction(LoggerEventArgs.LogPriority.CONSOLE, LoggerEventArgs.LogFlags.APP, Properties.Settings.Default.Text_Separator);

            //msg reading config
            logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_ReadingConfig);

            //ensure config file exists
            if (!IO.Exists(configPath))
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, String.Format(Properties.Settings.Default.Text_ErrConfigMissing, configPath));
                CloseFormal(logger, argParser.Silent, 0);
            }

            //ensure config file isnt empty
            if (String.IsNullOrWhiteSpace(configText))
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_ConfEmpty);
            }

            //configuration settings
            ConfigReader configuration = new ConfigReader(configText);

            //populate config list with pairs from config file
            try
            {
                configuration.Parse();
            }
            catch (Exception)
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_ErrConfigSyntax);
                CloseFormal(logger, argParser.Silent, 0);
            }

            //ensure configuration is of a supported layout
            if (configuration.Config.layoutVersion != 1)
            {
                logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_ErrConfigVersionMismatch);
                CloseFormal(logger, argParser.Silent, 0);
            }

            //msg config parse success
            logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.DEBUG, Properties.Settings.Default.Text_ConfigReadSuccess);

            //if set to uninstall
            if (argParser.Uninstall)
            {
                //TODO uninstaller
                //Properties.Settings.Default.Text_NotImplemented
                CloseFormal(logger, argParser.Silent, 0);
            }
            else
            {
                string profileDataPath = IO.Combine(IO.ExecutingDir, "profile.zip");

                //ensure profile.zip exists
                if (!IO.Exists(profileDataPath))
                {
                    logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.ERROR, Properties.Settings.Default.Text_ErrProfileMissing);
                    CloseFormal(logger, argParser.Silent, 0);
                }

                //confim installation
                logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.NOTICE, String.Format(Properties.Settings.Default.Text_InstallConfirmation, configuration.Config.friendlyName, Utils.StringArrayToFormatted(configuration.Config.authors), configuration.Config.mapVersion));
                logger.OnLogAction(LoggerEventArgs.LogPriority.CONSOLE, LoggerEventArgs.LogFlags.IMPORANT, Properties.Settings.Default.Text_InstallNoticeGameSave);

                if (Utils.LoopingReadKeyConfirm(argParser.Silent) == true)
                {
                    //close the game launcher if open
                    Utils.CloseLauncher();
                    //space out user input with new line
                    Console.WriteLine(Environment.NewLine);
                    //create new installer intance
                    Installer installer = new Installer(configuration, profileDataPath);
                    installer.Output += lserv.OnLoggerLog;
                    //have the thread take on the work of the installer
                    Thread install = new Thread(() => installer.Install());
                    install.Start();
                    install.Join();
                    if (installer.Status == Installer.ActionStatus.SUCCESS)
                    {
                        //show webpage
                        if (configuration.Config.showWebpages)
                        {
                            Utils.OpenWebpage(configuration.Config.webpageInstalled);
                        }
                    }
                    if (installer.Status == Installer.ActionStatus.ABORTED)
                    {
                        logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.NOTICE, String.Format(Properties.Settings.Default.Text_InstAborted, Utils.ProductVersion, configuration.Config.friendlyName));
                    }
                }
                else
                {
                    Console.WriteLine(Environment.NewLine);
                }
            }
            CloseFormal(logger, argParser.Silent, 0);
        }

        /// <summary>
        /// Close application with custom termination message
        /// </summary>
        /// <param name="code">exit code</param>
        public static void CloseFormal(LoggableAction logger, bool bypass, int code)
        {
            //thanks message
            Console.ForegroundColor = ConsoleColor.Magenta;
            logger.OnLogAction(LoggerEventArgs.LogPriority.BOTH, LoggerEventArgs.LogFlags.INFO, String.Format(Properties.Settings.Default.Text_Thanks, Properties.Settings.Default.ProductFriendlyName, Utils.ProductVersion));
            Console.ForegroundColor = ConsoleColor.Gray;
            if (!bypass)
                Console.ReadKey();
            Environment.Exit(code);
        }
    }
}
