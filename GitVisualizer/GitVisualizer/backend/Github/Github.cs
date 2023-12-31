using System.Text;
using System.Net.Http.Headers;

using CredentialManagement;
using Newtonsoft.Json.Linq;
using GitVisualizer;

namespace GithubSpace
{

    /// <summary>
    /// Class <c>Github</c> contains methods to abstract the interaction with the GitHub API and contain many of Github user's data.
    /// </summary>
    public class Github
    {
        /// <summary>
        /// Class <c>CredentialStore</c> contains methods to work with Windows Credential Manager to read and write GitHub user's data.
        /// </summary>
        private static class CredentialStore
        {
            private static readonly string CREDENTIAL_LOCATION = "VisualizerGitHub_user";

            /// <summary>
            /// Saves GitHub user's credential to Windows Credential Manager.
            /// </summary>
            /// <param name="username">The GitHub username.</param>
            /// <param name="token">The token.</param>
            /// <returns>true if credential is saved. false otherwise.</returns>
            public static bool SaveCredential(string username, string token)
            {
                Credential credential = new()
                {
                    Target = CREDENTIAL_LOCATION
                };

                if (!credential.Exists())
                {
                    credential.Username = username;
                    credential.Password = token;
                    credential.Type = CredentialType.Generic;
                    credential.PersistanceType = PersistanceType.LocalComputer;
                    credential.Save();
                    return true;
                }

                return false;
            }

            /// <summary>
            /// Retrieves user's token from Windows Credential Manager.
            /// </summary>
            /// <returns>A String for the token. null if retrieval fails.</returns>
            public static string? GetToken()
            {
                Credential credential = new()
                {
                    Target = CREDENTIAL_LOCATION
                };
                if (credential.Exists())
                {
                    credential.Load();
                    return credential.Password;
                }
                return null;
            }

            /// <summary>
            /// Retrieves user's username from Windows Credential Manager.
            /// </summary>
            /// <returns>A String for the username. empty string if retrieval fails.</returns>
            public static string? GetUserName()
            {
                Credential credential = new()
                {
                    Target = CREDENTIAL_LOCATION
                };
                if (credential.Exists())
                {
                    credential.Load();
                    return credential.Username;
                }
                return null;
            }

            /// <summary>
            /// Deletes GitHub user's credential from Windows Credential Manager.
            /// </summary>
            /// <returns>true if deletion is successful. false otherwise.</returns>
            public static bool DeleteCredential()
            {
                Credential credential = new()
                {
                    Target = CREDENTIAL_LOCATION
                };
                if (credential.Exists())
                {
                    credential.Delete();
                    return true;
                }
                return false;
            }
        }

        /// <summary> Gets or Sets the user code. </summary>
        public static string? UserCode { get; private set; } = null;

        /// <summary>
        /// Gets or Sets the device code.
        /// </summary>
        private static string? deviceCode = null;

        /// <summary> Gets or Sets the access token. </summary>
        public static string? AccessToken { get; private set; } = null;

        /// <summary>
        /// Gets or Sets the ask for delete repo permission boolean.
        /// </summary>
        public static bool AskDeleteRepo { get; private set; } = false;

        /// <summary>
        /// Gets or Sets the delete repo boolean.
        /// </summary>
        public static bool DeleteRepoPermission { get; private set; } = false;

        /// <summary>
        /// URL for login with code page on github
        /// </summary>
        public static readonly string API_DEVICE_LOGIN_CODE_URL = "https://github.com/login/device";

        public static readonly string BACKEND_API = "https://visualize-github-backend.onrender.com/api";

        private static int interval = 5;

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://api.github.com/"),
        };
        private static readonly ProductInfoHeaderValue PRODUCT = new("product", "1");
        private static readonly MediaTypeWithQualityHeaderValue JSON_TYPE = new("application/json");
        private static readonly MediaTypeWithQualityHeaderValue GITHUB_TYPE = new("application/vnd.github+json");

        private static readonly int MAX_AUTH_WAIT_DUR = 30;
        private static readonly int DEFAULT_RETRIES = 5;

        /// <summary>
        /// Gets or Sets the username.
        /// </summary>
        public static string? Username { get; private set; }

        /// <summary>
        /// Gets or Sets the avatar url.
        /// </summary>
        public static string? AvatarURL { get; private set; }

        /// <summary>
        /// Gets or Sets the user git hub url.
        /// </summary>
        public static string? UserGitHubURL { get; private set; }

        /// <summary>
        /// The constructor for Github.
        /// </summary>
        /// <param name="client">The HTTP client.</param>
        /// <param name="url">The url for the client.</param>
        public Github(HttpClient? client = null, string? url = null)
        {
            if (client != null)
                sharedClient = client;

            if (url != null)
                sharedClient.BaseAddress = new Uri(url);
        }

