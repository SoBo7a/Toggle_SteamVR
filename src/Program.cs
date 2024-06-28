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
            // Check if the application is running on the first install or is being updated
            using (var updateManager = await UpdateManager.GitHubUpdateManager("https://raw.githubusercontent.com/SoBo7a/Toggle_SteamVR/development/Releases/"))
            {
                SquirrelAwareApp.HandleEvents(
                    onInitialInstall: v => updateManager.CreateShortcutForThisExe(),
                    onAppUpdate: v => updateManager.CreateShortcutForThisExe(),
                    onAppUninstall: v => updateManager.RemoveShortcutForThisExe(),
                    onFirstRun: () => MessageBox.Show("Thanks for installing Toggle SteamVR!"));

                // Check for updates
                var updateResult = await updateManager.UpdateApp();

                // If an update was installed, restart the app
                if (updateResult != null)
                {
                    UpdateManager.RestartApp();
                }
            }

            // Continue with your application startup
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
