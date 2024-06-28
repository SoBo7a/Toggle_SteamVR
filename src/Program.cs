using Squirrel;
using System;
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
                MessageBox.Show("Starting update check...", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

                using (var mgr = new UpdateManager(releasesUrl))
                {
                    // Check for updates
                    MessageBox.Show("Checking for updates...", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var updateInfo = await mgr.CheckForUpdate();

                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        MessageBox.Show($"Found {updateInfo.ReleasesToApply.Count} updates. Applying updates...", "Update Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        var releaseEntry = await mgr.UpdateApp();

                        if (releaseEntry != null)
                        {
                            MessageBox.Show("Update applied. Restarting application..." + releaseEntry, "Update Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Restart the application after the update is applied
                            UpdateManager.RestartApp();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No updates found.", "No Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to check for updates: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Continue with your application startup
            MessageBox.Show("Starting application...", "Application Startup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
