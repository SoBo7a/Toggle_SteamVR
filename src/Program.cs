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
            await Updater.CheckAndUpdate();

            ApplicationConfiguration.Initialize();
            Application.Run(new NotificationForm());
        }
    }
}
