using GitVisualizer.UI;
using SkiaSharp;

namespace GitVisualizer
{
    /// <summary>
    /// Setting pages for the app with various functionalities to adjust the app itself in some way.
    /// </summary>
    partial class SettingsForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            notifyIcon1 = new NotifyIcon(components);
            rememberMeCheckbox = new CheckBox();
            selectThemeLabel = new Label();
            themeCombo = new ComboBox();
            credits = new Label();
            credentialsLabel = new Label();
            configButton = new Button();
            linksText = new Label();
            othersLabel = new Label();
            button3 = new Button();
            deleteCredButton = new Button();
            themeText = new Label();
            settingsText = new Label();
            authButton = new Button();
            credentialManagerButton = new Button();
            appRepoButton = new Button();
            storeCredButton = new Button();
            retrieveCredButton = new Button();
            namWebsiteButton = new Button();
            versionLabel = new Label();
            resetLabel = new Label();
            resetInstruction = new Label();
            credentialsInstruction = new Label();
            permission = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "This will send you to an external site, proceed?";
            notifyIcon1.Visible = true;
            // 
            // rememberMeCheckbox
            // 
            rememberMeCheckbox.AutoSize = true;
            rememberMeCheckbox.Checked = true;
            rememberMeCheckbox.CheckState = CheckState.Checked;
            rememberMeCheckbox.Dock = DockStyle.Fill;
            rememberMeCheckbox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            rememberMeCheckbox.Location = new Point(312, 331);
            rememberMeCheckbox.Name = "rememberMeCheckbox";
            rememberMeCheckbox.Size = new Size(303, 76);
            rememberMeCheckbox.TabIndex = 25;
            rememberMeCheckbox.Text = "Remember Me";
            rememberMeCheckbox.TextAlign = ContentAlignment.MiddleCenter;
            rememberMeCheckbox.UseVisualStyleBackColor = true;
            rememberMeCheckbox.CheckedChanged += RemMeCheck_Change;
            // 
            // selectThemeLabel
            // 
            selectThemeLabel.Dock = DockStyle.Fill;
            selectThemeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            selectThemeLabel.Location = new Point(3, 574);
            selectThemeLabel.Name = "selectThemeLabel";
            selectThemeLabel.Size = new Size(303, 84);
            selectThemeLabel.TabIndex = 16;
            selectThemeLabel.Text = "Select the theme:";
            selectThemeLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // themeCombo
            // 
            themeCombo.Dock = DockStyle.Fill;
            themeCombo.FormattingEnabled = true;
            themeCombo.Items.AddRange(new object[] { "Dark", "Blue Light", "Blue Dark" });
            themeCombo.Location = new Point(312, 577);
            themeCombo.Name = "themeCombo";
            themeCombo.Size = new Size(303, 23);
            themeCombo.TabIndex = 15;
            themeCombo.SelectedIndexChanged += ThemeCombo_Change;
            // 
            // credits
            // 
            credits.Dock = DockStyle.Fill;
            credits.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            credits.ImageAlign = ContentAlignment.BottomLeft;
            credits.Location = new Point(3, 82);
            credits.Name = "credits";
            tableLayoutPanel1.SetRowSpan(credits, 2);
            credits.Size = new Size(303, 164);
            credits.TabIndex = 31;
            credits.Text = "Credits:\r\nNam Do - Forked Developer\r\n\r\nRyan Pecha - Backend\r\nKyle Walker - Frontend\r\nPatrick Comden - Frontend";
            // 
            // credentialsLabel
            // 
            credentialsLabel.AutoSize = true;
            credentialsLabel.Dock = DockStyle.Fill;
            credentialsLabel.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            credentialsLabel.Location = new Point(3, 246);
            credentialsLabel.Name = "credentialsLabel";
            credentialsLabel.Size = new Size(303, 82);
            credentialsLabel.TabIndex = 21;
            credentialsLabel.Text = "Credentials";
            credentialsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // configButton
            // 
            configButton.Dock = DockStyle.Fill;
            configButton.FlatStyle = FlatStyle.Flat;
            configButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            configButton.Location = new Point(621, 577);
            configButton.Name = "configButton";
            configButton.Size = new Size(303, 78);
            configButton.TabIndex = 27;
            configButton.Text = "View Config Files";
            configButton.UseVisualStyleBackColor = true;
            configButton.Click += ConfigButton_Click;
            // 
            // linksText
            // 
            linksText.AutoSize = true;
            linksText.Dock = DockStyle.Fill;
            linksText.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            linksText.Location = new Point(621, 0);
            linksText.Name = "linksText";
            linksText.Size = new Size(303, 82);
            linksText.TabIndex = 9;
            linksText.Text = "Links";
            linksText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // othersLabel
            // 
            othersLabel.AutoSize = true;
            othersLabel.Dock = DockStyle.Fill;
            othersLabel.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            othersLabel.Location = new Point(621, 492);
            othersLabel.Name = "othersLabel";
            othersLabel.Size = new Size(303, 82);
            othersLabel.TabIndex = 26;
            othersLabel.Text = "Others";
            othersLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Fill;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(930, 167);
            button3.Name = "button3";
            button3.Size = new Size(303, 76);
            button3.TabIndex = 13;
            button3.Text = "Instructions";
            button3.UseVisualStyleBackColor = true;
            // 
            // deleteCredButton
            // 
            deleteCredButton.Dock = DockStyle.Fill;
            deleteCredButton.FlatStyle = FlatStyle.Flat;
            deleteCredButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            deleteCredButton.Location = new Point(621, 413);
            deleteCredButton.Name = "deleteCredButton";
            deleteCredButton.Size = new Size(303, 76);
            deleteCredButton.TabIndex = 19;
            deleteCredButton.Text = "Delete Stored Credential";
            deleteCredButton.UseVisualStyleBackColor = true;
            deleteCredButton.Click += DeleteCredButton_Click;
            // 
            // themeText
            // 
            themeText.CausesValidation = false;
            themeText.Dock = DockStyle.Fill;
            themeText.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            themeText.Location = new Point(3, 492);
            themeText.Name = "themeText";
            themeText.Size = new Size(303, 82);
            themeText.TabIndex = 7;
            themeText.Text = "Theme";
            themeText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // settingsText
            // 
            settingsText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            settingsText.AutoSize = true;
            settingsText.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            settingsText.Location = new Point(3, 0);
            settingsText.Name = "settingsText";
            settingsText.Size = new Size(303, 82);
            settingsText.TabIndex = 29;
            settingsText.Text = "Settings";
            settingsText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // authButton
            // 
            authButton.Dock = DockStyle.Fill;
            authButton.FlatStyle = FlatStyle.Flat;
            authButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            authButton.Location = new Point(930, 413);
            authButton.Name = "authButton";
            authButton.Size = new Size(303, 76);
            authButton.TabIndex = 20;
            authButton.Text = "Open OAuth Applications";
            authButton.UseVisualStyleBackColor = true;
            authButton.Click += AuthButton_Click;
            // 
            // credentialManagerButton
            // 
            credentialManagerButton.Dock = DockStyle.Fill;
            credentialManagerButton.FlatStyle = FlatStyle.Flat;
            credentialManagerButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            credentialManagerButton.Location = new Point(621, 167);
            credentialManagerButton.Name = "credentialManagerButton";
            credentialManagerButton.Size = new Size(303, 76);
            credentialManagerButton.TabIndex = 10;
            credentialManagerButton.Text = "Credential Manager";
            credentialManagerButton.UseVisualStyleBackColor = true;
            credentialManagerButton.Click += CredManagerButton_Click;
            // 
            // appRepoButton
            // 
            appRepoButton.Dock = DockStyle.Fill;
            appRepoButton.FlatStyle = FlatStyle.Flat;
            appRepoButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            appRepoButton.Location = new Point(621, 85);
            appRepoButton.Name = "appRepoButton";
            appRepoButton.Size = new Size(303, 76);
            appRepoButton.TabIndex = 11;
            appRepoButton.Text = "App Repository";
            appRepoButton.UseVisualStyleBackColor = true;
            appRepoButton.Click += AppRepoButton_Click;
            // 
            // storeCredButton
            // 
            storeCredButton.Dock = DockStyle.Fill;
            storeCredButton.FlatStyle = FlatStyle.Flat;
            storeCredButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            storeCredButton.Location = new Point(3, 413);
            storeCredButton.Name = "storeCredButton";
            storeCredButton.Size = new Size(303, 76);
            storeCredButton.TabIndex = 24;
            storeCredButton.Text = "Store Credentials";
            storeCredButton.UseVisualStyleBackColor = true;
            storeCredButton.Click += StoreCredButton_Click;
            // 
            // retrieveCredButton
            // 
            retrieveCredButton.Dock = DockStyle.Fill;
            retrieveCredButton.FlatStyle = FlatStyle.Flat;
            retrieveCredButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            retrieveCredButton.Location = new Point(312, 413);
            retrieveCredButton.Name = "retrieveCredButton";
            retrieveCredButton.Size = new Size(303, 76);
            retrieveCredButton.TabIndex = 23;
            retrieveCredButton.Text = "Retrieve Credentials";
            retrieveCredButton.UseVisualStyleBackColor = true;
            retrieveCredButton.Click += RetrieveCredButton_Click;
            // 
            // namWebsiteButton
            // 
            namWebsiteButton.Dock = DockStyle.Fill;
            namWebsiteButton.FlatStyle = FlatStyle.Flat;
            namWebsiteButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            namWebsiteButton.Location = new Point(930, 85);
            namWebsiteButton.Name = "namWebsiteButton";
            namWebsiteButton.Size = new Size(303, 76);
            namWebsiteButton.TabIndex = 12;
            namWebsiteButton.Text = "Nam's Website";
            namWebsiteButton.UseVisualStyleBackColor = true;
            namWebsiteButton.Click += NamWebsite_Click;
            // 
            // versionLabel
            // 
            versionLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            versionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            versionLabel.Location = new Point(1146, 637);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new Size(87, 21);
            versionLabel.TabIndex = 18;
            versionLabel.Text = "Version 1.0\r\n";
            versionLabel.TextAlign = ContentAlignment.BottomRight;
            // 
            // resetLabel
            // 
            resetLabel.Dock = DockStyle.Fill;
            resetLabel.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            resetLabel.Location = new Point(621, 246);
            resetLabel.Name = "resetLabel";
            resetLabel.Size = new Size(303, 82);
            resetLabel.TabIndex = 14;
            resetLabel.Text = "Reset";
            resetLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // resetInstruction
            // 
            tableLayoutPanel1.SetColumnSpan(resetInstruction, 2);
            resetInstruction.Dock = DockStyle.Fill;
            resetInstruction.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            resetInstruction.Location = new Point(621, 328);
            resetInstruction.Name = "resetInstruction";
            resetInstruction.Size = new Size(612, 82);
            resetInstruction.TabIndex = 17;
            resetInstruction.Text = "You can delete stored credential in Credential Manager. If you want to delete your authorization for this app to use your repositories, you can go to GitHub and remove it.\r\n";
            // 
            // credentialsInstruction
            // 
            credentialsInstruction.Dock = DockStyle.Fill;
            credentialsInstruction.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            credentialsInstruction.Location = new Point(3, 328);
            credentialsInstruction.Name = "credentialsInstruction";
            credentialsInstruction.Size = new Size(303, 82);
            credentialsInstruction.TabIndex = 22;
            credentialsInstruction.Text = "Store/retrieve credentials (assuming \"Remember Me\" is checked and access token is available).";
            // 
            // permission
            // 
            permission.Dock = DockStyle.Fill;
            permission.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            permission.Location = new Point(312, 82);
            permission.Name = "permission";
            tableLayoutPanel1.SetRowSpan(permission, 2);
            permission.Size = new Size(303, 164);
            permission.TabIndex = 30;
            permission.Text = "You grant the app permision to read/write to your repositories on your behalf. As you can see from our code, we do not collect your data or make unauthorized actions without your permission.";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Controls.Add(namWebsiteButton, 3, 1);
            tableLayoutPanel1.Controls.Add(appRepoButton, 2, 1);
            tableLayoutPanel1.Controls.Add(credentialManagerButton, 2, 2);
            tableLayoutPanel1.Controls.Add(settingsText, 0, 0);
            tableLayoutPanel1.Controls.Add(button3, 3, 2);
            tableLayoutPanel1.Controls.Add(linksText, 2, 0);
            tableLayoutPanel1.Controls.Add(permission, 1, 1);
            tableLayoutPanel1.Controls.Add(selectThemeLabel, 0, 7);
            tableLayoutPanel1.Controls.Add(themeText, 0, 6);
            tableLayoutPanel1.Controls.Add(storeCredButton, 0, 5);
            tableLayoutPanel1.Controls.Add(retrieveCredButton, 1, 5);
            tableLayoutPanel1.Controls.Add(credentialsInstruction, 0, 4);
            tableLayoutPanel1.Controls.Add(rememberMeCheckbox, 1, 4);
            tableLayoutPanel1.Controls.Add(resetLabel, 2, 3);
            tableLayoutPanel1.Controls.Add(resetInstruction, 2, 4);
            tableLayoutPanel1.Controls.Add(deleteCredButton, 2, 5);
            tableLayoutPanel1.Controls.Add(authButton, 3, 5);
            tableLayoutPanel1.Controls.Add(othersLabel, 2, 6);
            tableLayoutPanel1.Controls.Add(configButton, 2, 7);
            tableLayoutPanel1.Controls.Add(versionLabel, 3, 7);
            tableLayoutPanel1.Controls.Add(themeCombo, 1, 7);
            tableLayoutPanel1.Controls.Add(credits, 0, 1);
            tableLayoutPanel1.Controls.Add(credentialsLabel, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10.1724138F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.827586F));
            tableLayoutPanel1.Size = new Size(1236, 658);
            tableLayoutPanel1.TabIndex = 31;
            // 
            // SettingsForm
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(1236, 658);
            Controls.Add(tableLayoutPanel1);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GitHelper Login";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
