using Toggle_SteamVR.src;
using System.Xml.Linq;
using System.Diagnostics;
using System.Reflection;

namespace Toggle_SteamVR
{
    public partial class SettingsForm : Form
    {
        private string repoBaseUrl = "https://github.com/SoBo7a/Toggle_SteamVR";
        private string appVersion;
        private string steamVRPath;
        private bool autoUpdateEnabled;
        private bool startWithWindows;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
            DisplayVersion();
        }

        private void LoadSettings()
        {
            ConfigurationManager.LoadConfiguration();
            textBoxSteamVRPath.Text = ConfigurationManager.SteamVRPath;
            updateCheckBox.Checked = ConfigurationManager.AutoUpdateEnabled;
            startWithWindowsCheckBox.Checked = ConfigurationManager.StartWithWindows;
        }

        private void textBoxSteamVRPath_TextChanged(object sender, EventArgs e)
        {
            steamVRPath = textBoxSteamVRPath.Text;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ConfigurationManager.SaveConfiguration(steamVRPath, autoUpdateEnabled, startWithWindows);

            ConfigurationManager.LoadConfiguration();

            MessageBox.Show("Configuration saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoUpdateEnabled = updateCheckBox.Checked;

        }

        private void startWithWindowsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            startWithWindows = startWithWindowsCheckBox.Checked;
        }

        private void DisplayVersion()
        {
            appVersion = GetFormattedVersion();
            currentVersionLink.Text = $"Current Version: {appVersion}";
            currentVersionLink.Links.Add(17, appVersion.Length);
        }

        private string GetFormattedVersion()
        {
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            return version.Revision == 0 ? version.ToString(3) : version.ToString();
        }

        private string GetGitHubLink(string version)
        {
            return $"{repoBaseUrl}/tree/{version}/CHANGELOG.md";
        }

        private void linkLabelVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = GetGitHubLink(appVersion);

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to open link: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void manageDisableAppsButton_Click(object sender, EventArgs e)
        {
            ManageAppsForm manageAppsForm = new ManageAppsForm();
            manageAppsForm.ShowDialog();
        }
    }
}
