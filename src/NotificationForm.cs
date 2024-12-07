using System.Diagnostics;
using System.Reflection;
using Toggle_SteamVR.src;

namespace Toggle_SteamVR
{
    public partial class NotificationForm : Form
    {
        private Icon enabledIcon;
        private Icon disabledIcon;
        private string steamVRPath;
        private string disabledSteamVRPath;
        private System.Windows.Forms.Timer checkProcessTimer;
        private bool isLockedState;

        public NotificationForm()
        {
            InitializeComponent();
            LoadIcons();
            ConfigurationManager.LoadConfiguration();
            steamVRPath = ConfigurationManager.SteamVRPath;
            disabledSteamVRPath = steamVRPath + " - Disabled";
            InitializeTimer();
        }

        private void LoadIcons()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream("Toggle_SteamVR.Assets.steamvr_enabled.ico"))
            {
                enabledIcon = new Icon(stream);
            }
            using (var stream = assembly.GetManifestResourceStream("Toggle_SteamVR.Assets.steamvr_disabled.ico"))
            {
                disabledIcon = new Icon(stream);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            UpdateMenuItems();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Updater.CheckAndUpdate(true);
        }

        private void enableMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSteamVR(true);
        }

        private void disableMenuItem_Click(object sender, EventArgs e)
        {
            ToggleSteamVR(false);
        }

        private void ToggleSteamVR(bool enable)
        {
            string disabledSteamVRPath = steamVRPath + " - Disabled";

            try
            {
                if (enable)
                {
                    if (Directory.Exists(disabledSteamVRPath))
                    {
                        Directory.Move(disabledSteamVRPath, steamVRPath);
                        notifyIcon1.ShowBalloonTip(500, "SteamVR Enabled", "SteamVR has been enabled.", ToolTipIcon.Info);
                        notifyIcon1.Icon = enabledIcon;
                        UpdateMenuItems();
                    }
                    else
                    {
                        MessageBox.Show("SteamVR is already enabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (Directory.Exists(steamVRPath))
                    {
                        Directory.Move(steamVRPath, disabledSteamVRPath);
                        notifyIcon1.ShowBalloonTip(500, "SteamVR Disabled", "SteamVR has been disabled.", ToolTipIcon.Info);
                        notifyIcon1.Icon = disabledIcon;
                        UpdateMenuItems();
                    }
                    else
                    {
                        MessageBox.Show("SteamVR is already disabled.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMenuItems()
        {
            enableMenuItem.Checked = Directory.Exists(steamVRPath);
            disableMenuItem.Checked = Directory.Exists(disabledSteamVRPath);

            if (enableMenuItem.Checked)
            {
                notifyIcon1.Icon = enabledIcon;
            }
            else if (disableMenuItem.Checked)
            {
                notifyIcon1.Icon = disabledIcon;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void InitializeTimer()
        {
            checkProcessTimer = new System.Windows.Forms.Timer();
            checkProcessTimer.Interval = 500; // Check every 0.5 seconds
            checkProcessTimer.Tick += CheckProcessTimer_Tick;
            checkProcessTimer.Start();
        }

        private void CheckProcessTimer_Tick(object sender, EventArgs e)
        {
            CheckRunningProcesses();
        }

        private void CheckRunningProcesses()
        {
            List<string> disableApps = ConfigurationManager.GetAppList();
            var runningProcesses = Process.GetProcesses().Select(p => p.ProcessName).ToList();

            bool shouldDisable = disableApps.Any(app => runningProcesses.Contains(app, StringComparer.OrdinalIgnoreCase));
            bool isSteamRunning = runningProcesses.Contains("Steam", StringComparer.OrdinalIgnoreCase);

            if (isSteamRunning && Directory.Exists(disabledSteamVRPath))
            {
                ToggleSteamVR(true);
                notifyIcon1.ShowBalloonTip(500, "Steam Detected", "SteamVR has been re-enabled because Steam is running.", ToolTipIcon.Info);
                return;
            }

            if (shouldDisable && Directory.Exists(steamVRPath))
            {
                ToggleSteamVR(false);
                isLockedState = true;
                enableMenuItem.Enabled = false;
                disableMenuItem.Enabled = false;
            }
            else if (shouldDisable && Directory.Exists(steamVRPath + " - Disabled"))
            {
                enableMenuItem.Enabled = false;
                disableMenuItem.Enabled = false;
                return;
            }
            else if (!shouldDisable && isLockedState && Directory.Exists(steamVRPath + " - Disabled"))
            {
                ToggleSteamVR(true);
                isLockedState = false;
                enableMenuItem.Enabled = true;
                disableMenuItem.Enabled = true; 
            }
            else if (!shouldDisable && Directory.Exists(steamVRPath + " - Disabled"))
            {
                enableMenuItem.Enabled = true;
                disableMenuItem.Enabled = true;
                return;
            }
        }
    }
}
