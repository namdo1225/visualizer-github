namespace GitVisualizer;

/// <summary>
/// The class represents a Git commit and stores data about it.
/// </summary>
public class Commit
{
    public int graphRowIndex; // row 0 at bottom; 
    public int graphColIndex = -1; // col 0 is leftmost;
    
    // pointers to parent coords below <row,col>
    public List<Tuple<int,int>> graphOutRowColPairs = new();

    // pointers to child coords above <row,col>
    public List<Tuple<int,int>> graphInRowColPairs = new();

    /// <summary>
    /// Gets or Sets RepositoryLocal of the commit.
    /// </summary>
    public RepositoryLocal? LocalRepository {get; set;}

    /// <summary>
    /// Gets or Sets the Branch of the commit.
    /// </summary>
    public List<Branch> Branches {get; set;} = new();

    /// <summary>
    /// Gets or Sets the long commit hash.
    /// </summary>
    public string? LongCommitHash {get; set;}

    /// <summary>
    /// Gets or Sets the short commit hash.
    /// </summary>
    public string? ShortCommitHash {get; set;}

    /// <summary>
    /// Gets or Sets the long tree hash.
    /// </summary>
    public string? LongTreeHash {get; set;}

    /// <summary>
    /// Gets or Sets the parent hashes.
    /// </summary>
    public List<string> ParentHashes {get; set;} = new();

    /// <summary>
    /// Gets or Sets the com res.
    /// </summary>
    public string? ComRes {get; set;}

    /// <summary>
    /// Gets or Sets the committer name.
    /// </summary>
    public string? CommitterName {get; set;}

    /// <summary>
    /// Gets or Sets the committer date.
    /// </summary>
    public DateTime CommitterDate {get; set;}

    /// <summary>
    /// Gets or Sets the subject.
    /// </summary>
    public string? Subject {get; set;}

    /// <summary>
    /// Gets or Sets the parent commits.
    /// </summary>
    public List<Commit> Parents {get; set;} = new();

    /// <summary>
    /// Gets or Sets the children commits.
    /// </summary>
    public List<Commit> Children {get; set;} = new();
}
