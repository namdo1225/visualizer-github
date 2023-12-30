namespace GitVisualizer;

/// <summary>
/// Class that is used to store data about a Git branch.
/// </summary>
public class Branch(string title, Commit commit)
{
    /// <summary>
    /// Gets or Sets for the branch's name.
    /// </summary>
    public string Title { get; private set; } = title;

    /// <summary>
    /// Gets or Sets for the branch's current commit.
    /// </summary>
    public Commit Commit { get; set; } = commit;

    /// <summary>
    /// The string conversion is the branch's name.
    /// </summary>
    /// <returns>The branch's name.</returns>
    public override string ToString()
    {
        return Title;
    }
}
