using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Toggle_SteamVR.src
{
    public static class ConfigurationManager
    {
        private static string configFilePath = Path.Combine(Application.StartupPath, "config.xml");
        private static string steamVRPath;
        private static bool autoUpdateEnabled;
        private static bool startWithWindows;

        public static void LoadConfiguration()
        {
            try
            {
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

        private static void UpdateAutoStartRegistry(bool enable)
        {
            const string runKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
            const string appName = "Toggle_SteamVR";

            MessageBox.Show("Updating Autostart");

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
    }
}
