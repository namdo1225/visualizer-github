using System.Diagnostics;

using GithubSpace;
using GitVisualizer.backend;

namespace GitVisualizer.UI.UI_Forms
{
    /// <summary>
    /// Class for repositories control UI.
    /// </summary>
    public partial class RepositoriesControl : UserControl
    {
        private Tuple<RepositoryLocal?, RepositoryRemote?> selectedRepo = new(null, null);
        private List<Tuple<RepositoryLocal?, RepositoryRemote?>> allRepos = new();

        /// <summary>
        /// The RepositoriesControl constructor.
        /// </summary>
        public RepositoriesControl()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
        }


        /// <summary>
        /// Use Github API to request remote repositories then add results to repos data grid table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EnterControl()
        {
            GitAPI.Scanning.ScanForAllRepos(UpdateGridCallback);
        }

        /// <summary>
        /// The update grid callback handler.
        /// </summary>
        public void UpdateGridCallback()
        {
            Invoke(AddReposToTable);
        }

        /// <summary>
        /// Main thread function to populate data grid cells with APi result repos
        /// </summary>
        public void AddReposToTable()
        {
            Program.MainForm.UpdateAppTitle();

            allRepos = GitAPI.Getters.GetAllRepositories();

            if (GitAPI.LiveRepository != null)
            {
                activeRepositoryTextLabel.Text = GitAPI.LiveRepository.Title;
                activeRepositoryTextLabel.ForeColor = MainForm.AppTheme.TextBright;
            }

            repositoriesGridView.Columns.Clear();
            repositoriesGridView.DataSource = allRepos;
            repositoriesGridView.Columns[0].HeaderCell.Value = "Local Repositories";
            repositoriesGridView.Columns[1].HeaderCell.Value = "Remote Repositories";

            localRepoComboBox.DataSource = GitAPI.Actions.LocalActions.GetTrackedDirs();
        }

        /// <summary>
        /// The repositories grid view_ cell content click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void RepositoriesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            selectedRepo = (Tuple<RepositoryLocal?, RepositoryRemote?>)repositoriesGridView.Rows[e.RowIndex].DataBoundItem;
            if (selectedRepo == null)
                return;

            // If local exists
            if (selectedRepo.Item1 != null)
            {
                localRepoButtonsLabel.Text = $"Local: {selectedRepo.Item1.Title}";
                cloneToLocalButton.Visible = false;
                setAsActiveRepoButton.Visible = openInFileExplorerButton.Visible = deleteLocalRepoButton.Visible = true;
            }
            else
            {
                localRepoButtonsLabel.Text = "No Local Repo, Need to Clone First!";
                cloneToLocalButton.Visible = true;
                setAsActiveRepoButton.Visible = openInFileExplorerButton.Visible = deleteLocalRepoButton.Visible = false;
            }

            // If remote exists
            if (selectedRepo.Item2 != null)
            {
                remoteRepoButtonsLabel.Text = $"Remote: {selectedRepo.Item2.Title}";
                createNewRemoteRepoButton.Visible = false;
                openOnGithubComButton.Visible = true;
                deleteRemoteRepoButton.Visible = Github.DeleteRepoPermission;
            }
            else
            {
                remoteRepoButtonsLabel.Text = "No Remote for This Local. Create New Remote Repo First.";
                createNewRemoteRepoButton.Visible = true;
                openOnGithubComButton.Visible = deleteRemoteRepoButton.Visible = false;
            }

        }

        /// <summary>
        /// The create new local repo button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnCreateNewLocalRepoButton(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.CreateLocalRepository(UpdateGridCallback);
            GitAPI.Scanning.ScanForAllRepos(UpdateGridCallback);
        }

        /// <summary>
        /// The open in file explorer button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnOpenInFileExplorerButton(object sender, EventArgs e)
        {
            if (selectedRepo != null && selectedRepo.Item1 != null)
                Process.Start("explorer.exe", selectedRepo.Item1.DirPath);
        }

        /// <summary>
        /// The track existing repos button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnTrackExistingReposButton(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.UserSelectTrackDirectory(true, UpdateGridCallback);
        }

        /// <summary>
        /// The clone to local button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnCloneToLocalButton(object sender, EventArgs e)
        {
            if (selectedRepo != null && selectedRepo.Item2 != null)
                GitAPI.Actions.RemoteActions.CloneRemoteRepository(selectedRepo.Item2, UpdateGridCallback);
        }

        /// <summary>
        /// The set as active repo button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnSetAsActiveRepoButton(object sender, EventArgs e)
        {
            if (selectedRepo == null || selectedRepo.Item1 == null)
                return;

            GitAPI.Actions.LocalActions.SetLiveRepository(selectedRepo.Item1);
            activeRepositoryTextLabel.Text = selectedRepo.Item1.Title;
            activeRepositoryTextLabel.ForeColor = MainForm.AppTheme.TextBright;
            Program.MainForm.UpdateAppTitle();
        }

        /// <summary>
        /// The open on github button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnOpenOnGithubComButton(object sender, EventArgs e)
        {
            if (selectedRepo != null && selectedRepo.Item2 != null)
                Process.Start(new ProcessStartInfo { FileName = selectedRepo.Item2.WebURL, UseShellExecute = true });
        }

        /// <summary>
        /// The create new remote repo button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnCreateNewRemoteRepoButton(object sender, EventArgs e)
        {
            GitAPI.Actions.RemoteActions.CreateRemoteRepository(selectedRepo.Item1, UpdateGridCallback);
        }

        /// <summary>
        /// The untrack repos button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnUntrackReposButton(object sender, EventArgs e)
        {
            if (localRepoComboBox.SelectedItem is LocalTrackedDir tracked)
                GitAPI.Actions.LocalActions.UntrackDirectory(tracked, UpdateGridCallback);
        }

        /// <summary>
        /// The rescan button click handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void RescanButton_Click(object sender, EventArgs e)
        {
            GitAPI.Scanning.ScanForAllRepos(UpdateGridCallback);
        }

        /// <summary>
        /// The repositories control panel paint handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The PaintEventArgs.</param>
        private void RepositoriesControlPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// The delete local repo button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void DeleteLocalRepo_Click(object sender, EventArgs e)
        {
            bool result = GitAPI.Actions.LocalActions.DeleteRepo(selectedRepo.Item1);
            if (result)
            {
                GitAPI.Actions.LocalActions.UntrackDirectory(null, UpdateGridCallback, selectedRepo.Item1.DirPath);
                localRepoComboBox.DataSource = GitAPI.Actions.LocalActions.GetTrackedDirs();
                GitAPI.Scanning.ScanForAllRepos(UpdateGridCallback);
            }
        }

        /// <summary>
        /// The delete remote repo button handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private async void DeleteRemoteRepo_Click(object sender, EventArgs e)
        {
            bool result = await Github.DeleteRemoteRepo(selectedRepo.Item2.WebURL);
            if (result)
                GitAPI.Scanning.ScanForAllRepos(UpdateGridCallback);
        }
    }
}
