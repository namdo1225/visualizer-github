using System.Diagnostics;

using GithubSpace;
using GitVisualizer.backend;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GitVisualizer.UI.UI_Forms
{

    /// <summary>
    /// The main form UI.
    /// </summary>
    public partial class MainForm : Form
    {
        public static UITheme.AppTheme AppTheme = UITheme.DarkTheme;

        private readonly RepositoriesControl repositoriesControl = new();
        private readonly BranchesControl branchesControl = new();
        private readonly MergingControl mergingControl = new();
        private readonly SettingsForm settingsForm = new();

        /// <summary>
        /// The constructor for MainForm.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            CheckValidation();
            rememberMeCheckbox.Checked = GVSettings.Data.RememberGitHubLogin;
            switch (GVSettings.Data.Theme)
            {
                case "Dark":
                    AppTheme = UITheme.DarkTheme;
                    break;
                case "Blue Light":
                    AppTheme = UITheme.BlueThemeLight;
                    break;
                case "Blue Dark":
                    AppTheme = UITheme.BlueThemeDark;
                    break;
            }
            ApplyColorTheme(AppTheme);
        }

        /// <summary>
        /// Checks if user has already logged in with this device, and show login page if not
        /// </summary>
        private void CheckValidation()
        {
            ShowControlInMainPanel(repositoriesControl);

            bool credRetrieved = Github.LoadStoredCredentials();
            bool canUseGitHub = false;
            if (credRetrieved)
            {
                Task task = Task.Run(Github.GetUserInfo);
                task.Wait();

                canUseGitHub = Github.Username != null;
                if (!canUseGitHub)
                    Github.DeleteStoredCredential();
            }

            if (!credRetrieved || !canUseGitHub)
            {
                SetupForm setup = new();
                setup.ShowDialog();
            }

            if (Github.AccessToken != null)
                loginButton.Text = Github.Username;

            if (Github.Username == null)
            {
                Task task = Task.Run(Github.GetUserInfo);
                task.Wait();
            }

            loginButton.Text = Github.Username ?? "Login";
            if (GVSettings.Data.RememberGitHubLogin && Github.AccessToken != null && Github.Username != null)
                Github.SaveUser();

            repositoriesControl.EnterControl();
        }

        /// <summary>
        /// The repositories button press handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnRepositoriesButtonPress(object sender, EventArgs e)
        {
            ShowControlInMainPanel(repositoriesControl);
        }

        /// <summary>
        /// The branches button press handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnBranchesButtonPress(object sender, EventArgs e)
        {
            branchesControl.OnBranchesControlFocus();
            ShowControlInMainPanel(branchesControl);
        }

        /// <summary>
        /// The merging button press handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnMergingButtonPress(object sender, EventArgs e)
        {
            mergingControl.OnMergingControlFocus();
            ShowControlInMainPanel(mergingControl);
        }

        /// <summary>
        /// Updates app title.
        /// </summary>
        public void UpdateAppTitle()
        {
            RepositoryLocal liveRepo = GitAPI.LiveRepository;
            if (liveRepo != null)
                TopLevelControl.Text = $"GitVisualizer - {liveRepo.Title} ({liveRepo.DirPath})";
        }

        /// <summary>
        /// Clears current main panel control and swaps it for given User Control view
        /// </summary>
        /// <param name="control"></param>
        private void ShowControlInMainPanel(UserControl control)
        {
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// The main form closing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void MainForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (GVSettings.Data.RememberGitHubLogin)
            {
                Github.SaveUser();
            }
            else if (Github.AccessToken != null)
            {
                Github.DeleteToken();
            }
        }

        /// <summary>
        /// Changes Github remember user auth bool based on result of remember me checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RememberMeCheckboxChanged(object sender, EventArgs e)
        {
            bool toRemember = rememberMeCheckbox.Checked;
            GVSettings.Data.RememberGitHubLogin = toRemember;
            GVSettings.SaveSettings();
        }

        /// <summary>
        /// The login button click handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (Github.Username == null)
            {
                SetupForm setup = new();
                setup.ShowDialog();

                if (Github.AccessToken != null)
                {
                    Github.GetUserInfo();
                    loginButton.Text = Github.Username;
                }
            }
        }

        /// <summary>
        /// The login button mouse enter handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void LoginButton_MouseEnter(object sender, EventArgs e)
        {
            if (Github.Username != null)
                loginButton.Text = "Already Logged In";
        }

        /// <summary>
        /// The login button mouse leave handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void LoginButton_MouseLeave(object sender, EventArgs e)
        {
            loginButton.Text = Github.Username ?? "Login";
        }

        /// <summary>
        /// The revoke access button click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RevokeAccessButton_Click(object sender, EventArgs e)
        {
            if (Github.AccessToken != null)
            {
                Github.DeleteToken();
                GitAPI.Actions.RemoteActions.UntrackRemoteRepos(repositoriesControl.UpdateGridCallback);
            }
            else
                loginButton.Text = "Login";
        }

        /// <summary>
        /// The settings button click handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void SettingsButton_Click(object sender, EventArgs e)
        {
            settingsForm.UpdateRememberButton();
            settingsForm.ShowDialog();
        }

        /// <summary>
        /// The main form activated handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            ApplyColorTheme(AppTheme);
            branchesControl.ApplyColorTheme(AppTheme);
            mergingControl.ApplyColorTheme(AppTheme);
            repositoriesControl.ApplyColorTheme(AppTheme);
        }

        /// <summary>
        /// Opens a dialog.
        /// </summary>
        /// <param name="message">The message for the dialog.</param>
        public static void OpenDialog(String message)
        {
            Form dialog = new();
            Label text = new()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Text = $"Error message: {message}"
            };
            dialog.Controls.Add(text);
            dialog.ShowDialog();
        }
    }
}