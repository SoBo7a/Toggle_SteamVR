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
                await mgr.UpdateApp();

                // Continue with your application startup
                ApplicationConfiguration.Initialize();
                Application.Run(new Form1());
            }
        }
    }
}
