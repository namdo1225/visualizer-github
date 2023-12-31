using GitVisualizer.UI.UI_Forms;
using SkiaSharp;
using System.Diagnostics;
using GithubSpace;
using GitVisualizer.backend;

namespace GitVisualizer
{

    /// <summary>
    /// The program class to start the main program.
    /// </summary>
    internal static class Program
    {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        ///
        private static readonly Github _github = new();
        private static readonly MainForm _mainForm = new();

        /// <summary>
        /// Github API instance used for handling Github.com requests and communications with Github server.
        /// </summary>
        public static Github Github => _github;

        /// <summary>
        /// Instance for Main WindowsForm Form, where the main app window exists
        /// </summary>
        public static MainForm MainForm => _mainForm;

        /// <summary>
        /// The main method used to start the program.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(_mainForm);
            if (!GVSettings.Data.RememberGitHubLogin)
                Github.DeleteToken();
        }
    }
}
