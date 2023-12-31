using System.Diagnostics;
using System.Management.Automation;
using System.Text.RegularExpressions;
using GithubSpace;
using GitVisualizer.backend;
using GitVisualizer.backend.git;
using GitVisualizer.UI.UI_Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace GitVisualizer;

/// <summary>
/// The class containing methods to use various Git functionalities.
/// </summary>
public static class GitAPI
{

    /// <summary>
    /// Gets or Sets the Github object.
    /// </summary>
    public static Github Github { get; set; }

    /// <summary> pointer to the live commit </summary>
    public static Branch? LiveBranch { get; private set; }

    /// <summary> currently checked out commit </summary>
    public static Commit? LiveCommit { get; private set; }

    /// <summary> currently tracked local repository </summary>
    public static RepositoryLocal? LiveRepository { get; private set; }

    // url -> remoteRepo
    private static readonly Dictionary<string, RepositoryRemote> remoteRepositories;
    // dirPath -> localRepo;
    private static readonly Dictionary<string, RepositoryLocal> localRepositories;
    // url -> list<localRepo>
    private static readonly Dictionary<string, HashSet<RepositoryLocal>> remoteBackedLocalRepositories;

    /// <summary>
    /// Gets or Sets the number of commits ahead.
    /// </summary>
    public static int? CommitsAhead { get; private set; } = 0;

    /// <summary>
    /// Gets or Sets the number of commits behind.
    /// </summary>
    public static int? CommitsBehind { get; private set; } = 0;

    /// <summary> GitAPI initialization </summary>
    static GitAPI()
    {
        // ref to program GitHub api
        Github = Program.Github;

        remoteRepositories = new Dictionary<string, RepositoryRemote>();
        localRepositories = new Dictionary<string, RepositoryLocal>();
        remoteBackedLocalRepositories = new Dictionary<string, HashSet<RepositoryLocal>>();
    }

    /// <summary>
    /// The class for scanning functionalities.
    /// </summary>
    public class Scanning
    {
        /// <summary>
        /// Scans for local repos.
        /// </summary>
        /// <param name="callback">The Action callback.</param>
        public static void ScanForLocalRepos(Action? callback)
        {
            localRepositories.Clear();
            remoteBackedLocalRepositories.Clear();

            foreach (LocalTrackedDir trackedDir in GVSettings.Data.TrackedLocalDirs)
            {
                string dirPath = trackedDir.Path;
                bool recursive = trackedDir.Recursive;
                SearchOption searchOption = recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

                if (!Directory.Exists(dirPath))
                    continue;

                string[] gitFolderPaths = Directory.GetDirectories(dirPath, ".git", searchOption);
                foreach (string gitFolderPath in gitFolderPaths)
                {
                    // getting parent folder of .git folder
                    DirectoryInfo? repoDirInfo = Directory.GetParent(gitFolderPath);

                    // skipping null info
                    if (repoDirInfo == null)
                        continue;

                    // getting repo folder abs path
                    string repoDirPath = repoDirInfo.FullName;
                    string repoName = repoDirInfo.Name;
                    RepositoryLocal newLocalRepo = new(repoName, repoDirPath);

                    // skipping already loaded local repos
                    if (localRepositories.ContainsKey(newLocalRepo.DirPath))
                        continue;
                    localRepositories[newLocalRepo.DirPath] = newLocalRepo;

                    // skipping remote refs for local only repos
                    string? remoteURL = newLocalRepo.GetRemoteURL();
                    if (remoteURL == null)
                        continue;
                    if (remoteBackedLocalRepositories.ContainsKey(remoteURL))
                    {
                        // skipping already tracked remote backed local repos
                        remoteBackedLocalRepositories[remoteURL].Add(newLocalRepo);
                        continue;
                    }
                    // initalizing with single local repo
                    remoteBackedLocalRepositories[remoteURL] = [newLocalRepo];
                }
            }
            callback?.Invoke();
        }

