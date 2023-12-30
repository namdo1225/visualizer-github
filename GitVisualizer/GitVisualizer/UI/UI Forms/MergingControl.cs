using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitVisualizer.UI.UI_Forms
{

    /// <summary>
    /// The merging control UI class.
    /// </summary>
    public partial class MergingControl : UserControl
    {
        private List<Tuple<string, string>> stagedChanges = new();
        private List<Tuple<string, string>> unstagedChanges = new();
        private List<Tuple<string, string>> diffResults = new();

        /// <summary>
        /// The MergingControl constructor.
        /// </summary>
        public MergingControl()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
        }

        /// <summary>
        /// The merging control focus handler.
        /// </summary>
        public void OnMergingControlFocus()
        {
            UpdateGridViews();
            UpdateLiveReposAndBranch();
        }

        /// <summary>
        /// Updates live repos and branch.
        /// </summary>
        private void UpdateLiveReposAndBranch()
        {
            if (GitAPI.LiveRepository == null)
                return;

            Program.MainForm.UpdateAppTitle();
            activeRepositoryTextLabel.Text = GitAPI.LiveRepository.Title;
            activeRepositoryTextLabel.ForeColor = MainForm.AppTheme.TextBright;

            if (GitAPI.LiveBranch != null)
                checkedOutBranchTextLabel.Text = "Branch: " + GitAPI.LiveBranch.Title;
            else if (GitAPI.LiveCommit != null)
                checkedOutBranchTextLabel.Text = "Commit: " + GitAPI.LiveCommit.ShortCommitHash;

        }

        /// <summary>
        /// Updates grid views.
        /// </summary>
        private void UpdateGridViews()
        {
            stagedChanges = GitAPI.Getters.GetStagedFiles();
            unstagedChanges = GitAPI.Getters.GetUnStagedFiles();
            stagedChangesDataGridView.Rows.Clear();
            unstagedChangesDataGridView.Rows.Clear();
            foreach (Tuple<string, string> change in stagedChanges)
            {
                string filename = Path.GetFileName(change.Item2) + " (" + change.Item2 + ")";
                int index = stagedChangesDataGridView.Rows.Add(filename, "-");
                stagedChangesDataGridView.Rows[index].Cells[0].ToolTipText = change.Item2;
                stagedChangesDataGridView.Rows[index].Cells[1].Style.ForeColor = MainForm.AppTheme.TextSelectable;
                stagedChangesDataGridView.Rows[index].Cells[1].Style.BackColor = MainForm.AppTheme.ElementBackground;
            }
            foreach (Tuple<string, string> change in unstagedChanges)
            {
                string filename = Path.GetFileName(change.Item2) + " (" + change.Item2 + ")";
                int index = unstagedChangesDataGridView.Rows.Add(filename, "+", "<-");
                unstagedChangesDataGridView.Rows[index].Cells[0].ToolTipText = change.Item2;
            }

            if (GitAPI.CommitsBehind != null)
            {
                incomingCountTextLabel.Text = GitAPI.CommitsBehind.ToString();
            }
            if (GitAPI.CommitsAhead != null)
            {

                outgoingCountTextLabel.Text = GitAPI.CommitsAhead.ToString();
            }

        }

        private void MergingControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Unstages a cell content
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void StagedChangesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1)
                return;
            GitAPI.Actions.LocalActions.UnStageChange(stagedChanges[e.RowIndex].Item2);
            UpdateGridViews();
        }

        /// <summary>
        /// Stages changes in the data grid view.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void UnstagedChangesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1)
                return;
            if (e.ColumnIndex == 1)
            {
                GitAPI.Actions.LocalActions.StageChange(unstagedChanges[e.RowIndex].Item2);
                UpdateGridViews();
            }
            else if (e.ColumnIndex == 2)
            {
                GitAPI.Actions.LocalActions.RevertUnstagedChange(unstagedChanges[e.RowIndex].Item2);
                UpdateGridViews();
            }
        }

        /// <summary>
        /// Checks whether commit messages are entered to enable commit changes butotn.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void CommitMessageTextBox_TextChanged(object sender, EventArgs e)
        {
            commitChangesButton.Enabled = (commitMessageTextBox.Text.Length != 0);
        }

        /// <summary>
        /// Unstages all changes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnUnstageAllButton(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.UnstageAllStagedChanges();
            UpdateGridViews();
        }

        /// <summary>
        /// Stages all changes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnStageAllButton(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.StageAllUnstagedChanges();
            UpdateGridViews();

        }

        /// <summary>
        /// Reverts all changes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnRevertAllButton(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.RevertAllUnstagedChanges();
            UpdateGridViews();
        }

        /// <summary>
        /// Commits changes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnCommitChangesButton(object sender, EventArgs e)
        {
            string message = commitMessageTextBox.Text;
            GitAPI.Actions.LocalActions.CommitStagedChanges(message);
            commitMessageTextBox.Text = string.Empty;
            UpdateGridViews();
        }

        /// <summary>
        /// Syncs local and remote repositories changes.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void OnSyncButton(object sender, EventArgs e)
        {
            GitAPI.Actions.RemoteActions.Sync();
            UpdateGridViews();
        }

        /// <summary>
        /// Updates diff grid view.
        /// </summary>
        private void UpdateDiffGridView()
        {
            diffGridView.Rows.Clear();
            foreach (Tuple<string, string> diff in diffResults)
            {
                int rowIndex = diffGridView.Rows.Add(diff.Item1, diff.Item2);
                if (diff.Item1.StartsWith("@"))
                {
                    diffGridView.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Aquamarine;
                }

                if (diff.Item1.StartsWith("+"))
                {
                    diffGridView.Rows[rowIndex].Cells[0].Style.BackColor = Color.DarkOliveGreen;
                }
                if (diff.Item1.StartsWith("-"))
                {
                    diffGridView.Rows[rowIndex].Cells[0].Style.BackColor = Color.DarkRed;
                }


                if (diff.Item2.StartsWith("+"))
                {
                    diffGridView.Rows[rowIndex].Cells[1].Style.BackColor = Color.DarkOliveGreen;
                }
                if (diff.Item2.StartsWith("-"))
                {
                    diffGridView.Rows[rowIndex].Cells[1].Style.BackColor = Color.DarkRed;
                }

            }
        }

        /// <summary>
        /// The unstaged file cell selected handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void OnUnstagedFileCellSelected(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex != 0) { return; }
            string filePath = unstagedChanges[e.RowIndex].Item2;

            diffResults = GitAPI.Getters.GetFileDiff(filePath, false);
            UpdateDiffGridView();
        }

        /// <summary>
        /// The staged file cell selected handler.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void OnStagedFileCellSelected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 0)
                return;
            string filePath = stagedChanges[e.RowIndex].Item2;

            diffResults = GitAPI.Getters.GetFileDiff(filePath, true);
            UpdateDiffGridView();
        }

        /// <summary>
        /// The refresh button click handler. Refreshes merging control focus.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            OnMergingControlFocus();
        }
    }
}
