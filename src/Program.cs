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
                    MessageBox.Show("Updated to Version: " + releaseEntry?.Version, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RestartApplication();
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void RestartApplication()
        {
            string batchFilePath = Path.Combine(Path.GetTempPath(), "restart.bat");

            using (StreamWriter writer = new StreamWriter(batchFilePath))
            {
                writer.WriteLine("@echo off");
                writer.WriteLine("timeout /t 3 /nobreak > nul");
                writer.WriteLine("start \"\" \"" + Application.ExecutablePath + "\"");
            }

            Process.Start(new ProcessStartInfo()
            {
                FileName = batchFilePath,
                UseShellExecute = false,
                CreateNoWindow = true
            });

            Application.Exit();
        }
    }
}
