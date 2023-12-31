using System.Management.Automation;
using System.Collections.ObjectModel;
using GitVisualizer.UI.UI_Forms;
using System.Diagnostics;

namespace GitVisualizer.backend.git
{
    /// <summary>
    /// The class providing PowerShell functionalities.
    /// </summary>
    public static class Shell
    {
        /// <summary> persistent shell instance for running commands </summary>
        private static readonly PowerShell iShell;

        /// <summary>
        /// The Shell constructor.
        /// </summary>
        static Shell()
        {
            iShell = PowerShell.Create();
        }

        /// <summary>
        /// Synchronously executes the given command returns the result struct
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The result of the command.</returns>
        public static ShellComRes Exec(string command)
        {
            try
            {
                iShell.Commands.Clear();
                iShell.AddScript(command);
                Collection<PSObject> psObjects = iShell.Invoke();
                bool success = !iShell.HadErrors;
                string? errmsg = null;
                if (!success && iShell.Streams.Error.Count > 0)
                {
                    ErrorRecord err = iShell.Streams.Error[0];
                    errmsg = err.ToString();
                    iShell.Streams.Error.Clear();
                    MainForm.OpenDialog(errmsg, command);
                }
                return new ShellComRes(success, errmsg, psObjects);
            }
            catch (Exception e)
            {
                string errmsg = e.ToString();
                MainForm.OpenDialog(errmsg, command);
                return new ShellComRes(false, errmsg, null);
            }
        }
    }

    /// <summary> Class used to represent the results of a shell command</summary>
    public class ShellComRes(bool success, string? errmsg, Collection<PSObject>? psObjects)
    {
        /// <summary>
        /// Whether the command execution is successful.
        /// </summary>
        public bool Success { get; private set; } = success;

        /// <summary>
        /// Gets or Sets the error message of the command.
        /// </summary>
        public string? Errmsg { get; private set; } = errmsg;

        /// <summary>
        /// Gets or Sets the Powershell objects.
        /// </summary>
        public Collection<PSObject>? PsObjects { get; private set; } = psObjects;
    }
}