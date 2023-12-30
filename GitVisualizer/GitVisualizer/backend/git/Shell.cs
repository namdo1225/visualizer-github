using System.Management.Automation;
using System.Collections.ObjectModel;
using GitVisualizer.UI.UI_Forms;

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
            // TODO async shell command queue
            try
            {
                iShell.Commands.Clear();
                iShell.AddScript(command);
                Collection<PSObject> psObjects = iShell.Invoke();
                bool success = !iShell.HadErrors;
                string? errmsg = null;
                if (!success)
                {
                    ErrorRecord err = iShell.Streams.Error[0];
                    errmsg = err.ToString();
                    iShell.Streams.Error.Clear();
                    MainForm.OpenDialog(errmsg);
                }
                return new ShellComRes(success, errmsg: errmsg, psObjects: psObjects);
            }
            catch (Exception e)
            {
                string errmsg = e.ToString();
                return new ShellComRes(success: false, errmsg: errmsg, psObjects: null);
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