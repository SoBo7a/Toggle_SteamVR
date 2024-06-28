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
            // Initialize Squirrel update manager with your GitHub releases URL
            const string releasesUrl = "https://raw.githubusercontent.com/SoBo7a/Toggle_SteamVR/development/Releases/";
            using (var mgr = new UpdateManager(releasesUrl))
            {
                // Check for updates
                var releaseEntry = await mgr.UpdateApp();


                if (releaseEntry != null)
                {
                    //restart app if an update was installed
                    // MessageBox.Show("Updated, please restart the app...", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateManager.RestartApp();
                    MessageBox.Show("Updated to Version: " + releaseEntry?.Version, "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }            
            }                       

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
