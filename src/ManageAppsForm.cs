namespace Toggle_SteamVR.src
{
    public partial class ManageAppsForm : Form
    {
        public ManageAppsForm()
        {
            InitializeComponent();
            LoadAppList();
        }

        private void buttonAddApp_Click(object sender, EventArgs e)
        {
            string appName = textBoxAppName.Text.Trim();

            if (!string.IsNullOrEmpty(appName) && !listBoxDisableApps.Items.Contains(appName))
            {
                listBoxDisableApps.Items.Add(appName);
                SaveAppList();

                textBoxAppName.Text = "";
            }
        }

        private void buttonRemoveApp_Click(object sender, EventArgs e)
        {
            if (listBoxDisableApps.SelectedItem != null)
            {
                listBoxDisableApps.Items.Remove(listBoxDisableApps.SelectedItem);
                SaveAppList();
            }
        }

        private void LoadAppList()
        {
            List<string> appList = ConfigurationManager.GetAppList();

            foreach (string app in appList)
            {
                listBoxDisableApps.Items.Add(app);
            }
        }

        private void SaveAppList()
        {
            List<string> appList = listBoxDisableApps.Items.Cast<string>().ToList();
            ConfigurationManager.AddToAppList(appList);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
