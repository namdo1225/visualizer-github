﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitVisualizer.UI.UI_Forms
{
    public partial class MergingControl : UserControl
    {
        private List<Tuple<string, string>> stagedChanges = new List<Tuple<string, string>>();
        private List<Tuple<string, string>> unstagedChanges = new List<Tuple<string, string>>();

        public MergingControl()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
            commitMessageTextBox.PlaceholderText = "Enter commit message...";
        }

        public void OnMergingControlFocus()
        {
            UpdateGridViews();
        }

        private void UpdateGridViews()
        {
            stagedChanges = GitAPI.Getters.getStagedFiles();
            unstagedChanges = GitAPI.Getters.getUnStagedFiles();
            stagedChangesDataGridView.Rows.Clear();
            unstagedChangesDataGridView.Rows.Clear();
            foreach (Tuple<string, string> change in stagedChanges)
            {
                string filename = Path.GetFileName(change.Item2) + " (" + change.Item2 + ")";
                int index = stagedChangesDataGridView.Rows.Add(filename, "-");
                stagedChangesDataGridView.Rows[index].Cells[0].ToolTipText = change.Item2;
            }
            foreach (Tuple<string, string> change in unstagedChanges)
            {
                string filename = Path.GetFileName(change.Item2) + " (" + change.Item2 + ")";
                int index = unstagedChangesDataGridView.Rows.Add(filename, "+", "<-");
                unstagedChangesDataGridView.Rows[index].Cells[0].ToolTipText = change.Item2;
            }

        }
        private void MergingControl_Load(object sender, EventArgs e)
        {

        }

        private void stagedChangesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != 1) { return; }
            Debug.WriteLine("UNSTAGING " + stagedChanges[e.RowIndex].Item2);
            GitAPI.Actions.LocalActions.unStageChange(stagedChanges[e.RowIndex].Item2);
            UpdateGridViews();
        }

        private void unstagedChangesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 1) { return; }
            if (e.ColumnIndex == 1)
            {
                GitAPI.Actions.LocalActions.stageChange(unstagedChanges[e.RowIndex].Item2);
                UpdateGridViews();
            }
            else if (e.ColumnIndex == 2)
            {

            }
        }
    }
}