        /// <summary>
        /// Attempts to ask user to grant the app permission to read/write public repositories.
        /// </summary>
        /// <param name="scope">The scope as a string. "public" for read/write access to public repos.
        /// "private" read/write access to ALL repos.</param>
        /// <returns>The user code.</returns>
        public static async Task<string?> GivePermission(string scope = "public", bool delete = false)
        {
            AskDeleteRepo = delete;
            return await Task.Run(() => RegisterUser(scope, delete));
        }

        /// <summary>
        /// The method waits for the user to authorize the app to read and write to the user's repository.
        /// </summary>
        /// <returns>The task object.</returns>
        public static async Task WaitForAuthorization()
        {
            await Task.Run(PollAuthorizationDevice);
        }

        /// <summary>
        /// The method gets a list of the user repository as JSON.
        /// </summary>
        /// <param name="scope">The scope as a string. "public" for all public repos. "private" for all private repos.
        /// "all" for all user's repos.</param>
        /// <returns>The list of remote repositories.</returns>
        public static async Task<List<RepositoryRemote>?> ScanReposAsync(string scope = "all")
        {
            return await Task.Run(() => GetRepoList(scope));
        }

        /// <summary>
        /// The method deletes the user access token, essentially disassociating them from the app.
        /// </summary>
        /// <returns>The bool for whether the revoking process completed successfully.</returns>
        public static bool DeleteToken()
        {
            DeleteStoredCredential();
            return AccessToken != null && RevokeAccessToken();
        }

        /// <summary>
        /// The method runs the request to get specific information about the user.
        /// </summary>
        /// <returns>The user's username.</returns>
        public static async Task<string?> GetUserInfo()
        {
            if (AccessToken == null)
                return null;
            return await Task.Run(GetGitHubUser);
        }

        /// <summary>
        /// Returns an authenticated string good for git cloning.
        /// </summary>
        /// <returns>The string URL for git cloning.</returns>
        public static string? CreateAuthenticatedGit(string url)
        {
            if (AccessToken == null)
                return null;
            return "https://" + AccessToken + "@" + url;
        }

        /// <summary>
        /// Saves the GitHub user's credentials into local computer.
        /// </summary>
        /// <returns>A bool. true if saved. false otherwise.</returns>
        public static bool SaveUser()
        {
            if (Username == null)
                Task.Run(GetUserInfo).Wait();

            if (AccessToken == null || Username == null)
                return false;

            return CredentialStore.SaveCredential(Username, AccessToken);
        }

        /// <summary>
        /// Read token and user name from storage.
        /// </summary>
        /// <returns>true if members accessToken and username have been set properly. false otherwise.</returns>
        public static bool LoadStoredCredentials()
        {
            string? loadedToken = CredentialStore.GetToken();
            string? loadedUsername = CredentialStore.GetUserName();
            AccessToken = loadedToken ?? AccessToken;
            Username = loadedUsername ?? Username;
            return AccessToken != null && Username != null;
        }

        /// <summary>
        /// Delete GitHub user's credential from storage. NOTE: Might be useful to restrict user's token as well.
        /// </summary>
        /// <returns>true if credential is deleted. false otherwise.</returns>
        public static bool DeleteStoredCredential()
        {
            return CredentialStore.DeleteCredential();
        }

        /// <summary>
        /// Deletes a remote repo on GitHub.
        /// </summary>
        /// <returns>The result.</returns>
        public static async Task<bool> DeleteRemoteRepo(string url)
        {
            if (AccessToken == null)
                return false;

            return await Task.Run(() => DeleteRepo(url));
        }

