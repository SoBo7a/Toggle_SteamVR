# Toggle SteamVR

Toggle SteamVR is a small Windows application built with C# that allows users to enable or disable SteamVR by toggling the directory name. It runs in the background and provides a system tray icon for easy access.


## Features

- Enable/Disable SteamVR: Quickly toggle between enabling and disabling SteamVR by renaming its installation directory.
- System Tray Integration: Displays an icon in the system tray (notification area) for easy access to enable or disable SteamVR.
- Notification Balloons: Shows informative tooltips when SteamVR is enabled or disabled.


## Installation

To use Toggle SteamVR:

1. Download: Clone or download the repository.
2. Build: Open the solution in Visual Studio and build the project.
3. Run: Execute the built executable (Toggle_SteamVR.exe).


## Configuration

- Config File: Modify config.xml to specify the installation path of SteamVR. Ensure the installPath element under steamVR is correctly set to your SteamVR directory.

Example config.xml:
```xml
<?xml version="1.0" encoding="utf-8"?>
<config>
  <steamVR>
    <installPath>C:\Program Files (x86)\Steam\steamapps\common\SteamVR</installPath>
  </steamVR>
</config>
```


## Usage

- System Tray Icon: Right-click the Toggle SteamVR icon in the system tray to access options:
  - Enable SteamVR: Renames the SteamVR directory to remove "- Disabled".
  - Disable SteamVR: Renames the SteamVR directory to append " - Disabled".
  - Exit: Closes the application.


## Troubleshooting

If you encounter issues:

- Configuration Error: Ensure config.xml contains the correct path to your SteamVR installation.
- Icon Missing: If the application icon is missing in the system tray, ensure all resources (*.ico files) are properly included in the project and set in Visual Studio project settings.


## Contributing

Contributions are welcome! Feel free to fork the repository, make changes, and submit pull requests.


## Attribution

The badge VR icon used in this project is from [icon-icons.com](https://icon-icons.com/icon/badge-vr/185339), and is used under the MIT License.


## License

This project is licensed under the MIT License - see the [LICENSE](https://github.com/SoBo7a/Toggle_SteamVR/blob/master/LICENSE.txt) file for details.