#pragma warning disable Commentor // Missing comments.
        private void ApplyColorTheme(UITheme.AppTheme theme)
#pragma warning restore Commentor // Missing comments.
        {
            BackColor = theme.AppBackground;
            ForeColor = theme.TextSoft;

            settingsText.ForeColor = theme.TextHeader;
            permission.ForeColor = theme.TextNormal;

            /// Apply themes to all buttons
            IEnumerable<Control> buttons = this.Controls.OfType<Control>().Where(x => x is Button);
            foreach (Button button in buttons)
            {
                button.BackColor = theme.ElementBackground;
                button.ForeColor = theme.TextSelectable;
            }
            /// Apply themes to all radio buttons
            IEnumerable<Control> radios = this.Controls.OfType<RadioButton>().Where(x => x is RadioButton);
            foreach (RadioButton radio in radios)
            {
                radio.ForeColor = theme.TextNormal;
            }
        }
        private NotifyIcon notifyIcon1;
        private CheckBox rememberMeCheckbox;
        private Label selectThemeLabel;
        private ComboBox themeCombo;
        private Label credits;
        private Label credentialsLabel;
        private Button configButton;
        private Label linksText;
        private Label othersLabel;
        private Button button3;
        private Button deleteCredButton;
        private Label themeText;
        private Label settingsText;
        private Button authButton;
        private Button credentialManagerButton;
        protected internal Button appRepoButton;
        private Button storeCredButton;
        private Button retrieveCredButton;
        private Button namWebsiteButton;
        private Label versionLabel;
        private Label resetLabel;
        private Label resetInstruction;
        private Label credentialsInstruction;
        private Label permission;
        private TableLayoutPanel tableLayoutPanel1;
    }
}