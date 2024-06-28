using Squirrel;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toggle_SteamVR.src
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            const string releasesUrl = "https://raw.githubusercontent.com/SoBo7a/Toggle_SteamVR/development/Releases/";
            using (var mgr = new UpdateManager(releasesUrl))
            {
                var releaseEntry = await mgr.UpdateApp();

                if (releaseEntry != null)
                {
                    MessageBox.Show("Updated to Version:\n" + releaseEntry?.Version, "Toggle SteamVR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartApplication(releaseEntry?.Version.ToString());
                    return; // Exit Main method to prevent further execution
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
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
        }
    }
}
