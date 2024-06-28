using Squirrel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toggle_SteamVR.src
{
    internal static class Updater
    {
        public static async Task CheckAndUpdate(string releasesUrl)
        {
            using (var mgr = new UpdateManager(releasesUrl))
            {   
                var releaseEntry = await mgr.UpdateApp();

                if (releaseEntry != null)
                {
                    MessageBox.Show($"Updated to Version:\n{releaseEntry.Version}", "Toggle SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartApplication(releaseEntry.Version.ToString());
                    return; // Exit method to prevent further execution
                }
            }
        }

        private static void RestartApplication(string newVersion)
        {
            string currentExecutablePath = Application.ExecutablePath;
            string currentDirectory = Path.GetDirectoryName(currentExecutablePath);
            string newExecutablePath = Path.Combine(currentDirectory, "..", $"app-{newVersion}", "Toggle_SteamVR.exe");

            string batchFilePath = Path.Combine(Path.GetTempPath(), "restart.bat");

            using (StreamWriter writer = new StreamWriter(batchFilePath))
            {
                writer.WriteLine("@echo off");
                writer.WriteLine("timeout /t 0.5 /nobreak > nul");
                writer.WriteLine("taskkill /f /im Toggle_SteamVR.exe");
                writer.WriteLine("timeout /t 0.5 /nobreak > nul");
                writer.WriteLine($"start \"\" \"{newExecutablePath}\"");
            }

            Process.Start(new ProcessStartInfo()
            {
                FileName = batchFilePath,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            // Exit the current process
            Application.Exit();
        }
    }
}
