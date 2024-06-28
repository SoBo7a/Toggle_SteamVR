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

            await Updater.CheckAndUpdate(releasesUrl);

            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
