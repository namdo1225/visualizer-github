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
    /// Code for the Error Workspace Form Window, including event handlers, color theme settings, and component initialization
    /// </summary>
    public partial class ErrorForm : Form
    {
        /// <summary>
        /// The Error constructor
        /// </summary>
        public ErrorForm()
        {
            InitializeComponent();
            ApplyColorTheme(MainForm.AppTheme);
        }

        /// <summary>
        /// Opens the repository page on new website page.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void RepoPageButton_Click(object sender, EventArgs e)
        {
            MainForm.OpenExternalWebsite(MainForm.REPO_WEBSITE);
        }

        /// <summary>
        /// Changes the dialog's labels
        /// </summary>
        /// <param name="message">The message for the dialog.</param>
        /// <param name="command">The command for the dialog.</param>
        public void ChangeLabel(string message, string command)
        {
            messageInsert.Text = message;
            commandInsert.Text = command;
        }

        /// <summary>
        /// Copies command text to clipboard.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void CopyCommandButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(commandInsert.Text);
        }

        /// <summary>
        /// Copies mesage text to clipboard.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The EventArgs.</param>
        private void CopyMessageButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(messageInsert.Text);
        }
    }
}
