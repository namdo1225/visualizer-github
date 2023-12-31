﻿using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace GitVisualizer.UI.UI_Forms
{

    /// <summary>
    /// The branches control UI.
    /// </summary>
    public partial class BranchesControl : UserControl
    {
        private const int pixelsPerBranchNode = 32;
        private const int pixelsPerBranchRow = 42;
        private const int branchNodeRadius = 12;
        private readonly List<Color> branchNodeColors =
            [Color.PowderBlue,
            Color.LightGreen,
            Color.DarkSalmon,
            Color.Gold,
            Color.Plum,
            Color.Aqua,
            Color.Bisque,
            Color.DarkOliveGreen,
            Color.Orange];

        private Tuple<List<Branch>, List<Commit>>? commitHistory = null;

        //private Tuple<List<Branch>, List<Commit>> commitHistory = null;
        Commit? selectedCommit = null;

        /// <summary>
        /// The constructor for BranchesControl.
        /// </summary>
        public BranchesControl()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
        }

        /// <summary>
        /// Called when this control is displayed again.
        /// </summary>
        public void OnBranchesControlFocus()
        {
            UpdateLiveReposAndBranch();
            UpdateGridView();
        }

        /// <summary>
        /// Updates live repos and branch.
        /// </summary>
        private void UpdateLiveReposAndBranch()
        {
            if (GitAPI.LiveRepository == null)
                return;

            activeRepositoryTextLabel.Text = GitAPI.LiveRepository.Title;
            activeRepositoryTextLabel.ForeColor = MainForm.AppTheme.TextBright;

            if (GitAPI.LiveBranch != null)
                checkedOutBranchTextLabel.Text = $"Branch: {GitAPI.LiveBranch.Title}";
            else if (GitAPI.LiveCommit != null)
                checkedOutBranchTextLabel.Text = $"Commit: {GitAPI.LiveCommit.ShortCommitHash}";
        }

        /// <summary>
        /// Updates grid view.
        /// </summary>
        private void UpdateGridView()
        {
            // Update view by getting branch info
            branchesGridView.Rows.Clear();
            commitHistory = GitAPI.Getters.GetCommitsAndBranches();

            Program.MainForm.UpdateAppTitle();

            foreach (Commit commit in commitHistory.Item2)
            {
                List<Branch> branches = commit.Branches;
                string branchList = string.Join(", ", branches);
                int index = branchesGridView.Rows.Add(null, branchList, commit.ShortCommitHash, commit.CommitterName, commit.CommitterDate, commit.Subject);
                branchesGridView.Rows[index].Cells[0].ToolTipText = commit.LongCommitHash;
            }

            branchComboBox.DataSource = commitHistory.Item1;
            if (commitHistory.Item1.Count > 0)
                checkoutBranchButton.Visible = deleteBranchButton.Visible = newBranchFromCommitTextBox.Visible =
                    createBranchFromCurrentButton.Visible = mergeButton.Visible = true;
        }

        /// <summary>
        /// Checkouts a branch.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnCheckoutToBranchButton(object sender, EventArgs e)
        {
            if (branchComboBox.Items.Count == 0)
                return;
            Branch selected = (Branch)branchComboBox.SelectedItem;
            GitAPI.Actions.LocalActions.CheckoutBranch(selected);

            if (GitAPI.LiveBranch == null)
                return;
            checkedOutBranchTextLabel.Text = $"Branch: {GitAPI.LiveBranch.Title}";
            branchesGridView.Refresh();
        }

        /// <summary>
        /// Deletes branch button.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnDeleteBranchButton(object sender, EventArgs e)
        {
            if (branchComboBox.Items.Count > 0) {
                Branch? selected = null;
                GitAPI.Actions.LocalActions.DeleteBranchLocal(selected);
            }
        }

        /// <summary>
        /// Creates branch from current branch.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnCreateBranchFromCurrentButton(object sender, EventArgs e)
        {
            string title = newBranchFromCommitTextBox.Text;
            if (title.Length == 0)
            {
                MainForm.OpenDialog("Branch name is empty!");
                return;
            }
            Branch branch = GitAPI.Actions.LocalActions.CreateLocalBranch(title, GitAPI.LiveCommit);
            GitAPI.Actions.RemoteActions.AddLocalBranchToRemote(branch);
        }

        /// <summary>
        /// Checkouts to selected commit.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        public void OnCheckoutToSelectedCommitButton(object sender, EventArgs e)
        {
            if (selectedCommit == null) { return; }
            checkedOutBranchTextLabel.Text = $"Commit: {selectedCommit.ShortCommitHash}";
            GitAPI.Actions.LocalActions.CheckoutCommit(selectedCommit);
            branchesGridView.Refresh();
        }

        /// <summary>
        /// Creates branch from selected commit.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        public void OnCreateBranchFromSelectedButton(object sender, EventArgs e)
        {
            if (selectedCommit == null) { return; }
            string title = newBranchFromCommitTextBox.Text;
            if (title.Length == 0) { return; }
            GitAPI.Actions.LocalActions.CreateLocalBranch(title, selectedCommit);
        }

        /// <summary>
        /// The handler for branches grid view cell content click.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The DataGridViewCellEventArgs.</param>
        private void BranchesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                selectedCommit = commitHistory.Item2[index];
                selectedCommitTextLabel.Text = $"Selected Commit: {selectedCommit.ShortCommitHash} - {selectedCommit.Subject}";
                checkoutCommitButton.Visible = newBranchFromCommitTextBox.Visible = createBranchFromSelectedButton.Visible =
                    undoCommitButton.Visible = true;
            }
        }

        /// <summary>
        /// Draws little circle nodes representing branches in the branch cell view on any cell draw event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BranchesGridViewDrawCell(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //// Ignore header row and any columns besides Graph column
            if (e.ColumnIndex > 0 || e.RowIndex == -1) { return; }
            int lineXOffset = 6;

            Commit commit = commitHistory.Item2[e.RowIndex];

            int depthOffset = commit.graphColIndex + 1;


            SmoothingMode prevSmoothing = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.PaintBackground(e.CellBounds, true);


            int colorOffset = depthOffset % branchNodeColors.Count;


            int xOffset = depthOffset * pixelsPerBranchNode;
            Pen pen = new(branchNodeColors[colorOffset], 3);

            // Node circles

            int x = e.CellBounds.X + (xOffset / 2);
            int y = e.CellBounds.Y + ((e.CellBounds.Height / 2) - (branchNodeRadius / 2));

            Point start = new(x, y);

            // Parents
            foreach (Tuple<int, int> targetCoord in commit.graphOutRowColPairs)
            {
                int xDiff = (targetCoord.Item2 - commit.graphColIndex) * (pixelsPerBranchNode);
                int yDiff = (targetCoord.Item1 - commit.graphRowIndex) * (pixelsPerBranchNode + 12);
                Pen linepen = new(branchNodeColors[(targetCoord.Item2 + 1) % branchNodeColors.Count], 3);
                e.Graphics.DrawLine(linepen, start.X + lineXOffset, start.Y, start.X + xDiff + lineXOffset, start.Y - yDiff);
            }
            //// Children
            //foreach (Tuple<int, int> targetCoord in commit.graphInRowColPairs)
            //{
            //    int xDiff = (commit.graphColIndex - targetCoord.Item2) * pixelsPerBranchNode;
            //    int yDiff = (commit.graphRowIndex - targetCoord.Item1) * pixelsPerBranchNode;
            //    e.Graphics.DrawLine(pen, start.X + lineXOffset, start.Y, start.X - xDiff + lineXOffset, start.Y + yDiff);
            //}



            e.Graphics.FillEllipse(pen.Brush, x, y, branchNodeRadius, branchNodeRadius);
            if (commit == GitAPI.LiveCommit)
            {
                pen.Color = Color.Black;
                e.Graphics.FillEllipse(pen.Brush, x + 3, y + 3, branchNodeRadius / 2, branchNodeRadius / 2);
            }

            e.Graphics.SmoothingMode = prevSmoothing;
            e.Handled = true;


            #region graphicOld
            //int curOffset = 1;
            //foreach (char symbol in curGraphLine)
            //{

            //    int colorOffset = curOffset % branchNodeColors.Count;
            //    Pen pen = new Pen(branchNodeColors[colorOffset], 3);
            //    int xOffset = curOffset * pixelsPerBranchNode;

            //    if (symbol == ' ') {
            //    }
            //    else if (symbol == '|')
            //    {
            //        int x = e.CellBounds.X + (xOffset/2);
            //        int offsetY = (cellHeight / 2);
            //        e.Graphics.DrawLine(pen, x + lineXOffset, e.CellBounds.Y + (offsetY * 2), x + lineXOffset, e.CellBounds.Y);

            //    }
            //    else if (symbol == '*')
            //    {
            //        int x = e.CellBounds.X + (xOffset / 2);
            //        int y = e.CellBounds.Y + ((e.CellBounds.Height / 2) - (branchNodeRadius / 2));

            //        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //        e.Graphics.FillEllipse(pen.Brush, x, y, branchNodeRadius, branchNodeRadius);
            //        if (commit == GitAPI.liveCommit)
            //        {
            //            pen.Color = Color.Black;
            //            e.Graphics.FillEllipse(pen.Brush, x + 3, y + 3, branchNodeRadius/2, branchNodeRadius/2);
            //        }
            //    }
            //    else if (symbol == '&')
            //    {
            //        break;
            //    }
            //    curOffset++;

            //}

            //if (curMergeLine != string.Empty)
            //{
            //    curOffset = 1;
            //    foreach (char symbol in curMergeLine)
            //    {
            //        int colorOffset = curOffset % branchNodeColors.Count;


            //        int xOffset = curOffset * pixelsPerBranchNode;
            //        if (symbol == '|')
            //        {
            //            Pen pen = new Pen(branchNodeColors[colorOffset], 3);
            //            int x = e.CellBounds.X + (xOffset / 2);
            //            int offsetY = (cellHeight / 2);
            //            e.Graphics.DrawLine(pen, x + lineXOffset, e.CellBounds.Y + offsetY, x + lineXOffset, e.CellBounds.Y);
            //        }
            //        else if (symbol == '/')
            //        {
            //            Pen pen = new Pen(branchNodeColors[colorOffset - 1], 3);
            //            int x = e.CellBounds.X + (xOffset / 2);
            //            int offsetY = (cellHeight / 2);
            //            e.Graphics.DrawLine(pen, x - lineXOffset, e.CellBounds.Y + offsetY, x + lineXOffset, e.CellBounds.Y);
            //        }
            //        else if (symbol == '\\')
            //        {
            //            Pen pen = new Pen(branchNodeColors[colorOffset - 1], 3);
            //            int x = e.CellBounds.X + (xOffset / 2);
            //            int offsetY = (cellHeight / 2);
            //            e.Graphics.DrawLine(pen, x + lineXOffset, e.CellBounds.Y + offsetY, x - lineXOffset, e.CellBounds.Y);
            //        }

            //        curOffset++;
            //    }
            //}




            /*
            // Get Branch index for offset
            int branchIndex = 1;
            

            // Get branch color from index
            Pen pen = new Pen(branchNodeColors[branchIndex], 3);

            // Set position based on cell bounds using branch offset and radius
            //int x = e.CellBounds.X + (xOffset / 2);
            int y = e.CellBounds.Y + ((e.CellBounds.Height / 2) - (branchNodeRadius / 2));
            // Paint background first, how the cell was going to do it anyways
            e.PaintBackground(e.CellBounds, true);
            // Save smoothing mode before changing, then change to antialias for smooth node drawing
            SmoothingMode prevSmoothing = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.DrawEllipse(pen, x, y, branchNodeRadius, branchNodeRadius);
            if (commit == GitAPI.liveCommit)
            {
                e.Graphics.FillEllipse(pen.Brush, x, y, branchNodeRadius, branchNodeRadius);
            }

            e.Graphics.SmoothingMode = prevSmoothing;
            // Handle event and return to continue drawing
            */
            #endregion graphicOld

        }

        /// <summary>
        /// The undo commit button click handler. Undo the last commit for a local repository.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void UndoCommitButton_Click(object sender, EventArgs e)
        {
            GitAPI.Actions.LocalActions.UndoLastCommit();
            OnBranchesControlFocus();
        }

        /// <summary>
        /// The merge button click handler. Merge currently selected branch to another branch.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void MergeButton_Click(object sender, EventArgs e)
        {
            string? branch = branchComboBox.SelectedItem.ToString();
            string? currentBranch = GitAPI.LiveBranch.Title;
            if (currentBranch != null && branch != null && currentBranch != branch)
            {
                GitAPI.Actions.LocalActions.Merge(branch);
                OnBranchesControlFocus();
            }
            else if (currentBranch == null || branch == null)
                MainForm.OpenDialog("No branch is currently selected.");
            else
                MainForm.OpenDialog("You cannot merge a branch with itself.");
        }
    }
}