        /// <summary>
        /// Scans for remote repos.
        /// </summary>
        /// <param name="callback">The Action callback.</param>
        /// <returns>The Task.</returns>
        public static async Task ScanForRemoteReposAsync(Action? callback)
        {
            List<RepositoryRemote>? newRemoteRepos = await Github.ScanReposAsync();
            if (newRemoteRepos != null)
            {
                foreach (RepositoryRemote newRemoteRepo in newRemoteRepos)
                {
                    // skipping already tracked remotes
                    if (remoteRepositories.ContainsKey(newRemoteRepo.CloneURL))
                        continue;
                    remoteRepositories[newRemoteRepo.CloneURL] = newRemoteRepo;
                }
            }
            callback?.Invoke();
        }

        /// <summary>
        /// Scans for all repos.
        /// </summary>
        /// <param name="callback">The Action callback.</param>
        public static async void ScanForAllRepos(Action? callback)
        {
            // loading local repositories
            ScanForLocalRepos(null);
            // loading remote repositories
            await ScanForRemoteReposAsync(null);
            if (GVSettings.Data.LiveRepostoryPath != null && LiveRepository == null && localRepositories.ContainsKey(GVSettings.Data.LiveRepostoryPath))
            {
                RepositoryLocal curRepo = localRepositories[GVSettings.Data.LiveRepostoryPath];
                Actions.LocalActions.SetLiveRepository(curRepo);
            }
            callback?.Invoke();
        }
    }

    /// <summary> The class for Git action functionalities </summary>
    public static class Actions
    {

        /// <summary>
        /// The class for Git remote action functionalities
        /// </summary>
        public static class RemoteActions
        {

            /// <summary>
            /// Untrack remote repos.
            /// </summary>
            /// <param name="callback">The Action callback.</param>
            public static void UntrackRemoteRepos(Action callback)
            {
                remoteRepositories.Clear();
                callback();
            }

            /// <summary>
            /// Creates a remote repository.
            /// </summary>
            /// <param name="localRepo">The local repo data.</param>
            /// <param name="callback">The callback.</param>
            public static async void CreateRemoteRepository(RepositoryLocal localRepo, Action callback)
            {
                string? cloneURL = await Github.CreateRepo(localRepo.Title);
                if (cloneURL == null)
                    return;

                RepositoryLocal? curLiveRepo = LiveRepository;
                LocalActions.SetLiveRepository(localRepo);

                Tuple<string?, string?> curShorthashAndBranch = Getters.GetLiveCommitShortHashAndBranch();

                string? curShortHash = curShorthashAndBranch.Item1;
                string? curBranchName = curShorthashAndBranch.Item2;

                if (cloneURL != null)
                {
                    string com = $"cd '{localRepo.DirPath}'; git remote add origin https://{cloneURL}.git; ";
                    if (curBranchName != null)
                        com += $"git branch -M {curBranchName}; git push -u origin {curBranchName}; ";
                    else if (curShortHash != null)
                        com += $"git branch -M main; git push -u origin main; ";
                    Shell.Exec(com);
                }

                remoteBackedLocalRepositories.Clear();
                localRepositories.Clear();
                remoteRepositories.Clear();

                Scanning.ScanForAllRepos(callback);

                if (curLiveRepo != null)
                    foreach (KeyValuePair<string, RepositoryLocal> localrepoPair in localRepositories)
                    {
                        string repoPath = localrepoPair.Key;
                        if (repoPath == curLiveRepo.DirPath)
                            LocalActions.SetLiveRepository(localrepoPair.Value);
                    }

                callback();
            }

