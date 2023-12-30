using GitVisualizer.backend.git;
using System.Diagnostics;

namespace GitVisualizer;

/// <summary>
/// The class to keep track of a local repository.
/// </summary>
public class RepositoryLocal(string title, string dirPath) : Repository(title)
{
    /// <summary>
    /// Gets or sets the repository directory path.
    /// </summary>
    public string DirPath { get; private set; } = dirPath;

    /// <summary>
    /// Returns the directory path.
    /// </summary>
    /// <returns>The directory path.</returns>
    public override string ToString()
    {
        return DirPath;
    }

    /// <summary>
    /// Gets the remote url.
    /// </summary>
    /// <returns>The remote url.</returns>
    public string? GetRemoteURL()
    {
        string? res = null;
        // TODO check that .git folder and repo exist
        string com = $"cd '{DirPath}'; git config --get remote.origin.url";
        ShellComRes result = Shell.Exec(com);

        if (result.Success && result.PsObjects != null && result.PsObjects.Count > 0 && result.PsObjects[0] != null)
        {
            res = result.PsObjects[0].ToString();
            if (res.StartsWith("https://github.com/"))
            {
                // https 
                if (!res.EndsWith(".git"))
                    res += ".git";
                // stripping "https://"
                res = res[8..];
            }
            else if (res.StartsWith("git@github.com:"))
            {
                // ssh
                if (!res.EndsWith(".git"))
                    res += ".git";
                // stripping "https://"
                res = "github.com/" + res[15..];
            }
        }

        return res;
    }
}
