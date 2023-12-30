namespace GitVisualizer;

/// <summary>
/// A class representing the differences between 2 commits for merging.
/// </summary>
public class Diff
{

    /// <summary>
    /// Gets or Sets the 1st commit.
    /// </summary>
    public Commit CommitA { get; private set; }

    /// <summary>
    /// Gets or Sets the 2nd commit.
    /// </summary>
    public Commit CommitB { get; private set; }

    /// <summary>
    /// The constructor for Diff
    /// </summary>
    /// <param name="commitA">The 1st commit for comparsion.</param>
    /// <param name="commitB">The 2nd commit for comparison.</param>
    public Diff(Commit commitA, Commit commitB)
    {
        this.CommitA = commitA;
        this.CommitB = commitB;
        EvaluateDiff();
    }

    /// <summary>
    /// Calculates the difference between 2 commits.
    /// </summary>
    /// <param name="commitA">The commit a.</param>
    /// <param name="commitB">The commit b.</param>
    public void RecalcDiff(Commit commitA, Commit commitB)
    {
        this.CommitA = commitA;
        this.CommitB = commitB;
        EvaluateDiff();
    }

    /// <summary>
    /// Evalutes differences between 2 commits.
    /// </summary>
    private void EvaluateDiff()
    {

    }
}
