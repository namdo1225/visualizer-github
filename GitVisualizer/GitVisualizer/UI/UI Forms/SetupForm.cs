using GitVisualizer.UI.UI_Forms;
using System.Diagnostics;

using GithubSpace;
using GitVisualizer.backend;

namespace GitVisualizer
{
    /// <summary>
    /// Code for the Workspace Form Window, including event handlers, color theme settings, and component initialization
    /// </summary>
    public partial class SetupForm : Form
    {

        /// <summary>
        /// The SetupForm constructor.
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
            FormClosing += new FormClosingEventHandler(LoadMainAppFormLocal); // Open main window when closing this one, skipping Auth
        }

        /// <summary>
        /// Runs when SetupForm load.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void SetupForm_Load(object sender, EventArgs e)
        {
            rememberMeCheckbox.Checked = GVSettings.Data.RememberGitHubLogin;
        }

        /// <summary>
        /// Event handler that highlights the Github login button to signify login is required for user's desired use case.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void HighlightLoginButton(object sender, EventArgs e)
        {
            githubLoginButton.Select();
        }

        /// <summary>
        /// Event handler that highlights the Local Workspace button to signify no login is required for user's desired use case.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void HighlightLocalButton(object sender, EventArgs e)
        {
            localWorkspaceButton.Select();
        }

        /// <summary>
        /// Loads main page routed from Remote login button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadMainAppFormRemote(object sender, EventArgs e)
        {
            GetPermissionGithub();
            OpenExternalWebsite(Github.API_DEVICE_LOGIN_CODE_URL);
        }

        /// <summary>
        /// Requests Github App for permission, and shows user code on window. Waits until user authorizes OAuth app,
        /// then on success closes window to open main app
        /// </summary>
        private async void GetPermissionGithub()
        {
            authorizationPanel.Visible = true;

            string? userCode = await Github.GivePermission(repoTypeButton.Checked ? "private" : "public", grantDeleteRepo.Checked);

            if (userCode != null)
            {
                userCodeLabel.Text = "****-****";
                userCodeLabel.Visible = true;
                await Github.WaitForAuthorization();
            }

            Hide();
        }

        /// <summary>
        /// Code checkbox clicked handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void ShowCodeCheckboxChanged(object sender, EventArgs e)
        {
            userCodeLabel.Text = (showCodeCheckBox.Checked) ? Github.UserCode : "****-****";
        }

        /// <summary>
        /// Opens an external website.
        /// </summary>
        /// <param name="siteURL">The url to open.</param>
        private static void OpenExternalWebsite(string siteURL)
        {
            Process.Start(new ProcessStartInfo { FileName = siteURL, UseShellExecute = true });
        }

        /// <summary>
        /// Loads main page routed from local repository button
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void LoadMainAppFormLocal(object? sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// Changes Github remember user auth bool based on result of remember me checkbox.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void RememberMeCheckboxChanged(object sender, EventArgs e)
        {
            bool toRemember = rememberMeCheckbox.Checked;
            GVSettings.Data.RememberGitHubLogin = toRemember;
            GVSettings.SaveSettings();
        }

        /// <summary>
        /// The clipboard button click handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void ClipboardButton_Click(object sender, EventArgs e)
        {
            string clipboardCode = string.Join("\r", Github.UserCode);
            Clipboard.SetText(clipboardCode);
        }

        /// <summary>
        /// The browser button click handler. Opens up the GitHub Shared Device cod website.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void BrowserButton_Click(object sender, EventArgs e)
        {
            OpenExternalWebsite(Github.API_DEVICE_LOGIN_CODE_URL);
        }
    }
}
