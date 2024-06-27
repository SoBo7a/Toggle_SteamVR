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
                using (var mgr = new UpdateManager(releasesUrl))
                {
                    await mgr.UpdateApp();

                    ApplicationConfiguration.Initialize();
                    Application.Run(new Form1());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to check for updates: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Continue starting the application even if update check fails
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
        }
    }
}
