namespace Toggle_SteamVR
{
    partial class SettingsForm
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
            labelSteamVRPath = new Label();
            textBoxSteamVRPath = new TextBox();
            buttonSave = new Button();
            updateCheckBox = new CheckBox();
            currentVersionLink = new LinkLabel();
            startWithWindowsCheckBox = new CheckBox();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // labelSteamVRPath
            // 
            labelSteamVRPath.AutoSize = true;
            labelSteamVRPath.Location = new Point(12, 9);
            labelSteamVRPath.Name = "labelSteamVRPath";
            labelSteamVRPath.Size = new Size(84, 15);
            labelSteamVRPath.TabIndex = 0;
            labelSteamVRPath.Text = "SteamVR Path:";
            // 
            // textBoxSteamVRPath
            // 
            textBoxSteamVRPath.Location = new Point(12, 27);
            textBoxSteamVRPath.Name = "textBoxSteamVRPath";
            textBoxSteamVRPath.Size = new Size(286, 23);
            textBoxSteamVRPath.TabIndex = 1;
            textBoxSteamVRPath.TextChanged += textBoxSteamVRPath_TextChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(223, 146);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new Size(75, 23);
            buttonSave.TabIndex = 3;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // updateCheckBox
            // 
            updateCheckBox.AutoSize = true;
            updateCheckBox.Location = new Point(12, 69);
            updateCheckBox.Name = "updateCheckBox";
            updateCheckBox.Size = new Size(197, 19);
            updateCheckBox.TabIndex = 4;
            updateCheckBox.Text = "Automatically update on startup";
            updateCheckBox.UseVisualStyleBackColor = true;
            updateCheckBox.CheckedChanged += updateCheckBox_CheckedChanged;
            // 
            // currentVersionLink
            // 
            currentVersionLink.AutoSize = true;
            currentVersionLink.Location = new Point(12, 146);
            currentVersionLink.Name = "currentVersionLink";
            currentVersionLink.Size = new Size(94, 15);
            currentVersionLink.TabIndex = 5;
            currentVersionLink.TabStop = true;
            currentVersionLink.Text = "Current Version: ";
            currentVersionLink.LinkClicked += linkLabelVersion_LinkClicked;
            // 
            // startWithWindowsCheckBox
            // 
            startWithWindowsCheckBox.AutoSize = true;
            startWithWindowsCheckBox.Location = new Point(12, 94);
            startWithWindowsCheckBox.Name = "startWithWindowsCheckBox";
            startWithWindowsCheckBox.Size = new Size(128, 19);
            startWithWindowsCheckBox.TabIndex = 6;
            startWithWindowsCheckBox.Text = "Start with Windows";
            startWithWindowsCheckBox.UseVisualStyleBackColor = true;
            startWithWindowsCheckBox.CheckedChanged += startWithWindowsCheckBox_CheckedChanged;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(142, 146);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 7;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(310, 173);
            Controls.Add(cancelButton);
            Controls.Add(startWithWindowsCheckBox);
            Controls.Add(currentVersionLink);
            Controls.Add(updateCheckBox);
            Controls.Add(buttonSave);
            Controls.Add(textBoxSteamVRPath);
            Controls.Add(labelSteamVRPath);
            Name = "SettingsForm";
            Text = "Toggle SteamVR - Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSteamVRPath;
        private TextBox textBoxSteamVRPath;
        private Button buttonSave;
        private CheckBox updateCheckBox;
        private LinkLabel currentVersionLink;
        private CheckBox startWithWindowsCheckBox;
        private Button cancelButton;
    }
}