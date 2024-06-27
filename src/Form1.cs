using System.Reflection;
using System.Xml.Linq;

namespace Toggle_SteamVR
{
    public partial class Form1 : Form
    {
        private Icon enabledIcon;
        private Icon disabledIcon;
        private string steamVRPath;

        public Form1()
        {
            InitializeComponent();
            LoadIcons();
            LoadConfiguration();
        }

        private void LoadIcons()
        {
            // Load icons from embedded resources
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

        private void LoadConfiguration()
        {
            try
            {
                // Load configuration file
                XDocument doc = XDocument.Load("config.xml");
                XElement steamVRConfig = doc.Element("config")?.Element("steamVR");
                if (steamVRConfig != null)
                {
                    steamVRPath = steamVRConfig.Element("installPath")?.Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading configuration: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (string.IsNullOrEmpty(steamVRPath))
            {
                MessageBox.Show("SteamVR installation path not found in configuration.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close(); // Close the application if the path is not found
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
                        notifyIcon1.ShowBalloonTip(500, "SteamVR Enabled - Test", "SteamVR has been enabled.", ToolTipIcon.Info);
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
                        notifyIcon1.ShowBalloonTip(500, "SteamVR Disabled - Test", "SteamVR has been disabled.", ToolTipIcon.Info);
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
            string disabledSteamVRPath = steamVRPath + " - Disabled";

            enableMenuItem.Checked = Directory.Exists(steamVRPath);
            disableMenuItem.Checked = Directory.Exists(disabledSteamVRPath);

            // Set the correct icon based on the current state
            if (enableMenuItem.Checked)
            {
                notifyIcon1.Icon = enabledIcon;
            }
            else if (disableMenuItem.Checked)
            {
                notifyIcon1.Icon = disabledIcon;
            }
        }
    }
}