using Squirrel;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

            try
            {
                using (var mgr = new UpdateManager(releasesUrl))
                {
                    // Check for updates and apply them
                    var releaseEntry = await mgr.UpdateApp();

                    if (releaseEntry != null)
                    {
                        MessageBox.Show("Updated to Version: " + releaseEntry.Version, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Restart the application
                        RestartApplication();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to check for updates: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Continue with your application startup
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        private static void RestartApplication()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDir = Path.GetDirectoryName(exePath);
            string newExePath = Path.Combine(exeDir, Path.GetFileName(exePath));

            var startInfo = new ProcessStartInfo(newExePath)
            {
                UseShellExecute = true,
                WorkingDirectory = exeDir
            };

            Process.Start(startInfo);
            Application.Exit();
        }
    }
}
