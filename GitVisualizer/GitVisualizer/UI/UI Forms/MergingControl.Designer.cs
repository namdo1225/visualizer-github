﻿namespace GitVisualizer.UI.UI_Forms
{
    partial class MergingControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mergingControlPanel = new Panel();
            titleLabel = new Label();
            mergingPanel = new Panel();
            diffPanel = new Panel();
            diffSplitContainer = new SplitContainer();
            diffFile1Group = new GroupBox();
            diffFile2Group = new GroupBox();
            diffControlPanel = new Panel();
            buttonSplitContainer = new SplitContainer();
            chooseLocalButton = new Button();
            chooseRemoteButton = new Button();
            gitCommandPanel = new Panel();
            mergingControlPanel.SuspendLayout();
            mergingPanel.SuspendLayout();
            diffPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)diffSplitContainer).BeginInit();
            diffSplitContainer.Panel1.SuspendLayout();
            diffSplitContainer.Panel2.SuspendLayout();
            diffSplitContainer.SuspendLayout();
            diffControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)buttonSplitContainer).BeginInit();
            buttonSplitContainer.Panel1.SuspendLayout();
            buttonSplitContainer.Panel2.SuspendLayout();
            buttonSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // mergingControlPanel
            // 
            mergingControlPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mergingControlPanel.BorderStyle = BorderStyle.FixedSingle;
            mergingControlPanel.Controls.Add(titleLabel);
            mergingControlPanel.Dock = DockStyle.Left;
            mergingControlPanel.Location = new Point(0, 0);
            mergingControlPanel.MinimumSize = new Size(280, 280);
            mergingControlPanel.Name = "mergingControlPanel";
            mergingControlPanel.Size = new Size(280, 675);
            mergingControlPanel.TabIndex = 4;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Margin = new Padding(0);
            titleLabel.MinimumSize = new Size(280, 50);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(280, 50);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Merging";
            // 
            // mergingPanel
            // 
            mergingPanel.Controls.Add(diffPanel);
            mergingPanel.Controls.Add(gitCommandPanel);
            mergingPanel.Dock = DockStyle.Fill;
            mergingPanel.Location = new Point(280, 0);
            mergingPanel.Name = "mergingPanel";
            mergingPanel.Size = new Size(895, 675);
            mergingPanel.TabIndex = 5;
            // 
            // diffPanel
            // 
            diffPanel.Controls.Add(diffSplitContainer);
            diffPanel.Controls.Add(diffControlPanel);
            diffPanel.Dock = DockStyle.Fill;
            diffPanel.Location = new Point(0, 0);
            diffPanel.Name = "diffPanel";
            diffPanel.Size = new Size(895, 575);
            diffPanel.TabIndex = 4;
            // 
            // diffSplitContainer
            // 
            diffSplitContainer.Dock = DockStyle.Fill;
            diffSplitContainer.Location = new Point(0, 50);
            diffSplitContainer.Margin = new Padding(8);
            diffSplitContainer.Name = "diffSplitContainer";
            // 
            // diffSplitContainer.Panel1
            // 
            diffSplitContainer.Panel1.Controls.Add(diffFile1Group);
            // 
            // diffSplitContainer.Panel2
            // 
            diffSplitContainer.Panel2.Controls.Add(diffFile2Group);
            diffSplitContainer.Size = new Size(895, 525);
            diffSplitContainer.SplitterDistance = 438;
            diffSplitContainer.SplitterWidth = 18;
            diffSplitContainer.TabIndex = 1;
            // 
            // diffFile1Group
            // 
            diffFile1Group.AutoSize = true;
            diffFile1Group.Dock = DockStyle.Fill;
            diffFile1Group.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            diffFile1Group.Location = new Point(0, 0);
            diffFile1Group.Margin = new Padding(6);
            diffFile1Group.Name = "diffFile1Group";
            diffFile1Group.Padding = new Padding(6);
            diffFile1Group.Size = new Size(438, 525);
            diffFile1Group.TabIndex = 0;
            diffFile1Group.TabStop = false;
            diffFile1Group.Text = "Diff_File1.txt";
            // 
            // diffFile2Group
            // 
            diffFile2Group.AutoSize = true;
            diffFile2Group.Dock = DockStyle.Fill;
            diffFile2Group.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            diffFile2Group.Location = new Point(0, 0);
            diffFile2Group.Name = "diffFile2Group";
            diffFile2Group.Size = new Size(439, 525);
            diffFile2Group.TabIndex = 1;
            diffFile2Group.TabStop = false;
            diffFile2Group.Text = "Diff_File2.txt";
            // 
            // diffControlPanel
            // 
            diffControlPanel.Controls.Add(buttonSplitContainer);
            diffControlPanel.Dock = DockStyle.Top;
            diffControlPanel.Location = new Point(0, 0);
            diffControlPanel.Name = "diffControlPanel";
            diffControlPanel.Size = new Size(895, 50);
            diffControlPanel.TabIndex = 0;
            // 
            // buttonSplitContainer
            // 
            buttonSplitContainer.Dock = DockStyle.Top;
            buttonSplitContainer.Location = new Point(0, 0);
            buttonSplitContainer.Margin = new Padding(8);
            buttonSplitContainer.Name = "buttonSplitContainer";
            // 
            // buttonSplitContainer.Panel1
            // 
            buttonSplitContainer.Panel1.Controls.Add(chooseLocalButton);
            // 
            // buttonSplitContainer.Panel2
            // 
            buttonSplitContainer.Panel2.Controls.Add(chooseRemoteButton);
            buttonSplitContainer.Size = new Size(895, 50);
            buttonSplitContainer.SplitterDistance = 438;
            buttonSplitContainer.SplitterWidth = 18;
            buttonSplitContainer.TabIndex = 2;
            // 
            // chooseLocalButton
            // 
            chooseLocalButton.Dock = DockStyle.Fill;
            chooseLocalButton.FlatStyle = FlatStyle.Flat;
            chooseLocalButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            chooseLocalButton.Location = new Point(0, 0);
            chooseLocalButton.Name = "chooseLocalButton";
            chooseLocalButton.Size = new Size(438, 50);
            chooseLocalButton.TabIndex = 1;
            chooseLocalButton.Text = "Choose All Changes From Your File";
            chooseLocalButton.UseVisualStyleBackColor = true;
            chooseLocalButton.Click += revokeAccessButton_Click;
            // 
            // chooseRemoteButton
            // 
            chooseRemoteButton.Dock = DockStyle.Fill;
            chooseRemoteButton.FlatStyle = FlatStyle.Flat;
            chooseRemoteButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            chooseRemoteButton.Location = new Point(0, 0);
            chooseRemoteButton.Name = "chooseRemoteButton";
            chooseRemoteButton.Size = new Size(439, 50);
            chooseRemoteButton.TabIndex = 1;
            chooseRemoteButton.Text = "Choose All Changes From Source File";
            chooseRemoteButton.UseVisualStyleBackColor = true;
            chooseRemoteButton.Click += revokeAccessButton_Click;
            // 
            // gitCommandPanel
            // 
            gitCommandPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            gitCommandPanel.Dock = DockStyle.Bottom;
            gitCommandPanel.Location = new Point(0, 575);
            gitCommandPanel.Name = "gitCommandPanel";
            gitCommandPanel.Size = new Size(895, 100);
            gitCommandPanel.TabIndex = 5;
            // 
            // MergingControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(mergingPanel);
            Controls.Add(mergingControlPanel);
            Margin = new Padding(1);
            Name = "MergingControl";
            Size = new Size(1175, 675);
            Load += MergingControl_Load;
            mergingControlPanel.ResumeLayout(false);
            mergingControlPanel.PerformLayout();
            mergingPanel.ResumeLayout(false);
            diffPanel.ResumeLayout(false);
            diffSplitContainer.Panel1.ResumeLayout(false);
            diffSplitContainer.Panel1.PerformLayout();
            diffSplitContainer.Panel2.ResumeLayout(false);
            diffSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)diffSplitContainer).EndInit();
            diffSplitContainer.ResumeLayout(false);
            diffControlPanel.ResumeLayout(false);
            buttonSplitContainer.Panel1.ResumeLayout(false);
            buttonSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)buttonSplitContainer).EndInit();
            buttonSplitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private void ApplyColorTheme(UITheme.AppTheme theme)
        {
            mergingControlPanel.BackColor = theme.PanelBackground;
            mergingControlPanel.ForeColor = theme.TextBright;


            gitCommandPanel.BackColor = theme.ElementBackground;
            diffFile1Group.ForeColor = theme.TextHeader;
            diffFile2Group.ForeColor = theme.TextHeader;

            chooseLocalButton.BackColor = theme.ElementBackground;
            chooseRemoteButton.BackColor = theme.ElementBackground;
            chooseLocalButton.ForeColor = theme.TextSelectable;
            chooseRemoteButton.ForeColor = theme.TextSelectable;
        }
        private Panel mergingControlPanel;
        private Label titleLabel;
        private Panel mergingPanel;
        private Panel diffPanel;
        private SplitContainer diffSplitContainer;
        private GroupBox diffFile1Group;
        private GroupBox diffFile2Group;
        private Panel gitCommandPanel;
        private Button chooseLocalButton;
        private Button chooseRemoteButton;
        private Panel diffControlPanel;
        private SplitContainer buttonSplitContainer;
    }
}
