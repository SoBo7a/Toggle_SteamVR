# Toggle SteamVR

Toggle SteamVR is a small Windows application built with C# that allows users to enable or disable SteamVR by changing the folder name of SteamVR. It runs in the background and provides a system tray icon for easy access.


## Features
	
1. **System Tray Integration:**
	- **Easy Access:** An icon in the system tray (notification area) provides convenient access to enable or disable SteamVR with just a few clicks.

2. **Startup Options:**
	- **Start with Windows:** Choose whether the app should automatically start with Windows, providing immediate availability when your computer boots up.

3. **Automatic Updates:**
	- **Stay Up-to-Date:** Always receive the latest version of the app through automatic updates, ensuring you have the latest features and security improvements without manual intervention.

4. **Auto Disable Function:**
	- **Automatic Management:** Automatically disables SteamVR when specified applications are running and restores the previous state when they are closed.


## Installation

To use Toggle SteamVR:

1. **Download the Latest Version:**	
	- Navigate to the [Releases Section](https://github.com/SoBo7a/Toggle_SteamVR/releases/latest) on the GitHub repository and download the latest Setup.exe.

2. **Install the Application:**	
	- Run the downloaded Setup.exe to install Toggle SteamVR. The application will be installed in the %localappdata%\ToggleSteamVR directory on your machine.

3. **Launch the Application:**
	- After installation, the app will automatically launch and create a Desktop Shortcut. You can access the running app via the system tray icon on your taskbar.

4. **Configure Settings:**
	- Open the app settings from the system tray icon to configure and customize the application according to your needs.


## Usage

Right-click the Toggle SteamVR icon in the system tray to access the following options:

- **Enable SteamVR:** Restores the SteamVR directory to its default name, enabling SteamVR functionality.
- **Disable SteamVR:** Renames the SteamVR directory to append " - Disabled", effectively disabling SteamVR.
- **Check for Updates:** Automatically checks for and installs the latest version of Toggle SteamVR without requiring a restart.
- **Settings:** Opens the settings menu to customize and configure the application according to your preferences.
- **Exit:** Closes the application and removes the icon from the system tray.


## Troubleshooting

If you encounter issues:

- **Configuration Error:** Ensure config.xml contains the correct path to your SteamVR installation.


## Contributing

Contributions are welcome! Feel free to fork the repository, make changes, and submit pull requests.


## Release a New Version (For Developers Only)

To release a new version with your changes, follow these steps:

1. Open the project in Visual Studio.
2. Navigate to the Package Manager Console.
3. Run the following command:

```batch
.\build-and-release.ps1 -version [newVersionNumber] -releaseNotes "[someReleaseNotes]"
```
Replace [newVersionNumber] with the new version number and [releaseNotes] with a brief description of the changes in this release.

This command will:
- Build the app in "Release" config of Visual Studio.
- Create the NuGet package.
- Releasify the package.

Afterward, you can push the changes to the GitHub repository's branch from which users update their app (e.g., "master"). The new update can then be applied in the app using the update functionality.


## Attribution

The badge VR icon used in this project is from [icon-icons.com](https://icon-icons.com/icon/badge-vr/185339), and is used under the MIT License.


## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/SoBo7a/Toggle_SteamVR/blob/master/LICENSE.txt) file for details.