            /// <summary>
            /// Adds a local branch to remote repository.
            /// </summary>
            /// <param name="branch">The branch.</param>
            public static void AddLocalBranchToRemote(Branch branch)
            {
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git push -u {branch.Title}";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Clones a remote repository.
            /// </summary>
            /// <param name="repositoryRemote">The repository remote.</param>
            /// <param name="callback">The Action callback.</param>
            public static void CloneRemoteRepository(RepositoryRemote repositoryRemote, Action? callback)
            {
                FolderBrowserDialog dialog = new();
                DialogResult fdResult = dialog.ShowDialog();
                if (fdResult == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string cloneDirPath = Path.GetFullPath(dialog.SelectedPath);
                    string clonedRepoPath = cloneDirPath + "\\" + repositoryRemote.Title;
                    string com = $"cd '{cloneDirPath}'; git clone https://{repositoryRemote.CloneURL}";

                    Shell.Exec(com);

                    LocalActions.TrackDirectory(clonedRepoPath, true, callback);
                }
            }

            /// <summary>
            /// Syncs a repository.
            /// </summary>
            public static void Sync()
            {
                // fetch and pull
                if (LiveCommit != null)
                {
                    string com = $"cd '{LiveCommit?.LocalRepository?.DirPath}'; git fetch --all";
                    Shell.Exec(com);

                    com = $"cd '{LiveCommit?.LocalRepository?.DirPath}'; git pull --all -p";
                    Shell.Exec(com);

                    if (LiveBranch != null)
                    {
                        com = $"cd '{LiveCommit?.LocalRepository?.DirPath}'; git push origin {LiveBranch.Title}:{LiveBranch.Title}";
                        Shell.Exec(com);
                    }
                    Getters.SetCommitsAheadAndBehind();
                }
            }
        }


        /// <summary> actions on the local filesystem within the currently checked-out commit </summary>
        public static class LocalActions
        {
            /// <summary>
            /// Sets a current live repository.
            /// </summary>
            /// <param name="repositoryLocal">The repository local.</param>
            public static void SetLiveRepository(RepositoryLocal repositoryLocal)
            {
                if (!ReferenceEquals(repositoryLocal, LiveRepository))
                {
                    string com = $"cd '{repositoryLocal.DirPath}'; git init '{repositoryLocal.DirPath}'";

                    Shell.Exec(com);

                    LiveRepository = repositoryLocal;
                    // set commit to currently checked out repo commit
                    Getters.GetCommitsAndBranches();
                    // updating settings
                    GVSettings.Data.LiveRepostoryPath = LiveRepository.DirPath;
                    GVSettings.SaveSettings();
                }
                Getters.SetCommitsAheadAndBehind();
            }

            /// <summary>
            /// Check out a commit
            /// </summary>
            /// <param name="commit">The commit.</param>
            public static void CheckoutCommit(Commit commit)
            {
                if (!ReferenceEquals(commit, LiveCommit))
                {
                    string com = $"cd '{commit?.LocalRepository?.DirPath}'; git checkout {commit?.LongCommitHash}";
                    Shell.Exec(com);
                    LiveCommit = commit;
                    LiveBranch = null;
                }
                Getters.SetCommitsAheadAndBehind();
            }

            /// <summary>
            /// Checkout a branch.
            /// </summary>
            /// <param name="branch">The branch.</param>
            public static void CheckoutBranch(Branch branch)
            {
                if (!ReferenceEquals(branch?.Commit, LiveCommit))
                {
                    string com = $"cd '{branch?.Commit?.LocalRepository?.DirPath}'; git checkout {branch?.Title}";
                    Shell.Exec(com);
                    LiveCommit = branch?.Commit;
                    LiveBranch = branch;
                }
                Getters.SetCommitsAheadAndBehind();
            }

            /// <summary>
            /// Creates a local branch.
            /// </summary>
            /// <param name="title">The title.</param>
            /// <param name="commit">The commit.</param>
            /// <returns>The result.</returns>
            public static Branch CreateLocalBranch(string title, Commit commit)
            {
                string com = $"cd '{commit?.LocalRepository?.DirPath}'; git checkout -b {title} {commit?.LongCommitHash}";
                Shell.Exec(com);
                Branch branch = new(title, commit);
                LiveCommit = branch.Commit;
                return branch;
            }

