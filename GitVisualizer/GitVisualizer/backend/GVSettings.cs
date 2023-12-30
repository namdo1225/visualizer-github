using System.Diagnostics;
using System.Text.Json;

namespace GitVisualizer.backend
{

    /// <summary>
    /// The class containing functionalities to work with the app's config files.
    /// </summary>
    public static class GVSettings
    {
        private static readonly string SETTINGS_FPATH = "GitVisualizer.settings.json";

        /// <summary>
        /// Gets or Sets the full config file path.
        /// </summary>
        public static string? FULL_FPATH { get; private set; }

        /// <summary>
        /// Gets or Sets the config data.
        /// </summary>
        public static GVSettingsData Data { get; private set; }

        /// <summary>
        /// The GVSettings constructor.
        /// </summary>
        static GVSettings()
        {
            Data = new GVSettingsData();
            if (!Path.Exists(SETTINGS_FPATH))
                SaveSettings();
            else
            {
                FULL_FPATH = Path.GetFullPath(SETTINGS_FPATH);
                LoadSettings();
            }
        }

        /// <summary>
        /// Loads config settings.
        /// </summary>
        public static void LoadSettings()
        {
            string settingsStr = File.ReadAllText(SETTINGS_FPATH);
            GVSettingsData? nullableData = JsonSerializer.Deserialize<GVSettingsData>(
                settingsStr
            );
            if (nullableData == null)
            {
                File.Delete(SETTINGS_FPATH);
                Data = new GVSettingsData();
            }
            else
            {
                Data = nullableData;
            }
        }

        /// <summary>
        /// Saves config settings.
        /// </summary>
        public static void SaveSettings()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string settingsString = JsonSerializer.Serialize(Data, options);
            File.WriteAllText(SETTINGS_FPATH, settingsString);
            FULL_FPATH = Path.GetFullPath(SETTINGS_FPATH);
        }

        /// <summary>
        /// Class representing config setting data.
        /// </summary>
        public class GVSettingsData
        {

            /// <summary>
            /// Gets or Sets the remember git hub login indicator.
            /// </summary>
            public bool RememberGitHubLogin { get; set; }

            /// <summary>
            /// Gets or Sets the locally tracked directories.
            /// </summary>
            public List<LocalTrackedDir> TrackedLocalDirs { get; set; }

            /// <summary>
            /// Gets or Sets the live repostory path.
            /// </summary>
            public string? LiveRepostoryPath { get; set; }

            /// <summary>
            /// Gets or Sets the app's theme.
            /// </summary>
            public string? Theme { get; set; }

            /// <summary>
            /// The GVSettingsData constructor with prefilled config.
            /// </summary>
            /// <param name="rememberGitHubLogin">The bool for remembering GitHub login.</param>
            /// <param name="trackedLocalDirs">The list of locally tracked directories.</param>
            /// <param name="liveRepostoryPath">The live repostory path.</param>
            /// <param name="theme">The theme of the app.</param>
            public GVSettingsData(bool rememberGitHubLogin, List<LocalTrackedDir> trackedLocalDirs, string liveRepostoryPath, string? theme)
            {
                RememberGitHubLogin = rememberGitHubLogin;
                TrackedLocalDirs = trackedLocalDirs;
                LiveRepostoryPath = liveRepostoryPath;
                Theme = theme;
            }

            /// <summary>
            /// The GVSettingsData default constructor.
            /// </summary>
            public GVSettingsData()
            {
                RememberGitHubLogin = true;
                TrackedLocalDirs = new List<LocalTrackedDir>();
                LiveRepostoryPath = null;
                Theme = "Dark";
            }
        }
    }

    /// <summary>
    /// The class to keep track of information about a locally tracked directory.
    /// </summary>
    public class LocalTrackedDir(string path, bool recursive)
    {

        /// <summary>
        /// Gets or Sets the localled tracked path.
        /// </summary>
        public string Path { get; set; } = path;

        /// <summary>
        /// Gets or Sets the recursive indicator.
        /// </summary>
        public bool Recursive { get; set; } = recursive;

        /// <summary>
        /// Returns the path and recursive indicator.
        /// </summary>
        /// <returns>The string path.</returns>
        public override string ToString()
        {
            string recursiveIndicator = Recursive ? "Recursive" : "Not Recursive";
            return Path + $" [{recursiveIndicator}]";
        }
    }
}