        /// <summary>
        /// The private method to handle registering user with the app.
        /// </summary>
        /// <param name="scope">The scope as a string. "public" for read/write access to public repos.
        /// "private" read/write access to ALL repos.</param>
        /// <returns>The user code.</returns>
        private static async Task<string?> RegisterUser(string scope = "public", bool delete = false)
        {
            string actualScope = (scope == "private") ? "repo" : "public_repo";
            if (delete)
                actualScope += ",delete_repo";

            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    client_id = GetClientID(),
                    scope = actualScope
                }),
                Encoding.UTF8,
                 JSON_TYPE);

            CommonHelper();
            HttpResponseMessage response;
            try
            {
                response = await sharedClient.PostAsync("https://github.com/login/device/code", jsonContent);
            }
            catch (Exception)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);

                string s = json["interval"].ToString();
                interval = int.Parse(s);
                deviceCode = json["device_code"].ToString();
                UserCode = json["user_code"].ToString();

                return UserCode;
            }

            return null;
        }

        /// <summary>
        /// The private method to handle registering user with the app.
        /// </summary>
        /// <returns>The task object.</returns>
        private static async Task PollAuthorizationDevice()
        {
            if (UserCode == null)
                return;

            CommonHelper();
            PeriodicTimer timer = new(TimeSpan.FromSeconds(interval + 5));
            int calculatedRetries = MAX_AUTH_WAIT_DUR / interval;
            int finalRetries = (calculatedRetries == 1) ? DEFAULT_RETRIES : calculatedRetries;

            while (await timer.WaitForNextTickAsync() && finalRetries > 0)
            {
                string? status = await SendAuthorizationRequest();
                if (status != null || status == "denied")
                    break;
                finalRetries--;
            }
        }

        /// <summary>
        /// The private method to handle polling for the user to grant the app the appropriate permission to read/write to repository.
        /// </summary>
        /// <returns>The Task<String> object that can be awaited for the String. "denied" means the user rejects the permission grant request.
        /// Any code returned means the user accepted the request.</returns>
        private static async Task<string?> SendAuthorizationRequest()
        {
            HttpResponseMessage response;
            StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    client_id = GetClientID(),
                    device_code = deviceCode,
                    grant_type = "urn:ietf:params:oauth:grant-type:device_code"
                }),
                Encoding.UTF8,
                JSON_TYPE);

            try
            {
                response = await sharedClient.PostAsync("https://github.com/login/oauth/access_token", jsonContent);
            }
            catch (Exception)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                if (content.Contains("access_denied"))
                    return "denied";
                else if (!content.Contains("error"))
                {
                    JObject json = JObject.Parse(content);
                    AccessToken = json["access_token"].ToString();
                }

                DeleteRepoPermission = AskDeleteRepo;
            }
            return AccessToken;
        }

        /// <summary>
        /// The private method to handle getting the GitHub user's repository list.
        /// </summary>
        /// <param name="scope">The scope as a string. "public" for all public repos. "private" for all private repos.
        /// "all" for all user's repos.</param>
        /// <returns>The result.</returns>
        private static async Task<List<RepositoryRemote>?> GetRepoList(string scope = "all")
        {
            scope = (scope != "private" && scope != "all") ? "public" : scope;
            if (AccessToken == null)
                return null;
            CommonAuthenticatedHelper();
            int page = 1;

            List<RepositoryRemote> repositoryRemotes = new();
            while (true)
            {
                HttpResponseMessage response;
                try
                {
                    response = await sharedClient.GetAsync($"{sharedClient.BaseAddress}user/repos?type={scope}&per_page=100&page={page++}");
                }
                catch (Exception)
                {
                    return null;
                }

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    JArray jRemotesArray = JArray.Parse(content);
                    foreach (JToken jToken in jRemotesArray)
                    {
                        JToken? titleToken = jToken["name"];
                        JToken? cloneUrlHTTPSToken = jToken["git_url"];
                        if (titleToken == null)
                        {
                            return null;
                        }
                        if (cloneUrlHTTPSToken == null)
                        {
                            return null;
                        }
                        string title = titleToken.ToString();
                        string cloneURL = cloneUrlHTTPSToken.ToString()[6..];
                        string webURL = cloneURL[..^4];
                        webURL = $"https://{webURL}";
                        RepositoryRemote remote = new(title, cloneURL, webURL);
                        repositoryRemotes.Add(remote);
                    }
                    if (jRemotesArray.Count == 0)
                        return repositoryRemotes;
                }
            }
        }

        /// <summary>
        /// The private method to handle getting the GitHub user's public information.
        /// </summary>
        /// <returns>The Task<String> object that can be awaited for the String</returns>
        private static async Task<string?> GetGitHubUser()
        {
            CommonAuthenticatedHelper();

            HttpResponseMessage response;
            try
            {
                response = await sharedClient.GetAsync($"{sharedClient.BaseAddress}user");
            }
            catch (Exception)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                Username = json["login"].ToString();
                AvatarURL = json["avatar_url"].ToString();
                UserGitHubURL = json["html_url"].ToString();
                return Username;
            }

            return null;
        }


        /// <summary>
        /// The private method to revoke a user access token. Method will also call DeleteStoredCredential().
        /// </summary>
        /// <returns>The Task<String> object that can be awaited for the String</returns>
        private static bool RevokeAccessToken()
        {
            // Retrieves secret from backend server
            HttpResponseMessage response;
            try
            {
                response = sharedClient.GetAsync($"{BACKEND_API}/secret?user={Github.Username}&accessToken={Github.AccessToken}").Result;
            }
            catch (Exception)
            {
                return false;
            }

            string secret = "";
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(content);
                secret = json["clientsecret"].ToString();
            }

            // Now on to deleting the authorization
            StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    access_token = AccessToken,
                }),
                Encoding.UTF8,
                JSON_TYPE);

            HttpRequestMessage request = new(HttpMethod.Delete, $"{sharedClient.BaseAddress}applications/{GetClientID()}/grant");
            request.Headers.Add("Accept", "application/vnd.github+json");
            request.Headers.Add("User-Agent", PRODUCT.ToString());
            request.Content = jsonContent;
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", secret);

            try
            {
                response = sharedClient.Send(request);
            } catch (Exception)
            {
                return false;
            }

            if (response.StatusCode.ToString() == "NoContent")
            {
                AccessToken = Username = AvatarURL = UserGitHubURL = null;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to create an empty public repo with a specified name on GitHub.
        /// </summary>
        /// <param name="repoName">The repo name.</param>
        /// <returns>The git clone url of the newly created GitHub repo.</returns>
        public static async Task<string?> CreateRepo(string repoName)
        {
            if (AccessToken == null)
                return null;
            CommonAuthenticatedHelper();

            using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    name = repoName,
                }),
                Encoding.UTF8,
                 JSON_TYPE);

            HttpResponseMessage response;
            try
            {
                response = await sharedClient.PostAsync("https://api.github.com/user/repos", jsonContent);
            }
            catch (Exception)
            {
                return null;
            }

            if (response.StatusCode.ToString() == "Created")
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                int gitEndIndex = 8;
                return json["clone_url"].ToString()[gitEndIndex..]; ;
            }

            return null;
        }

        /// <summary>
        /// Deletes a remote repository.
        /// </summary>
        /// <param name="url">The url of the remote repository. URL has to point to a GitHub repository</param>
        /// <returns>true if repository is deleted.</returns>
        private static async Task<bool> DeleteRepo(string url)
        {
            HttpResponseMessage response;
            string[] subs = url.Split('/');

            if (!subs.Contains("github.com") || subs.Length != 5)
                return false;
            string userName = subs[3];
            string repo = subs[4];

            HttpRequestMessage request = new(HttpMethod.Delete, $"{sharedClient.BaseAddress}repos/{userName}/{repo}");
            request.Headers.Add("Accept", "application/vnd.github+json");
            request.Headers.Add("User-Agent", PRODUCT.ToString());
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

            try
            {
                response = await sharedClient.SendAsync(request);
            }
            catch (Exception)
            {
                return false;
            }

            return (response.StatusCode.ToString() == "NoContent");
        } 

        /// <summary>
        /// A helper method that can be used many times. Reset the Http client to make the formatting of requests easier to do.
        /// </summary>
        private static void CommonHelper()
        {
            sharedClient.DefaultRequestHeaders.Clear();
            sharedClient.DefaultRequestHeaders.UserAgent.Add(PRODUCT);
            sharedClient.DefaultRequestHeaders.Accept.Add(JSON_TYPE);
        }

        /// <summary>
        /// A helper method for authenticated requests that can be used many times.
        /// </summary>
        private static void CommonAuthenticatedHelper()
        {
            sharedClient.DefaultRequestHeaders.Clear();
            sharedClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            sharedClient.DefaultRequestHeaders.UserAgent.Add(PRODUCT);
            sharedClient.DefaultRequestHeaders.Accept.Add(GITHUB_TYPE);
        }

        /// <summary>
        /// Client ID getter.
        /// </summary>
        /// <returns>The client ID.</returns>
        private static string? GetClientID()
        {
            HttpResponseMessage response; 
            try
            {
                response = sharedClient.GetAsync($"{BACKEND_API}/client").Result;
            }
            catch (Exception)
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                JObject json = JObject.Parse(content);
                string id = json["clientid"].ToString();
                return id;
            }

            return null;
        }

        /// <summary>
        /// Sets http client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="url">The url for the client.</param>
        protected static void SetHttpClient(HttpClient client, string url)
        {
            sharedClient = client;
            sharedClient.BaseAddress = new Uri(url);
        }

        /// <summary>
        /// Sets test access code.
        /// </summary>
        protected static void SetTestAccessCode()
        {
            AccessToken = "123testaccess";
        }

        /// <summary>
        /// Sets test user code.
        /// </summary>
        protected static void SetUserCode()
        {
            UserCode = "123testuser";
        }

        /// <summary>
        /// Sets test username.
        /// </summary>
        protected static void SetUsername()
        {
            Username = "123testuser";
        }

        /// <summary>
        /// Resets class data.
        /// </summary>
        protected static void ResetData()
        {
            UserCode = AccessToken = null;
        }
      
    }
}