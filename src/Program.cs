using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Toggle_SteamVR.src
{
    internal static class Program
    {
        private static Mutex mutex = null;

        [STAThread]
        static async Task Main()
        {
            const string appName = "Toggle_SteamVR";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("The application is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the app is running from the installed location or the built exe is used for development
            ConfigurationManager.LoadConfiguration();
            bool autoUpdateEnabled = ConfigurationManager.AutoUpdateEnabled;

            if (IsRunningFromInstalledLocation() && autoUpdateEnabled)
            {
                await Updater.CheckAndUpdate();
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new NotificationForm());

            GC.KeepAlive(mutex);
        }

        private static bool IsRunningFromInstalledLocation()
        {
            string exePath = Application.ExecutablePath;
            string exeDirectory = Path.GetDirectoryName(exePath);

            string installedLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ToggleSteamVR";

            return exeDirectory.StartsWith(installedLocation, StringComparison.OrdinalIgnoreCase);
        }
    }
}
