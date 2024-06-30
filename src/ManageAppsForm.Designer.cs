namespace Toggle_SteamVR.src
{
    partial class ManageAppsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listBoxDisableApps = new ListBox();
            textBoxAppName = new TextBox();
            buttonAddApp = new Button();
            buttonRemoveApp = new Button();
            exitButton = new Button();
            SuspendLayout();
            // 
            // listBoxDisableApps
            // 
            listBoxDisableApps.FormattingEnabled = true;
            listBoxDisableApps.ItemHeight = 15;
            listBoxDisableApps.Location = new Point(12, 12);
            listBoxDisableApps.Name = "listBoxDisableApps";
            listBoxDisableApps.Size = new Size(218, 79);
            listBoxDisableApps.TabIndex = 0;
            // 
            // textBoxAppName
            // 
            textBoxAppName.Location = new Point(12, 97);
            textBoxAppName.Name = "textBoxAppName";
            textBoxAppName.Size = new Size(218, 23);
            textBoxAppName.TabIndex = 1;
            // 
            // buttonAddApp
            // 
            buttonAddApp.Location = new Point(236, 97);
            buttonAddApp.Name = "buttonAddApp";
            buttonAddApp.Size = new Size(75, 23);
            buttonAddApp.TabIndex = 2;
            buttonAddApp.Text = "Add";
            buttonAddApp.UseVisualStyleBackColor = true;
            buttonAddApp.Click += buttonAddApp_Click;
            // 
            // buttonRemoveApp
            // 
            buttonRemoveApp.Location = new Point(236, 41);
            buttonRemoveApp.Name = "buttonRemoveApp";
            buttonRemoveApp.Size = new Size(75, 23);
            buttonRemoveApp.TabIndex = 3;
            buttonRemoveApp.Text = "Remove";
            buttonRemoveApp.UseVisualStyleBackColor = true;
            buttonRemoveApp.Click += buttonRemoveApp_Click;
            // 
            // exitButton
            // 
            exitButton.Location = new Point(12, 132);
            exitButton.Name = "exitButton";
            exitButton.Size = new Size(75, 23);
            exitButton.TabIndex = 4;
            exitButton.Text = "Exit";
            exitButton.UseVisualStyleBackColor = true;
            exitButton.Click += exitButton_Click;
            // 
            // ManageAppsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(323, 167);
            Controls.Add(exitButton);
            Controls.Add(buttonRemoveApp);
            Controls.Add(buttonAddApp);
            Controls.Add(textBoxAppName);
            Controls.Add(listBoxDisableApps);
            Name = "ManageAppsForm";
            Text = "Auto-Disable Apps";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBoxDisableApps;
        private TextBox textBoxAppName;
        private Button buttonAddApp;
        private Button buttonRemoveApp;
        private Button exitButton;
    }
}