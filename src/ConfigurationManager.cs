﻿using System.Xml.Linq;

namespace Toggle_SteamVR.src
{
    public static class ConfigurationManager
    {
        private static string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToggleSteamVR_Config");
        private static string defaultConfigFilePath = Path.Combine(Application.StartupPath, "config.xml");
        private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ToggleSteamVR_Config", "config.xml");
        private static string steamVRPath;
        private static bool autoUpdateEnabled;
        private static bool startWithWindows;

        public static void LoadConfiguration()
        {
            try
            {
                CreateDefaultConfiguration();

                // Load configuration file
                XDocument doc = XDocument.Load(configFilePath);

                // Load SteamVR settings
                XElement steamVRConfig = doc.Element("config")?.Element("steamVR");
                if (steamVRConfig != null)
                {
                    steamVRPath = steamVRConfig.Element("installPath")?.Value;
                    bool.TryParse(steamVRConfig.Element("autoUpdateEnabled")?.Value, out autoUpdateEnabled);
                    bool.TryParse(steamVRConfig.Element("startWithWindows")?.Value, out startWithWindows);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Perform actions if required settings are not found
            if (string.IsNullOrEmpty(steamVRPath))
            {
                MessageBox.Show("SteamVR installation path not found in configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public static void SaveConfiguration(string newSteamVRPath, bool newAutoUpdateEnabled, bool newStartWithWindows)
        {
            try
            {
                XDocument doc = XDocument.Load(configFilePath);
                XElement steamVRConfig = doc.Element("config")?.Element("steamVR");
                if (steamVRConfig != null)
                {
                    steamVRConfig.Element("installPath").Value = newSteamVRPath;
                    steamVRConfig.Element("autoUpdateEnabled").Value = newAutoUpdateEnabled.ToString();
                    steamVRConfig.Element("startWithWindows").Value = newStartWithWindows.ToString();
                    doc.Save(configFilePath);

                    UpdateAutoStartRegistry(newStartWithWindows);

                    // Update static variables after saving
                    steamVRPath = newSteamVRPath;
                    autoUpdateEnabled = newAutoUpdateEnabled;
                    startWithWindows = newStartWithWindows;
                }
                else
                {
                    MessageBox.Show("Invalid configuration file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool SteamVRFolderExists()
        {
            // Check if the SteamVR folder exists
            if (Directory.Exists(steamVRPath))
            {
                return true;
            }

            // Check if the SteamVR folder with the " - Disabled" suffix exists
            string disabledSteamVRPath = steamVRPath + " - Disabled";
            if (Directory.Exists(disabledSteamVRPath))
            {
                return true;
            }

            MessageBox.Show(
                "The SteamVR installation path could not be found. Please check your settings and ensure the correct path is set for SteamVR. If SteamVR is installed but currently disabled, make sure the path reflects the current directory name.",
                "SteamVR Path Not Found",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            return false;
        }

        private static void UpdateAutoStartRegistry(bool enable)
        {
            const string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            const string appName = "Toggle_SteamVR";

            try
            {
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(runKey, true))
                {
                    if (key == null)
                        return;

                    if (enable)
                    {
                        // Add application to auto-start
                        key.SetValue(appName, Application.ExecutablePath);
                    }
                    else
                    {
                        // Remove application from auto-start
                        key.DeleteValue(appName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update auto-start setting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void CreateDefaultConfiguration()
        {
            try
            {
                // Ensure the configuration directory exists
                if (!Directory.Exists(configDirectory))
                {
                    Directory.CreateDirectory(configDirectory);
                }

                // Check if configFilePath exists, if not, copy default config.xml
                if (!File.Exists(configFilePath))
                {
                    if (File.Exists(defaultConfigFilePath))
                    {
                        File.Copy(defaultConfigFilePath, configFilePath);
                    }
                    else
                    {
                        // Handle case where default config.xml doesn't exist
                        MessageBox.Show("Default configuration file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while creating default configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string SteamVRPath
        {
            get { return steamVRPath; }
            set { steamVRPath = value; }
        }

        public static bool AutoUpdateEnabled
        {
            get { return autoUpdateEnabled; }
            set { autoUpdateEnabled = value; }
        }

        public static bool StartWithWindows
        {
            get { return startWithWindows; }
            set { startWithWindows = value; }
        }

        public static List<string> GetAppList()
        {
            List<string> appList = new List<string>();

            if (File.Exists(configFilePath))
            {
                XDocument doc = XDocument.Load(configFilePath);
                XElement disableAppsElement = doc.Element("config")?.Element("steamVR")?.Element("disableApps");

                if (disableAppsElement != null)
                {
                    foreach (XElement appElement in disableAppsElement.Elements("app"))
                    {
                        appList.Add(appElement.Value);
                    }
                }
            }

            return appList;
        }

        public static void AddToAppList(List<string> appList)
        {
            XDocument doc = XDocument.Load(configFilePath);
            XElement steamVRElement = doc.Element("config")?.Element("steamVR");

            if (steamVRElement != null)
            {
                // Remove existing disableApps element if it exists
                steamVRElement.Element("disableApps")?.Remove();

                // Create a new disableApps element
                XElement disableAppsElement = new XElement("disableApps");
                foreach (string app in appList)
                {
                    disableAppsElement.Add(new XElement("app", app));
                }

                steamVRElement.Add(disableAppsElement);
                doc.Save(configFilePath);
            }
            else
            {
                MessageBox.Show("The config file does not contain a <steamVR> element.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
