using GithubSpace;
using Moq;
using Moq.Protected;
using System.Text;
using System.Net.Http.Headers;

namespace GithubTestSpace
{
    /// <summary>
    /// The Github class test suite.
    /// </summary>
    [TestClass]
    public class GithubTest : Github
    {
        private static Github _github = new();
        private static readonly MediaTypeWithQualityHeaderValue jsonType = new("application/json");
        private static readonly string mockAccessToken = "ghu_TEST";

        /// <summary>
        /// Mocks the user code and send API request to retrieve the user code.
        /// </summary>
        private static void MockUserCode()
        {
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                device_code = "123-345",
                user_code = "FEUK-EVMK",
                vertification_uri = "https://github.com/login/device",
                expires_in = "900",
                interval = "5"
            }),
            Encoding.UTF8,
            jsonType);

            string url = "https://github.com/login/device/code";

            HttpResponseMessage httpResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = json
            };

            Mock<HttpMessageHandler> mockHandler = new();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post && r.RequestUri.ToString().StartsWith(url)),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            HttpClient httpClient = new(mockHandler.Object);
            _github = new(httpClient, url);

            //act
            Task task = Task.Run(() => GivePermission());
            task.Wait();
        }

        /// <summary>
        /// Mocks the access code and send API request to retrieve the access code.
        /// </summary>
        private static void MockAccessCode()
        {
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                access_token = mockAccessToken,
                scope = "",
                token_type = "bearer"
            }),
            Encoding.UTF8,
            jsonType);

            string url = "https://github.com/login/oauth/access_token";

            HttpResponseMessage httpResponse = new()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = json
            };

            Mock<HttpMessageHandler> mockHandler = new();

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Post && r.RequestUri.ToString().StartsWith(url)),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            HttpClient httpClient = new(mockHandler.Object);
            SetHttpClient(httpClient, url);

            //act
            Task task = Task.Run(Github.WaitForAuthorization);
            task.Wait();
        }

        /// <summary>
        /// Sets mock web handler.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <param name="urlString">The url string.</param>
        /// <param name="code">The HTTP status code.</param>
        /// <param name="method">The HTTP method.</param>
        private static void SetMockWebHandler(StringContent jsonObject, string urlString, System.Net.HttpStatusCode code, HttpMethod method)
        {
            HttpResponseMessage httpResponse = new()
            {
                StatusCode = code,
                Content = jsonObject
            };

            Mock<HttpMessageHandler> mockHandler = new();

            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == method && r.RequestUri.ToString().StartsWith(urlString)),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            HttpClient httpClient = new(mockHandler.Object);
            SetHttpClient(httpClient, urlString);
        }

        /// <summary>
        /// Gives permission, get user code, test01.
        /// </summary>
        [TestMethod]
        public void GivePermission_GetUserCode_Test01()
        {
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                interval = 5,
                device_code = "340234",
                user_code = "GJE123"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://github.com/login/device/code", System.Net.HttpStatusCode.OK, HttpMethod.Post);

            //act
            Task task = Task.Run(() => GivePermission("public"));
            task.Wait();
            Equals(UserCode, "GJE123");
        }

        /// <summary>
        /// Give permission, fail generation code, test02.
        /// </summary>
        [TestMethod]
        public void GivePermission_FailGenerationCode_Test02()
        {
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "error"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://github.com/login/device/code", System.Net.HttpStatusCode.BadRequest, HttpMethod.Post);

            //act
            Task task = Task.Run(() => GivePermission("public"));
            task.Wait();
            Equals(UserCode, null);
        }

        /// <summary>
        /// Waits for authorization, retrieves access token, test01.
        /// </summary>
        [TestMethod]
        public void WaitForAuthorization_RetrieveAccessToken_Test01()
        {
            MockUserCode();
            MockAccessCode();
            Equals(mockAccessToken, AccessToken);
        }

        /// <summary>
        /// Waits for authorization, access denied, test02.
        /// </summary>
        [TestMethod]
        public void WaitForAuthorization_AccessDenied_Test02()
        {
            ResetData();
            SetUserCode();

            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "access_denied"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://github.com/login/oauth/access_token", System.Net.HttpStatusCode.OK, HttpMethod.Post);

            //act
            Task task = Task.Run(WaitForAuthorization);
            task.Wait();
            Assert.AreEqual(null, AccessToken);
        }

        /// <summary>
        /// Waits for authorization, timeout 1, test03.
        /// </summary>
        [TestMethod]
        public void WaitForAuthorization_Timeout01_Test03()
        {
            ResetData();
            SetUserCode();

            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                error = "timeout"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://github.com/login/oauth/access_token", System.Net.HttpStatusCode.OK, HttpMethod.Post);

            //act
            Task task = Task.Run(WaitForAuthorization);
            task.Wait();
            Assert.AreEqual(null, AccessToken);
        }

        /// <summary>
        /// Waits for authorization, timeout 2, test04.
        /// </summary>
        [TestMethod]
        public void WaitForAuthorization_Timeout02_Test04()
        {
            ResetData();
            SetUserCode();

            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                waiting = "waiting for authorization"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://github.com/login/oauth/access_token", System.Net.HttpStatusCode.BadRequest, HttpMethod.Post);

            //act
            Task task = Task.Run(WaitForAuthorization);
            task.Wait();
            Assert.AreEqual(null, AccessToken);
        }

        /// <summary>
        /// Waits for authorization, no user code, test05.
        /// </summary>
        [TestMethod]
        public void WaitForAuthorization_NoUserCode_Test05()
        {
            Github.ResetData();
            Task task = Task.Run(WaitForAuthorization);
            Assert.AreEqual(null, Github.AccessToken);
            task.Wait();
        }

        /// <summary>
        /// Get user info, get username, test01.
        /// </summary>
        [TestMethod]
        public void GetUserInfo_GetUsername_Test01()
        {
            SetTestAccessCode();

            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                login = "octocat",
                id = 1,
                node_id = "MDQ6VXNlcjE=",
                avatar_url = "https://github.com/images/error/octocat_happy.gif",
                gravatar_id = "none",
                url = "https://api.github.com/users/octocat",
                html_url = "https://github.com/octocat",
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/user", System.Net.HttpStatusCode.OK, HttpMethod.Get);

            //act
            Task task = Task.Run(GetUserInfo);
            task.Wait();

            Equals("octocat", Username);
            Equals("https://github.com/images/error/octocat_happy.gif", AvatarURL);
            Equals("https://github.com/octocat", UserGitHubURL);
        }

        /// <summary>
        /// Get user info, no access token, test02.
        /// </summary>
        [TestMethod]
        public void GetUserInfo_NoAccessToken_Test02()
        {
            ResetData();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                login = "octocat",
                id = 1,
                node_id = "MDQ6VXNlcjE=",
                avatar_url = "https://github.com/images/error/octocat_happy.gif",
                gravatar_id = "none",
                url = "https://api.github.com/users/octocat",
                html_url = "https://github.com/octocat",
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/user", System.Net.HttpStatusCode.OK, HttpMethod.Get);

            //act
            Task task = Task.Run(GetUserInfo);
            task.Wait();

            StringAssert.Equals(null, Username);
        }

        /// <summary>
        /// Get user info, API error, test03.
        /// </summary>
        [TestMethod]
        public void GetUserInfo_APIError_Test03()
        {
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/user", System.Net.HttpStatusCode.BadRequest, HttpMethod.Get);

            //act
            Task task = Task.Run(GetUserInfo);
            task.Wait();

            StringAssert.Equals(null, Username);
        }

        /// <summary>
        /// Deletes token, deletion is valid, test01.
        /// </summary>
        [TestMethod]
        public void DeleteToken_DeleteValid_Test01()
        {
            Github.SetTestAccessCode();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/applications/", System.Net.HttpStatusCode.NoContent, HttpMethod.Delete);

            //act
            Task task = Task.Run(DeleteToken);
            task.Wait();
            StringAssert.Equals(null, Github.AccessToken);
        }

        /// <summary>
        /// Deletes token, deletion is invalid 01, test02.
        /// </summary>
        [TestMethod]
        public void DeleteToken_DeleteInvalid01_Test02()
        {
            Github.SetTestAccessCode();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/applications/", System.Net.HttpStatusCode.BadRequest, HttpMethod.Delete);

            //act
            bool deleted = DeleteToken();
            Assert.IsTrue(!deleted);
        }

        /// <summary>
        /// Deletes token, deletion is invalid 02, test03.
        /// </summary>
        [TestMethod]
        public void DeleteToken_DeleteInvalid02_Test03()
        {
            SetTestAccessCode();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/applications/", System.Net.HttpStatusCode.NotModified, HttpMethod.Delete);

            //act
            bool deleted = DeleteToken();
            Assert.IsTrue(!deleted);
        }

        /// <summary>
        /// Deletes token,no access token, test04.
        /// </summary>
        [TestMethod]
        public void DeleteToken_NoAccessToken_Test04()
        {
            ResetData();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/applications/", System.Net.HttpStatusCode.PartialContent, HttpMethod.Delete);

            //act
            bool deleted = DeleteToken();
            Assert.IsTrue(!deleted);
        }

        /// <summary>
        /// Creates authenticated git string, valid string, test01.
        /// </summary>
        [TestMethod]
        public void CreateAuthenticatedGit_str_Valid_Test01()
        {
            SetTestAccessCode();
            Assert.AreEqual($"https://{AccessToken}@test", CreateAuthenticatedGit("test"));
        }

        /// <summary>
        /// Creates authenticated git string, no access token, test02.
        /// </summary>
        [TestMethod]
        public void CreateAuthenticatedGit_str_NoAccessToken_Test02()
        {
            ResetData();
            Assert.AreEqual(null, CreateAuthenticatedGit("test"));
        }

        /// <summary>
        /// Creates repository, no access token, test01.
        /// </summary>
        [TestMethod]
        public void CreateRepository_NoAccessToken_Test01()
        {
            ResetData();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/applications/", System.Net.HttpStatusCode.Created, HttpMethod.Post);

            //act
            String? result = Task.Run(() => CreateRepo("test")).Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Creates repository, valid, test02.
        /// </summary>
        [TestMethod]
        public void CreateRepository_Valid_Test02()
        {
            SetTestAccessCode();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
                clone_url = "https://github.com/octocat/test.git"
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/user/repos", System.Net.HttpStatusCode.Created, HttpMethod.Post);

            //act
            String? result = Task.Run(() => CreateRepo("test")).Result;
            Assert.AreEqual("github.com/octocat/test.git", result);
        }

        /// <summary>
        /// Creates repository, bad API response, test03.
        /// </summary>
        [TestMethod]
        public void CreateRepository_BadAPIResponse_Test03()
        {
            SetTestAccessCode();
            using StringContent json = new(
            System.Text.Json.JsonSerializer.Serialize(new
            {
            }),
            Encoding.UTF8,
            jsonType);

            SetMockWebHandler(json, "https://api.github.com/user/repos", System.Net.HttpStatusCode.BadRequest, HttpMethod.Post);

            //act
            string? result = Task.Run(() => CreateRepo("test")).Result;
            Assert.AreEqual(null, result);
        }

        /// <summary>
        /// Credential, set and get and delete, test01.
        /// </summary>
        [TestMethod]
        public void Credential_SetGetDelete_Test01()
        {
            ResetData();
            SetUsername();
            SetTestAccessCode();

            string? tempAccessToken = AccessToken;
            string? tempUsername = Username;

            Assert.AreEqual(true, SaveUser());
            Assert.AreEqual(true, LoadStoredCredentials());
            Assert.AreEqual(tempUsername, Username);
            Assert.AreEqual(tempAccessToken, AccessToken);
            Assert.AreEqual(true, DeleteStoredCredential());

        }

        /// <summary>
        /// Credential, credentials already exist, test02.
        /// </summary>
        [TestMethod]
        public void Credential_AlreadyExist_Test02()
        {
            ResetData();
            SetUsername();
            SetTestAccessCode();

            Assert.AreEqual(true, SaveUser());
            Assert.AreEqual(false, SaveUser());
            Assert.AreEqual(true, DeleteStoredCredential());
        }

        /// <summary>
        /// Credential, retrieves nonexisting credential, test03.
        /// </summary>
        [TestMethod]
        public void Credential_RetrieveNothing_Test03()
        {
            ResetData();
            SetUsername();
            SetTestAccessCode();

            DeleteStoredCredential();
            Assert.AreEqual(false, LoadStoredCredentials());
        }

        /// <summary>
        /// Credential, credential already deleted, test04.
        /// </summary>
        [TestMethod]
        public void Credential_AlreadyDeleted_Test04()
        {
            ResetData();
            SetUsername();
            SetTestAccessCode();

            DeleteStoredCredential();
            Assert.AreEqual(false, DeleteStoredCredential());
        }

        /// <summary>
        /// Credential, mo credentials to store, test04.
        /// </summary>
        [TestMethod]
        public void Credential_NothingToSave_Test04()
        {
            ResetData();
            Assert.AreEqual(false, SaveUser());
        }
    }
}
