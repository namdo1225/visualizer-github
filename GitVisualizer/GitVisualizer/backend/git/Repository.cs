namespace GitVisualizer;

/// <summary>
/// The parent class to store data of a repository.
/// </summary>
public abstract class Repository(string title)
{

    /// <summary>
    /// Name of the repository.
    /// </summary>
    public string Title { get; private set; } = FormatTitle(title);

    /// <summary>
    /// Formats repository name.
    /// </summary>
    /// <param name="title">The name of the repository.</param>
    /// <returns>The new repository name.</returns>
    public static string FormatTitle(string title)
    {
        return title.Replace(" ", "-");
    }

    /// <summary>
    /// Returns the name of the repository.
    /// </summary>
    /// <returns>The name of the repository.</returns>
    public override string ToString()
    {
        return Title;
    }
    
}
