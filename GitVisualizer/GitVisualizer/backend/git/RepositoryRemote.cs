namespace GitVisualizer;

/// <summary>
/// The class representing a remote repository.
/// </summary>
public class RepositoryRemote(string title, string cloneURL, string webURL) : Repository(title)
{

    /// <summary>
    /// Gets or sets the clone url.
    /// </summary>
    public string CloneURL { get; private set; } = cloneURL;

    /// <summary>
    /// Gets or sets the web url.
    /// </summary>
    public string WebURL { get; private set; } = webURL;

    /*
    public void setLocalRepo(RepositoryLocal localRepository)
    {
        this.localRepository = localRepository;
    }
    */
}
