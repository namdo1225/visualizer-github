using GitVisualizer.UI;
using GitVisualizer.UI.UI_Forms;
using System.Diagnostics;

using GithubSpace;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using GitVisualizer.backend.git;
using GitVisualizer.backend;

namespace GitVisualizer
{
    /// <summary>
    /// Code for the Settings Workspace Form Window, including event handlers, color theme settings, and component initialization
    /// </summary>
    public partial class SettingsForm : Form
    {
        private readonly string AUTH_WEBSITE = "https://github.com/settings/applications";

        /// <summary>
        /// The SettingsForm constructor
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
            themeCombo.SelectedItem = GVSettings.Data.Theme ?? "Dark";
        }

        /// <summary>
        /// Opens Nam's website.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void NamWebsite_Click(object sender, EventArgs e)
        {
            MainForm.OpenExternalWebsite(MainForm.NAM_WEBSITE);
        }

        /// <summary>
        /// Opens the app repo's website.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void AppRepoButton_Click(object sender, EventArgs e)
        {
            MainForm.OpenExternalWebsite(MainForm.REPO_WEBSITE);
        }

        /// <summary>
        /// Opens Credential Manager
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void CredManagerButton_Click(object sender, EventArgs e)
        {
            string com = "control.exe keymgr.dll";
            Shell.Exec(com);
        }

        /// <summary>
        /// The theme combo change handler. Changes the app's theme as appropriate.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void ThemeCombo_Change(object sender, EventArgs e)
        {
            string theme = themeCombo.SelectedItem.ToString();
            switch (theme)
            {
                case "Dark":
                    GVSettings.Data.Theme = "Dark";
                    MainForm.AppTheme = UITheme.DarkTheme;
                    break;
                case "Blue Light":
                    GVSettings.Data.Theme = "Blue Light";
                    MainForm.AppTheme = UITheme.BlueThemeLight;
                    break;
                case "Blue Dark":
                    GVSettings.Data.Theme = "Blue Dark";
                    MainForm.AppTheme = UITheme.BlueThemeDark;
                    break;
            }
            GVSettings.SaveSettings();
            ApplyColorTheme(MainForm.AppTheme);
        }

        /// <summary>
        /// Opens the authorized OAuth app list on GitHub.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void AuthButton_Click(object sender, EventArgs e)
        {
            MainForm.OpenExternalWebsite(AUTH_WEBSITE);
        }

        /// <summary>
        /// The delete cred button click handler. Deletes stored credentials.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void DeleteCredButton_Click(object sender, EventArgs e)
        {
            Github.DeleteStoredCredential();
        }

        /// <summary>
        /// Retrieves credentials.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RetrieveCredButton_Click(object sender, EventArgs e)
        {
            if (GVSettings.Data.RememberGitHubLogin)
                Github.LoadStoredCredentials();
        }

        /// <summary>
        /// Stores credentials.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void StoreCredButton_Click(object sender, EventArgs e)
        {
            if (GVSettings.Data.RememberGitHubLogin)
                Github.SaveUser();
        }

        /// <summary>
        /// Updates the checkbox with the config file's configuration.
        /// </summary>
        public void UpdateRememberButton()
        {
            rememberMeCheckbox.Checked = GVSettings.Data.RememberGitHubLogin;
        }

        /// <summary>
        /// Detects remember me checkbox's changes and saves to config file.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void RemMeCheck_Change(object sender, EventArgs e)
        {
            bool toRemember = rememberMeCheckbox.Checked;
            GVSettings.Data.RememberGitHubLogin = toRemember;
            GVSettings.SaveSettings();
        }

        /// <summary>
        /// The config button click handler. Opens the config file.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void ConfigButton_Click(object sender, EventArgs e)
        {
            string com = $"start {GVSettings.FULL_FPATH}";
            Shell.Exec(com);
        }
    }
}
