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
            textBoxSteamVRPath.Size = new Size(268, 23);
            textBoxSteamVRPath.TabIndex = 1;
            textBoxSteamVRPath.TextChanged += textBoxSteamVRPath_TextChanged;
            // 
            // buttonSave
            // 
            buttonSave.Location = new Point(205, 179);
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
            updateCheckBox.Size = new Size(131, 19);
            updateCheckBox.TabIndex = 4;
            updateCheckBox.Text = "Enable Auto Update";
            updateCheckBox.UseVisualStyleBackColor = true;
            updateCheckBox.CheckedChanged += updateCheckBox_CheckedChanged;
            // 
            // currentVersionLink
            // 
            currentVersionLink.AutoSize = true;
            currentVersionLink.Location = new Point(12, 183);
            currentVersionLink.Name = "currentVersionLink";
            currentVersionLink.Size = new Size(94, 15);
            currentVersionLink.TabIndex = 5;
            currentVersionLink.TabStop = true;
            currentVersionLink.Text = "Current Version: ";
            currentVersionLink.LinkClicked += linkLabelVersion_LinkClicked;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(291, 210);
            Controls.Add(currentVersionLink);
            Controls.Add(updateCheckBox);
            Controls.Add(buttonSave);
            Controls.Add(textBoxSteamVRPath);
            Controls.Add(labelSteamVRPath);
            Name = "SettingsForm";
            Text = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelSteamVRPath;
        private TextBox textBoxSteamVRPath;
        private Button buttonSave;
        private CheckBox updateCheckBox;
        private LinkLabel currentVersionLink;
    }
}