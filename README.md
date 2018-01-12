# MapZip Installer

Simplistic command-line map installer for Java Minecraft map-makers.

Last updated January 12th, 2018 - Current application version **1.2.0.0**

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
 
 Argument          | Description                                                       | Value type 
 ----------------- | ----------------------------------------------------------------- | ----------
 [-c, --config]    | Define a custom config file path.                                 | String, Path
 [-d, --data]      | Define a custom path to the profile data file.                    | String, Path
 [-s. --silent]    | Bypass confirmation message. **Not implemented completly**        | N/A
 [-u, --uninstall] | Uninstall the associated world from the game. **Not implemented** | N/A
 [-h, --help]      | Help message.                                                     | N/A

---

# Installer data
* **config.txt** - Global configuration of the installer. Configuration values:

     Name                      | Description                                                           | Value Type
     ------------------------- | --------------------------------------------------------------------- | -----------
     `version`                 | You can ignore this. Simply version management.                       | String
     `authors`                 | The names of the creators of the map, comma separated.                | Array
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
1. Navigate to the Data\ directory of the solution.
2. Create or edit the existing data files as described above.
3. Double-click the batch file named "BUILD.bat"
4. Your new installer will be in the Output\ folder.

### Advanced

1. Navigate to the Data\ directory of the solution.
2. Create or edit the existing data files as described above.
3. Double-click the Visual Studio .sln solution file and open it.
4. If you want to edit the application, the solution is laid out like so:
   * **mzinstaller** - the installer that parses the data and extracts the map to the game directory.
   * **mzwrapper** - the wrapping executable that contains the installer allowing for a one-executable installer file. Please note, the solution is setup to automatically wrap the installer when you build the solution, so you don't have to worry about it.
5. Once any changes have been made, you can build the entire solution.
6. Your new installer will be in the Output\ folder.

---

# Troubleshooting

Please report any build issues, or issues in general about this application to the author and/or the issue tracker.

---

# Goals

- [x] Make the installer completely functional.
- [x] Make the solution easy to use for unexperienced users.
- [x] Make the installer one file.
- [x] Add command line support.
- [x] Make the command line messages easily translatable.
- [ ] Add uninstaller .exe to the profile path.
- [ ] Add log file capibilities.

---

## Changelog

 Version   | Date released  | Changes                                                               
 --------- | -------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 1.1.0.0   | 1/11/2018      | First stable (non-published) release.
 1.2.0.0   | 1/12/2018      | Removed licence text as it could easily be added within the map. Changed configuration layout to use JSON. Added command line support. Made all messages editable via project settings file.  

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
