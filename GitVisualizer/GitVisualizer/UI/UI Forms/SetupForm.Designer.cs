using GitVisualizer.UI;
using SkiaSharp;

namespace GitVisualizer
{
    /// <summary>
    /// Setup page Designer code for app, which handles logging into Github, granting user codes, and linking to Github site
    /// before allowing manipulation of Repos. Stores all components for the window form, and includes functionality for
    /// directly modifying page components such as color theme.
    /// </summary>
    partial class SetupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
            workspaceText = new Label();
            instructionText01 = new Label();
            githubLoginButton = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            radioButton3 = new RadioButton();
            localWorkspaceButton = new Button();
            notifyIcon1 = new NotifyIcon(components);
            needHelpText = new Label();
            instructionText02 = new Label();
            userCodeLabelHeader = new Label();
            userCodeLabel = new Label();
            rememberMeCheckbox = new CheckBox();
            rememberMeLabel = new Label();
            authorizationPanel = new Panel();
            browserButton = new Button();
            clipboardButton = new Button();
            showCodeCheckBox = new CheckBox();
            repoTypeButton = new CheckBox();
            grantDeleteRepo = new CheckBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            authorizationPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // workspaceText
            // 
            workspaceText.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(workspaceText, 2);
            workspaceText.Dock = DockStyle.Fill;
            workspaceText.Font = new Font("Segoe UI Semibold", 32.25F, FontStyle.Bold, GraphicsUnit.Point);
            workspaceText.Location = new Point(3, 0);
            workspaceText.Name = "workspaceText";
            workspaceText.Size = new Size(534, 56);
            workspaceText.TabIndex = 0;
            workspaceText.Text = "Workspace Setup";
            // 
            // instructionText01
            // 
            tableLayoutPanel1.SetColumnSpan(instructionText01, 2);
            instructionText01.Dock = DockStyle.Fill;
            instructionText01.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            instructionText01.Location = new Point(3, 56);
            instructionText01.Name = "instructionText01";
            tableLayoutPanel1.SetRowSpan(instructionText01, 2);
            instructionText01.Size = new Size(534, 112);
            instructionText01.TabIndex = 1;
            instructionText01.Text = resources.GetString("instructionText01.Text");
            // 
            // githubLoginButton
            // 
            githubLoginButton.Dock = DockStyle.Fill;
            githubLoginButton.FlatStyle = FlatStyle.Flat;
            githubLoginButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            githubLoginButton.Location = new Point(8, 176);
            githubLoginButton.Margin = new Padding(8);
            githubLoginButton.Name = "githubLoginButton";
            githubLoginButton.Size = new Size(254, 40);
            githubLoginButton.TabIndex = 2;
            githubLoginButton.Text = "Login Using Github.com";
            githubLoginButton.UseVisualStyleBackColor = true;
            githubLoginButton.Click += LoadMainAppFormRemote;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(radioButton1, 2);
            radioButton1.Dock = DockStyle.Fill;
            radioButton1.Location = new Point(8, 456);
            radioButton1.Margin = new Padding(8);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(524, 40);
            radioButton1.TabIndex = 3;
            radioButton1.TabStop = true;
            radioButton1.Text = "Joining or hosting a collaborative project on Github";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.Click += HighlightLoginButton;
            // 
            // radioButton2
            // 
            tableLayoutPanel1.SetColumnSpan(radioButton2, 2);
            radioButton2.Dock = DockStyle.Fill;
            radioButton2.Location = new Point(8, 568);
            radioButton2.Margin = new Padding(8);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(524, 44);
            radioButton2.TabIndex = 4;
            radioButton2.TabStop = true;
            radioButton2.Text = "Creating or cloning a project on the Github cloud";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.Click += HighlightLoginButton;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(radioButton3, 2);
            radioButton3.Dock = DockStyle.Fill;
            radioButton3.Location = new Point(8, 512);
            radioButton3.Margin = new Padding(8);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(524, 40);
            radioButton3.TabIndex = 5;
            radioButton3.TabStop = true;
            radioButton3.Text = "Using a local folder to handle version control on my own device\r\n";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.Click += HighlightLocalButton;
            // 
            // localWorkspaceButton
            // 
            localWorkspaceButton.Dock = DockStyle.Fill;
            localWorkspaceButton.FlatStyle = FlatStyle.Flat;
            localWorkspaceButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            localWorkspaceButton.Location = new Point(278, 176);
            localWorkspaceButton.Margin = new Padding(8);
            localWorkspaceButton.Name = "localWorkspaceButton";
            localWorkspaceButton.Size = new Size(254, 40);
            localWorkspaceButton.TabIndex = 6;
            localWorkspaceButton.Text = "Use Local Workspace";
            localWorkspaceButton.UseVisualStyleBackColor = true;
            localWorkspaceButton.Click += LoadMainAppFormLocal;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Text = "This will send you to an external site, proceed?";
            notifyIcon1.Visible = true;
            // 
            // needHelpText
            // 
            needHelpText.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(needHelpText, 2);
            needHelpText.Dock = DockStyle.Fill;
            needHelpText.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point);
            needHelpText.Location = new Point(3, 336);
            needHelpText.Name = "needHelpText";
            needHelpText.Size = new Size(534, 56);
            needHelpText.TabIndex = 7;
            needHelpText.Text = "Need Help?";
            // 
            // instructionText02
            // 
            tableLayoutPanel1.SetColumnSpan(instructionText02, 2);
            instructionText02.Dock = DockStyle.Fill;
            instructionText02.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            instructionText02.Location = new Point(3, 392);
            instructionText02.Name = "instructionText02";
            instructionText02.Size = new Size(534, 56);
            instructionText02.TabIndex = 8;
            instructionText02.Text = "Select the use case that applies to you, and the appropriate button will be highlighted.";
            // 
            // userCodeLabelHeader
            // 
            userCodeLabelHeader.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            userCodeLabelHeader.Location = new Point(12, 9);
            userCodeLabelHeader.Name = "userCodeLabelHeader";
            userCodeLabelHeader.Size = new Size(375, 126);
            userCodeLabelHeader.TabIndex = 9;
            userCodeLabelHeader.Text = "A Github webpage should have opened in your browser. \r\n\r\nEnter the following code on that page to authorize your device:\r\n";
            // 
            // userCodeLabel
            // 
            userCodeLabel.Font = new Font("Segoe UI", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            userCodeLabel.Location = new Point(33, 151);
            userCodeLabel.Name = "userCodeLabel";
            userCodeLabel.Size = new Size(343, 86);
            userCodeLabel.TabIndex = 10;
            userCodeLabel.Text = "1234-5678";
            userCodeLabel.TextAlign = ContentAlignment.MiddleCenter;
            userCodeLabel.Visible = false;
            // 
            // rememberMeCheckbox
            // 
            rememberMeCheckbox.AutoSize = true;
            rememberMeCheckbox.Checked = true;
            rememberMeCheckbox.CheckState = CheckState.Checked;
            rememberMeCheckbox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            rememberMeCheckbox.Location = new Point(12, 351);
            rememberMeCheckbox.Name = "rememberMeCheckbox";
            rememberMeCheckbox.Size = new Size(177, 34);
            rememberMeCheckbox.TabIndex = 11;
            rememberMeCheckbox.Text = "Remember Me";
            rememberMeCheckbox.UseVisualStyleBackColor = true;
            rememberMeCheckbox.CheckedChanged += RememberMeCheckboxChanged;
            // 
            // rememberMeLabel
            // 
            rememberMeLabel.Location = new Point(12, 390);
            rememberMeLabel.Name = "rememberMeLabel";
            rememberMeLabel.Size = new Size(391, 100);
            rememberMeLabel.TabIndex = 12;
            rememberMeLabel.Text = "If checked, your authorization code will be remembered so you will not have to authorize again each time you open the app.\r\n\r\nLeave unchecked to revoke access to your account when exiting the app. ";
            // 
            // authorizationPanel
            // 
            authorizationPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            authorizationPanel.Controls.Add(browserButton);
            authorizationPanel.Controls.Add(clipboardButton);
            authorizationPanel.Controls.Add(showCodeCheckBox);
            authorizationPanel.Controls.Add(userCodeLabelHeader);
            authorizationPanel.Controls.Add(rememberMeCheckbox);
            authorizationPanel.Controls.Add(rememberMeLabel);
            authorizationPanel.Controls.Add(userCodeLabel);
            authorizationPanel.Dock = DockStyle.Right;
            authorizationPanel.Location = new Point(540, 0);
            authorizationPanel.MinimumSize = new Size(300, 0);
            authorizationPanel.Name = "authorizationPanel";
            authorizationPanel.Size = new Size(410, 620);
            authorizationPanel.TabIndex = 13;
            authorizationPanel.Visible = false;
            // 
            // browserButton
            // 
            browserButton.FlatStyle = FlatStyle.Flat;
            browserButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            browserButton.ForeColor = SystemColors.MenuHighlight;
            browserButton.Location = new Point(220, 485);
            browserButton.Name = "browserButton";
            browserButton.Size = new Size(167, 45);
            browserButton.TabIndex = 15;
            browserButton.Text = "Open Browser";
            browserButton.UseVisualStyleBackColor = true;
            browserButton.Click += BrowserButton_Click;
            // 
            // clipboardButton
            // 
            clipboardButton.FlatStyle = FlatStyle.Flat;
            clipboardButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            clipboardButton.ForeColor = SystemColors.MenuHighlight;
            clipboardButton.Location = new Point(33, 485);
            clipboardButton.Name = "clipboardButton";
            clipboardButton.Size = new Size(167, 45);
            clipboardButton.TabIndex = 14;
            clipboardButton.Text = "Copy to Clipboard";
            clipboardButton.UseVisualStyleBackColor = true;
            clipboardButton.Click += ClipboardButton_Click;
            // 
            // showCodeCheckBox
            // 
            showCodeCheckBox.AutoSize = true;
            showCodeCheckBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            showCodeCheckBox.Location = new Point(62, 288);
            showCodeCheckBox.Name = "showCodeCheckBox";
            showCodeCheckBox.Size = new Size(215, 25);
            showCodeCheckBox.TabIndex = 13;
            showCodeCheckBox.Text = "Show Code (Sensitive Info)";
            showCodeCheckBox.UseVisualStyleBackColor = true;
            showCodeCheckBox.CheckedChanged += ShowCodeCheckboxChanged;
            // 
            // repoTypeButton
            // 
            repoTypeButton.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(repoTypeButton, 2);
            repoTypeButton.Dock = DockStyle.Fill;
            repoTypeButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            repoTypeButton.Location = new Point(8, 232);
            repoTypeButton.Margin = new Padding(8);
            repoTypeButton.Name = "repoTypeButton";
            repoTypeButton.Size = new Size(524, 40);
            repoTypeButton.TabIndex = 16;
            repoTypeButton.Text = "Grant access to all repo (Public by default)";
            repoTypeButton.UseVisualStyleBackColor = true;
            // 
            // grantDeleteRepo
            // 
            grantDeleteRepo.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(grantDeleteRepo, 2);
            grantDeleteRepo.Dock = DockStyle.Fill;
            grantDeleteRepo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            grantDeleteRepo.Location = new Point(8, 288);
            grantDeleteRepo.Margin = new Padding(8);
            grantDeleteRepo.Name = "grantDeleteRepo";
            grantDeleteRepo.Size = new Size(524, 40);
            grantDeleteRepo.TabIndex = 17;
            grantDeleteRepo.Text = "Grant delete access to repositories (Off by default)";
            grantDeleteRepo.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(workspaceText, 0, 0);
            tableLayoutPanel1.Controls.Add(grantDeleteRepo, 0, 5);
            tableLayoutPanel1.Controls.Add(radioButton3, 0, 9);
            tableLayoutPanel1.Controls.Add(instructionText02, 0, 7);
            tableLayoutPanel1.Controls.Add(instructionText01, 0, 1);
            tableLayoutPanel1.Controls.Add(radioButton1, 0, 8);
            tableLayoutPanel1.Controls.Add(needHelpText, 0, 6);
            tableLayoutPanel1.Controls.Add(repoTypeButton, 0, 4);
            tableLayoutPanel1.Controls.Add(githubLoginButton, 0, 3);
            tableLayoutPanel1.Controls.Add(localWorkspaceButton, 1, 3);
            tableLayoutPanel1.Controls.Add(radioButton2, 0, 10);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 11;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.090909F));
            tableLayoutPanel1.Size = new Size(540, 620);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // SetupForm
            // 
            AutoScaleMode = AutoScaleMode.Inherit;
            ClientSize = new Size(950, 620);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(authorizationPanel);
            Name = "SetupForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GitHelper Login";
            Load += SetupForm_Load;
            authorizationPanel.ResumeLayout(false);
            authorizationPanel.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private void ApplyColorTheme(UITheme.AppTheme theme)
        {
            BackColor = theme.AppBackground;
            ForeColor = theme.TextSoft;

            workspaceText.ForeColor = theme.TextHeader;
            instructionText01.ForeColor = theme.TextNormal;

            userCodeLabelHeader.ForeColor = theme.TextBright;
            userCodeLabel.ForeColor = theme.TextSelectable;


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

            authorizationPanel.BackColor = theme.PanelBackground;
            rememberMeCheckbox.ForeColor = theme.TextSelectable;
            rememberMeLabel.ForeColor = theme.TextNormal;
        }

        private Label workspaceText;
        private Label instructionText01;
        private Button githubLoginButton;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private Button localWorkspaceButton;
        private NotifyIcon notifyIcon1;
        private Label needHelpText;
        private Label instructionText02;
        private Label userCodeLabelHeader;
        private Label userCodeLabel;
        private CheckBox rememberMeCheckbox;
        private Label rememberMeLabel;
        private Panel authorizationPanel;
        private CheckBox showCodeCheckBox;
        private Button clipboardButton;
        private Button browserButton;
        private CheckBox repoTypeButton;
        private CheckBox grantDeleteRepo;
        private TableLayoutPanel tableLayoutPanel1;
    }
}