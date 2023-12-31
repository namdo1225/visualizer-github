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
            label3 = new Label();
            themeCombo = new ComboBox();
            label8 = new Label();
            credentialsLabel = new Label();
            configButton = new Button();
            linksText = new Label();
            label7 = new Label();
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
            label5 = new Label();
            label1 = new Label();
            label4 = new Label();
            label6 = new Label();
            label2 = new Label();
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
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(3, 574);
            label3.Name = "label3";
            label3.Size = new Size(303, 84);
            label3.TabIndex = 16;
            label3.Text = "Select the theme:";
            label3.TextAlign = ContentAlignment.TopCenter;
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
            // label8
            // 
            label8.Dock = DockStyle.Fill;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ImageAlign = ContentAlignment.BottomLeft;
            label8.Location = new Point(3, 82);
            label8.Name = "label8";
            tableLayoutPanel1.SetRowSpan(label8, 2);
            label8.Size = new Size(303, 164);
            label8.TabIndex = 31;
            label8.Text = "Credits:\r\nNam Do - Forked Developer\r\n\r\nRyan Pecha - Backend\r\nKyle Walker - Frontend\r\nPatrick Comden - Frontend";
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
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label7.Location = new Point(621, 492);
            label7.Name = "label7";
            label7.Size = new Size(303, 82);
            label7.TabIndex = 26;
            label7.Text = "Others";
            label7.TextAlign = ContentAlignment.MiddleCenter;
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
            // label5
            // 
            label5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(1146, 637);
            label5.Name = "label5";
            label5.Size = new Size(87, 21);
            label5.TabIndex = 18;
            label5.Text = "Version 1.0\r\n";
            label5.TextAlign = ContentAlignment.BottomRight;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            label1.Location = new Point(621, 246);
            label1.Name = "label1";
            label1.Size = new Size(303, 82);
            label1.TabIndex = 14;
            label1.Text = "Reset";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            tableLayoutPanel1.SetColumnSpan(label4, 2);
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(621, 328);
            label4.Name = "label4";
            label4.Size = new Size(612, 82);
            label4.TabIndex = 17;
            label4.Text = "You can delete stored credential in Credential Manager. If you want to delete your authorization for this app to use your repositories, you can go to GitHub and remove it.\r\n";
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(3, 328);
            label6.Name = "label6";
            label6.Size = new Size(303, 82);
            label6.TabIndex = 22;
            label6.Text = "Store/retrieve credentials (assuming \"Remember Me\" is checked and access token is available).";
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(312, 82);
            label2.Name = "label2";
            tableLayoutPanel1.SetRowSpan(label2, 2);
            label2.Size = new Size(303, 164);
            label2.TabIndex = 30;
            label2.Text = "You grant the app permision to read/write to your repositories on your behalf. As you can see from our code, we do not collect your data or make unauthorized actions without your permission.";
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
            tableLayoutPanel1.Controls.Add(label2, 1, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 7);
            tableLayoutPanel1.Controls.Add(themeText, 0, 6);
            tableLayoutPanel1.Controls.Add(storeCredButton, 0, 5);
            tableLayoutPanel1.Controls.Add(retrieveCredButton, 1, 5);
            tableLayoutPanel1.Controls.Add(label6, 0, 4);
            tableLayoutPanel1.Controls.Add(rememberMeCheckbox, 1, 4);
            tableLayoutPanel1.Controls.Add(label1, 2, 3);
            tableLayoutPanel1.Controls.Add(label4, 2, 4);
            tableLayoutPanel1.Controls.Add(deleteCredButton, 2, 5);
            tableLayoutPanel1.Controls.Add(authButton, 3, 5);
            tableLayoutPanel1.Controls.Add(label7, 2, 6);
            tableLayoutPanel1.Controls.Add(configButton, 2, 7);
            tableLayoutPanel1.Controls.Add(label5, 3, 7);
            tableLayoutPanel1.Controls.Add(themeCombo, 1, 7);
            tableLayoutPanel1.Controls.Add(label8, 0, 1);
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
            label2.ForeColor = theme.TextNormal;

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
        private Label label3;
        private ComboBox themeCombo;
        private Label label8;
        private Label credentialsLabel;
        private Button configButton;
        private Label linksText;
        private Label label7;
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
        private Label label5;
        private Label label1;
        private Label label4;
        private Label label6;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
    }
}