            /// <summary>
            /// Deletes local branch.
            /// </summary>
            /// <param name="branch">The branch.</param>
            public static void DeleteBranchLocal(Branch branch)
            {
                if (!ReferenceEquals(branch.Commit, LiveCommit))
                {
                    string com = $"cd '{branch?.Commit?.LocalRepository?.DirPath}'; git branch -D {branch?.Title}";
                    Shell.Exec(com);
                    com = $"cd '{branch?.Commit?.LocalRepository?.DirPath}'; git push origin -d {branch?.Title}";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Merge local branch.
            /// </summary>
            public static void Merge(string branch)
            {
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git merge {branch}";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Tracks a directory.
            /// </summary>
            /// <param name="dirPath">The directory path.</param>
            /// <param name="recursive">The recursive bool. Whether to analyze directories recursively.</param>
            /// <param name="callback">The callback.</param>
            public static void TrackDirectory(string dirPath, bool recursive, Action? callback)
            {
                foreach (LocalTrackedDir trackedDir in GVSettings.Data.TrackedLocalDirs)
                {
                    if (trackedDir.Path == dirPath)
                    {
                        trackedDir.Recursive = recursive;
                        return;
                    }
                }
                LocalTrackedDir newTrackedDir = new(dirPath, recursive);
                GVSettings.Data.TrackedLocalDirs.Add(newTrackedDir);
                GVSettings.SaveSettings();
                Scanning.ScanForLocalRepos(callback);
            }

            /// <summary>
            /// Untracks a directory.
            /// </summary>
            /// <param name="trackedDir">The tracked directory.</param>
            /// <param name="callback">The Action callback.</param>
            public static void UntrackDirectory(LocalTrackedDir? trackedDir, Action? callback, String? path = null)
            {
                if (trackedDir != null && GVSettings.Data.TrackedLocalDirs.Contains(trackedDir))
                    GVSettings.Data.TrackedLocalDirs.Remove(trackedDir);
                else if (path != null) {
                    List<LocalTrackedDir> dirsByPath = GVSettings.Data.TrackedLocalDirs.Where(p => p.Path == path).ToList();
                    foreach (LocalTrackedDir dir in dirsByPath)
                        if (dir != null)
                            GVSettings.Data.TrackedLocalDirs.Remove(dir);
                }

                GVSettings.SaveSettings();
                Scanning.ScanForLocalRepos(callback);
            }

            /// <summary>
            /// User selects to track a directory.
            /// </summary>
            /// <param name="recursive">The recursive bool. Whether to analyze directory recursively.</param>
            /// <param name="callback">The Action callback.</param>
            public static void UserSelectTrackDirectory(bool recursive, Action? callback)
            {
                FolderBrowserDialog dialog = new();
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string fullPath = Path.GetFullPath(dialog.SelectedPath);
                    TrackDirectory(fullPath, recursive, callback);
                }
            }

            /// <summary>
            /// Gets tracked directories.
            /// </summary>
            /// <returns>The List of Locally Tracked directories.</returns>
            public static List<LocalTrackedDir> GetTrackedDirs()
            {
                return GVSettings.Data.TrackedLocalDirs;
            }

            /// <summary>
            /// Creates a local repository.
            /// </summary>
            public static void CreateLocalRepository()
            {
                FolderBrowserDialog dialog = new();
                DialogResult fdResult = dialog.ShowDialog();
                if (fdResult == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    string repoDirPath = Path.GetFullPath(dialog.SelectedPath);
                    string? repoName = Path.GetDirectoryName(repoDirPath);
                    if (repoName == null)
                        return;

                    string com = $"cd '{repoDirPath}'; git init --initial-branch=main; git add -A; git commit -m 'initalizing {repoName}'";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Performs the git clean command.
            /// </summary>
            public static void Clean()
            {
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git clean -fdx";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Stages changes.
            /// </summary>
            /// <param name="fpath">The file path.</param>
            public static void StageChange(string fpath)
            {
                // stage file changes to commit
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git add '{fpath}'";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Unstages changes.
            /// </summary>
            /// <param name="fpath">The file path.</param>
            public static void UnStageChange(string fpath)
            {
                // unstage file changes to commit
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git reset '{fpath}'";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Commits staged changes.
            /// </summary>
            /// <param name="message">The message for the commit.</param>
            public static void CommitStagedChanges(string message)
            {
                // unstage all staged changes
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git commit -m '{message}'";
                    Shell.Exec(com);
                }
                Getters.SetCommitsAheadAndBehind();
            }

            /// <summary>
            /// Unstages all staged changes.
            /// </summary>
            public static void UnstageAllStagedChanges()
            {
                // unstage all staged changes
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git reset";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Stages all unstaged changes.
            /// </summary>
            public static void StageAllUnstagedChanges()
            {
                // stage all changed files
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git add -u";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Reverts an unstaged change.
            /// </summary>
            /// <param name="fpath">The file path.</param>
            public static void RevertUnstagedChange(string fpath)
            {
                // reset file to state without any changes
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git checkout '{fpath}'";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Reverts all unstaged changes.
            /// </summary>
            public static void RevertAllUnstagedChanges()
            {
                // reset file to state without any changes
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git checkout .";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// Undo last commit.
            /// </summary>
            public static void UndoLastCommit()
            {
                if (LiveRepository != null)
                {
                    string com = $"cd '{LiveRepository.DirPath}'; git reset --soft HEAD~1";
                    Shell.Exec(com);
                }
            }

            /// <summary>
            /// The delete repo.
            /// </summary>
            /// <param name="localRepo">The local repo.</param>
            /// <returns>The result.</returns>
            public static bool DeleteRepo(RepositoryLocal localRepo)
            {
                string repoDirPath = localRepo.DirPath;
                string com = $"rm '{repoDirPath}' -r -force";
                ShellComRes result = Shell.Exec(com);
                return result.Success;
            }
        }
    }

    /// <summary> Git Data Getters </summary>
    public static class Getters
    {

        /// <summary>
        /// Retrieves file differences.
        /// </summary>
        /// <param name="fpath">The file path.</param>
        /// <param name="isStaged">A bool for whether files are staged or not.</param>
        /// <returns>The result.</returns>
        public static List<Tuple<string, string>> GetFileDiff(string fpath, bool isStaged)
        {
            // assumes valid fpath
            if (LiveRepository == null)
                return new();

            string stagedFlag = isStaged ? "--staged" : "";
            string com = $"cd '{LiveRepository.DirPath}'; git diff {stagedFlag} '{fpath}';";
            ShellComRes result = Shell.Exec(com);
            if (result.PsObjects == null)
                return new();

            List<string> diffLines = result.PsObjects.Select(s => s.ToString()).ToList();
            List<Tuple<string, string>> diff = new();

            int i = 0;
            // stripping command output header
            foreach (string line in diffLines)
            {
                if (line[0].Equals('@'))
                    break;
                i++;
            }

            for (int j = 0; j < i; j++)
            {
                string line = diffLines[0];
                diffLines.RemoveAt(0);
            }
            // building new and old
            foreach (string line in diffLines)
            {
                if (line[0].Equals('-'))
                    diff.Add(new("\n", line));
                else if (line[0].Equals('+'))
                    diff.Add(new(line, "\n"));
                else
                    diff.Add(new(line, line));
            }
            return diff;
        }

        /// <summary>
        /// Gets staged files.
        /// </summary>
        /// <returns>The List of files.</returns>
        public static List<Tuple<string, string>> GetStagedFiles()
        {
            // list<tuple<action,fpath>>
            if (LiveRepository != null)
            {
                string com = $"cd '{LiveRepository.DirPath}'; git diff --cached --name-status";
                ShellComRes result = Shell.Exec(com);

                if (result.PsObjects == null)
                    return new();

                // action(add,del,mod), fpath
                List<Tuple<string, string>> changes = new();
                foreach (PSObject pso in result.PsObjects)
                {
                    string line = pso.ToString().Trim();
                    // line : eventNameChar     fpath
                    string action = line[0].ToString().ToUpper();
                    string fpath = line[1..].Trim();
                    changes.Add(new Tuple<string, string>(action, fpath));
                }
                return changes;
            }
            return new();
        }

        /// <summary>
        /// Gets unstaged files.
        /// </summary>
        /// <returns>The List of files.</returns>
        public static List<Tuple<string, string>> GetUnStagedFiles()
        {
            // list<tuple<action,fpath>>
            if (LiveRepository != null)
            {
                string com = $"cd '{LiveRepository.DirPath}'; git add -A -n";
                ShellComRes result = Shell.Exec(com);
                if (result.PsObjects == null)
                    return new();

                // action(add,del,mod), fpath
                List<Tuple<string, string>> changes = new();
                foreach (PSObject pso in result.PsObjects)
                {
                    string line = pso.ToString().Trim();
                    // line : eventName 'fpath'
                    string[] splitLine = line.Split(" ");
                    // extracting
                    string action = line[0].ToString().ToUpper();
                    // extracting fname
                    string fpath = string.Join(" ", splitLine[1..]);
                    // removing enclosing parens
                    fpath = fpath[1..^1];
                    changes.Add(new Tuple<string, string>(action, fpath));
                }
                return changes;
            }
            return new();
        }

        /// <summary>
        /// Gets remote repositories.
        /// </summary>
        /// <returns>The list of remote repositories.</returns>
        public static List<RepositoryRemote> GetRemoteRepositories()
        {
            return remoteRepositories.Values.ToList();
        }

        /// <summary>
        /// Gets local repositories.
        /// </summary>
        /// <returns>The list of local repositories.</returns>
        public static List<RepositoryLocal> GetLocalRepositories()
        {
            return localRepositories.Values.ToList();
        }

        /// <summary>
        /// Gets all repositories.
        /// </summary>
        /// <returns>The list of local and remote repositories.</returns>
        public static List<Tuple<RepositoryLocal?, RepositoryRemote?>> GetAllRepositories()
        {
            List<Tuple<RepositoryLocal?, RepositoryRemote?>> repoPairs = new();

            HashSet<string> remoteURLs = remoteRepositories.Keys.ToHashSet();
            HashSet<string> localBackedURLS = remoteBackedLocalRepositories.Keys.ToHashSet();
            HashSet<string> allURLS = new();

            allURLS.UnionWith(remoteURLs);
            allURLS.UnionWith(localBackedURLS);

            HashSet<RepositoryLocal> curLocals = localRepositories.Values.ToHashSet();
            foreach (string remoteURL in allURLS)
            {
                // backed local + github
                if (remoteURLs.Contains(remoteURL) && localBackedURLS.Contains(remoteURL))
                {
                    RepositoryLocal? curLocal = null;
                    foreach (RepositoryLocal local in remoteBackedLocalRepositories[remoteURL])
                    {
                        RepositoryRemote remote = remoteRepositories[remoteURL];
                        curLocal = local;
                        repoPairs.Add(new Tuple<RepositoryLocal?, RepositoryRemote?>(local, remote));
                    }
                    if (curLocal != null)
                        curLocals.Remove(curLocal);
                }

                // backed local without github
                else if (localBackedURLS.Contains(remoteURL))
                {
                    RepositoryLocal? curLocal = null;
                    foreach (RepositoryLocal local in remoteBackedLocalRepositories[remoteURL])
                    {
                        curLocal = local;
                        repoPairs.Add(new Tuple<RepositoryLocal?, RepositoryRemote?>(local, null));
                    }
                    if (curLocal != null)
                        curLocals.Remove(curLocal);
                }

                // github only
                else
                {
                    RepositoryRemote remote = remoteRepositories[remoteURL];
                    repoPairs.Add(new Tuple<RepositoryLocal?, RepositoryRemote?>(null, remote));
                }
            }

            // local only
            foreach (RepositoryLocal local in curLocals)
                repoPairs.Add(new Tuple<RepositoryLocal?, RepositoryRemote?>(local, null));
            return repoPairs;
        }

        /// <summary>
        /// Gets live commit short hash and branch.
        /// </summary>
        /// <returns>The list of live commit short hash and branch.</returns>
        public static Tuple<string?, string?> GetLiveCommitShortHashAndBranch()
        {
            if (LiveRepository != null)
            {
                string com = $"cd '{LiveRepository.DirPath}'; git log -1 --pretty=format:%h";
                ShellComRes result = Shell.Exec(com);
                if (result.PsObjects == null)
                {
                    return new(null, null);
                }
                if (result.PsObjects.Count == 0)
                {
                    return new(null, null);
                }
                string shortHash = result.PsObjects[0].ToString().Trim();

                com = $"cd '{LiveRepository.DirPath}'; git rev-parse --abbrev-ref HEAD";
                result = Shell.Exec(com);
                if (result.PsObjects == null)
                    return new(null, null);

                string? branchName = result.PsObjects[0].ToString().Trim();
                if (branchName == "HEAD")
                    branchName = null;

                return new(shortHash, branchName);
            }
            return new(null, null);
        }

        /// <summary>
        /// Sets commits ahead and behind.
        /// </summary>
        public static void SetCommitsAheadAndBehind()
        {
            if (LiveRepository != null && LiveBranch != null) {
                    string com = $"cd '{LiveRepository.DirPath}'; git rev-list --left-right --count ";
                    com += (LiveRepository.GetRemoteURL() != null) ?
                            $"{LiveBranch.Title}...origin/{LiveBranch.Title}" :
                            $"{LiveBranch.Title}...{LiveBranch.Title}";

                    ShellComRes result = Shell.Exec(com);
                    if (result.PsObjects == null || result.PsObjects.Count == 0)
                    {
                        CommitsAhead = CommitsBehind = null;
                        return;
                    }
                    string line = result.PsObjects[0].ToString().Trim();
                    line = Regex.Replace(line, @"\s+", " ");
                    string[] nums = line.Split(" ");
                    CommitsBehind = int.Parse(nums[1]);
                    CommitsAhead = int.Parse(nums[0]);
            }
            else
                CommitsAhead = CommitsBehind = null;
        }

        /// <summary>
        /// Populates commit graph data.
        /// </summary>
        /// <param name="cur">The current commit.</param>
        /// <param name="colIndex">The column index.</param>
        /// <returns>Whether the graph has been populated.</returns>
        public static bool PopulateCommitGraphData(Commit cur, int colIndex)
        {
            if (cur.graphColIndex != -1)
            {
                // cur has already been visited
                return true;
            }
            cur.graphColIndex = colIndex;
            int childColIndex = colIndex;
            IEnumerable<Commit> reverseChildren = cur.Children;
            foreach (Commit child in reverseChildren.Reverse())
            {
                bool alreadyVisited = PopulateCommitGraphData(child, childColIndex);
                if (alreadyVisited)
                {
                    childColIndex--;
                }
                else
                {
                    childColIndex++;
                }
                
                Tuple<int, int> outRowColPair = new(cur.graphRowIndex, cur.graphColIndex);
                child.graphOutRowColPairs.Add(outRowColPair);
                
                Tuple<int, int> inRowColPair = new(child.graphRowIndex, child.graphColIndex);
                cur.graphInRowColPairs.Add(inRowColPair);
            }
            // cur has been visited for the first time
            return false;
        }

        /// <summary>
        /// Gets commits and branches.
        /// </summary>
        /// <returns>The list of branches and their commits.</returns>
        public static Tuple<List<Branch>, List<Commit>> GetCommitsAndBranches()
        {
            Tuple<string?, string?> liveCommitShortHashAndBranchName = GetLiveCommitShortHashAndBranch();
            string? liveCommitShortHash = liveCommitShortHashAndBranchName.Item1;
            string? liveBranchName = liveCommitShortHashAndBranchName.Item2;

            if (LiveRepository != null)
            {
                string baseCom = $"cd '{LiveRepository.DirPath}'; ";
                // Commit hash (H)
                // Abbreviated commit hash (h)
                // Tree hash (T)
                // Parent Hashes (P)
                // Committer name (cn)
                // Committer date (cd)
                // Subject (s)

                // all commits
                Dictionary<string, Commit> longHashToCommitDict = new();
                Dictionary<string, Commit> shortHashToCommitDict = new();
                List<Commit> commits = new();
                string delim = " | ";
                string com = baseCom + $"cd '{LiveRepository.DirPath}'; git log --all --oneline --pretty=format:\"%H{delim}%h{delim}%T{delim}%P\"";
                ShellComRes comResult = Shell.Exec(com);
                if (comResult.PsObjects == null)
                    return new(new(), new());

                // longCommitHash, shortCommitHash, longTreeHash, parentHashes
                foreach (PSObject pso in comResult.PsObjects)
                {
                    string sline = pso.ToString().Trim();
                    string[] cols = sline.Split(delim);

                    Commit commit = new()
                    {
                        LocalRepository = LiveRepository,
                        LongCommitHash = cols[0],
                        ShortCommitHash = cols[1],
                        LongTreeHash = cols[2],

                        ComRes = sline
                    };

                    if (cols.Length > 3)
                        foreach (string parentHash in cols[3].Split(" "))
                            commit.ParentHashes.Add(parentHash.Trim());

                    longHashToCommitDict[commit.LongCommitHash] = commit;
                    shortHashToCommitDict[commit.ShortCommitHash] = commit;
                    commits.Add(commit);

                    if (liveCommitShortHash != null && commit.ShortCommitHash == liveCommitShortHash)
                        LiveCommit = commit;
                }

                // committer name
                com = baseCom + $"git log --all --oneline --pretty=format:\"%cn\"";
                comResult = Shell.Exec(com);
                if (comResult.PsObjects == null)
                {
                    return new(new(), new());
                }
                int i = 0;
                foreach (PSObject pso in comResult.PsObjects)
                {
                    string committerName = pso.ToString().Trim();
                    Commit commit = commits[i];
                    commit.CommitterName = committerName;
                    i++;
                }

                // committer date
                com = baseCom + $"git log --all --oneline --pretty=format:\"%cd\"";
                comResult = Shell.Exec(com);
                if (comResult.PsObjects == null)
                {
                    return new(new(), new());
                }
                i = 0;
                foreach (PSObject pso in comResult.PsObjects)
                {
                    string gitDateFormat = pso.ToString().Trim();
                    DateTime.TryParseExact(
                        gitDateFormat,
                        "ddd MMM d HH:mm:ss yyyy K",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None,
                        out DateTime committerDate
                    );
                    Commit commit = commits[i];
                    commit.CommitterDate = committerDate;
                    i++;
                }

                // subject
                com = baseCom + $"git log --all --oneline --pretty=format:\"%s\"";
                comResult = Shell.Exec(com);
                if (comResult.PsObjects == null)
                {
                    return new(new(), new());
                }
                i = 0;
                foreach (PSObject pso in comResult.PsObjects)
                {
                    string subject = pso.ToString().Trim();
                    Commit commit = commits[i];
                    commit.Subject = subject;
                    i++;
                }

                // setting internal refs to build commit tree
                foreach (KeyValuePair<string, Commit> kvp in longHashToCommitDict)
                {
                    string longHash = kvp.Key;
                    Commit commit = kvp.Value;
                    foreach (string parentHash in commit.ParentHashes)
                    {
                        Commit parentCommit = longHashToCommitDict[parentHash];
                        commit.Parents.Add(parentCommit);
                        parentCommit.Children.Add(commit);
                    }
                }

                // populating graph row properties of commits
                int commitGraphRowIndex = commits.Count - 1;
                foreach (Commit c in commits)
                {
                    c.graphRowIndex = commitGraphRowIndex;
                    commitGraphRowIndex--;
                }
                // populating graph col properties of commits
                if (commits.Count > 0)
                {
                    Commit initCommit = commits.Last();
                    PopulateCommitGraphData(initCommit, 0);
                }

                // getting all branches
                List<Branch> allBranches = new();
                // getting commits
                // list local branchs : *(live or not) | name | short hash | most recent commit msg
                com = baseCom + $"git branch -vva";
                comResult = Shell.Exec(com);
                if (comResult.PsObjects == null)
                {
                    return new(new(), new());
                }
                foreach (PSObject pso in comResult.PsObjects)
                {
                    string line = pso.ToString().TrimEnd();
                    line = Regex.Replace(line, @"\s+", " ");
                    string[] items = line.Split(" ");
                    string title = items[1];
                    string shortCommitHash = items[2];

                    // setting branhc-commit refs
                    if (shortHashToCommitDict.ContainsKey(shortCommitHash))
                    {
                        Commit commit = shortHashToCommitDict[shortCommitHash];
                        Branch branch = new(title, commit);
                        commit.Branches.Add(branch);
                        allBranches.Add(branch);

                        if (liveCommitShortHash != null && liveBranchName != null && liveCommitShortHash == shortCommitHash && liveBranchName == branch.Title)
                            LiveBranch = branch;
                    }
                }

                SetCommitsAheadAndBehind();

                // branches-commits tuples
                return new Tuple<List<Branch>, List<Commit>>(allBranches, commits);
            }

            return new(new(), new());
        }
    }
}
