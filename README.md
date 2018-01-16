# MapZip Installer

Simplistic command-line map installer for Java Minecraft map-makers.

Last updated January 12th, 2018 - Current application version **1.2.0.2**

---

# Requirements

### Map-maker requirements

1. Windows 7+
2. Microsoft .NET 4.5.2+ Redistributable package ([download here][1]).
3. Visual Studio Community 15.0+ ([download here][2]) with Visual C#.
4. A file archiver with .zip support. 

### End-user requirements

1. Windows 7+
2. Microsoft .NET 4.5.2+ Redistributable package ([download page][1]).
3. Has opened the Minecraft 2.0 launcher at least once ([homepage][3]).

---

# Command-line arguments
 
 Argument            | Description                                                                 | Value type 
 ------------------- | --------------------------------------------------------------------------- | ----------
 `[-c, --config]`    | Define a custom config file path. (Realative to temp folder)                | String, Path
 `[-d, --data]`      | Define a custom path to the profile data file. (Realative to temp folder)   | String, Path
 `[-s. --silent]`    | Bypass confirmation message. **Partially implemented**                      | N/A
 `[-u, --uninstall]` | Uninstall the associated world from the game. **Not implemented**           | N/A
 `[-h, --help]`      | Argument usage message.                                                     | N/A

---

# Installer data
The data files, by default, are template examples. Please view these for refrence.

* **config.txt** - Global configuration of the installer. Configuration values:

     Name                      | Description                                                           | Value Type
     ------------------------- | --------------------------------------------------------------------- | -----------
     `version`                 | You can ignore this. Simply version management.                       | String
     `authors`                 | The names of the creators of the map.                                 | Array
     `friendlyName`            | The pretty, formatted name of the map and profile.                    | String
     `gameVersion`             | What version of the game you want to be used for the map.             | String
     `mapVersion`              | Add your own version control for your map.                            | String
     `name`                    | The internal name of the map, no spaces or special characters.        | String
     `profileIcon`             | The icon of the game. To view possible values, use the launcher.      | String
     `showWebpages`            | If true, the installer will open the webpage specified after install. | Boolean
     `webpageInstalled`        | The webpage to open upon installation.                                | String
	 `webpageUninstalled`      | The webpage to open upon uninstallation.                              | String

 * **profile.zip** - You create this part. Within it is the same folder layout as the .minecraft directory, place your maps resources within this zip.

---

# Use Instructions

### Quick & Simple
1. Navigate to the `Data\` directory of the solution.
2. Create or edit the existing data files as described above.
3. Double-click the batch file named "BUILD.bat"
4. Your new installer will be in the `Output\` folder.

### Advanced

1. Navigate to the `Data\` directory of the solution.
2. Create or edit the existing data files as described above.
3. Double-click the Visual Studio `.sln` solution file and open it.
4. If you want to edit the application, the solution is laid out like so:
   * **mzinstaller** - the installer that parses the data and extracts the map to the game directory.
   * **mzwrapper** - the wrapping executable that contains the installer allowing for a one-executable installer file. Please note, the solution is setup to automatically wrap the installer when you build the solution, so you don't have to worry about it.
5. Once any changes have been made, you can build the entire solution.
6. Your new installer will be in the `Output\` folder.
 
 \*Note: If required NuGet packages are not found when building the solution, consider running "nugetrestore.bat" and build again.

---

# Troubleshooting

Please report any build issues, or issues in general about this application to the author and/or the issue tracker.

---

# [Goals](https://github.com/InstanceGaming/Mapzip/projects/1)

- [x] Make the installer completely functional.
- [x] Make the solution easy to use for inexperienced users.
- [x] Make the installer one file.
- [x] Add command line support.
- [x] Make the command line messages easily translatable.
- [ ] Add uninstaller to the installed profile path.
- [ ] Add log file capabilities.

---

## Complete Changelog

 Version                     | Date released   | Rev. Compatibility  | Changes                                                           
 --------------------------- | --------------- | ------------------- | -----------------------------------------------------------------
 1.1.0.0 ([Ext. Archive][6]) | N/A             | N/A                 | First stable (non-published) release.						
 1.2.0.0 (N/A)               | N/A             | No                  | <ul><li>Removed licence text system as it can be implemented within the map.</li><li>Changed configuration to JSON syntaxing.</li><li>Made two webpage definitions, one for install and one for uninstall.</li><li>Fixed read-key continuing application instead of exiting.</li><li>Fixed file overwriting exception by moving the temporary folder to the executing directory of the installer.</li><li>Made the wrapper shell delete the temp directory.</li><li>Moved all command-line message strings to project settings for easy translations.</li><li>Removed other `.bat` files implace for internal pre/post-build events.</li></ul>                         
 1.2.0.1 ([Ext. Archive][7]) | 1/12/2018       | Yes                 | <ul><li>Added error catch to wrapper if executable not found.</li><li>Added error catch if configuration file is missing.</li><li>Reformatted argument error message.</li></ul> 																																																																																																																						
 1.2.0.2 ([Ext. Archive][8]) | 1/16/2018       | Yes                 | <ul><li>Cleaned up unused methods in wrapper.</li><li>Made temp folder hidden.</li><li>Added a touch of coloring to the installer command line.</li></ul>
 
 ---

## Author & Legal Stuff

MapZip Installer created by Jacob Jewett ([website][4], [twitter][5]).

THIS SOFTWARE IS DISTRIBUTED "AS IS" UNDER NO FORM OF WARENTY OR LIABILITY TO THE ORIGINAL AUTHOR.

This application and the author are NOT in any way affiliated with MOJANG AB. and their associates.

[1]: https://www.microsoft.com/en-us/download/details.aspx?id=42642
[2]: https://visualstudio.com
[3]: https://minecraft.net
[4]: http://instancegaming.net
[5]: http://twitter.com/Blackhawk341
[6]: https://drive.google.com/open?id=1MzR9KXm4Wova7dOn0Hije_GrPPEM4mKn
[7]: https://drive.google.com/open?id=1VGJfrf-cV6-fgmwFEX-5Bz59HMgfUsZh
[8]: https://drive.google.com/open?id=1ElZCFX6fVIh-REgP2yu9Z5bRFIoNbSPt
