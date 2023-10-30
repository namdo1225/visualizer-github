# Overall (for frontend developers):
- We are connecting to the GitHub API via GitHub's OAuth App feature.
- Nam registered our program as an OAuth App on GitHub.
- TEMP: Client ID and secret for the app are stored in the Github class for now. DON'T SHARE WITH ANYONE.

## Instructions:
- For the app to access a user's API, first call **GivePermission()**. **userCode** should have a value after it runs. Displays **userCode** to user.
- Calls **WaitForAuthorization()**. This function will simply wait for user to input **userCode** into https://github.com/login/device.
- NOTE: I will leave it to frontend to decide how they can prompt user to enter **userCode** to website.
- To test **WaitForAuthorization()** is successful, you can retrieve the user's repository list via: **GetRepositories()**.
- IF you want to delete a user token (might be useful for security purposes), call **DeleteToken()** when you no longer need the GitHub API.
- Please see **TestAsync()** for a simple example of how getting the GitHub user's repository list would look like.

## Implementation Specifics:
- All of these functions do not return anything useful.
- I set up members that you can access directly:
- Many of the methods are asynchronous, meaning you might have to use await or put my code inside a loop so you can retrieve data appropriately.
  I tend to create an infinite loop after calling my **TestAsync()** method so that the method has time to process the API.

## Members that you can get from class object:
- userCode: A String for user code. Displays this to user so they can go to https://github.com/login/device and enter it to give app's read/write repository permission.
- repos: A List of Repo class objects. A Repo object has 2 fields: String **name** and String **git_url**.
- username: A String for the Github username
- userGitHubURL: A String for the Github user profile page.

## API:
- CreateAuthenticatedGit(int i): returns a String URL for which you can use git clone on that URL. The parameter selects which element of **repos** you can to retrieve.
- CreateAuthenticatedGit(String url): returns a String URL for which you can use git clone on that URL. The parameter is the a url you want to make an authenticated URL out of.
url should be in the format "github.com/[user]/[repoName].git".

- None of the methods below should block. Hopefully, your frontend UI won't freeze due to the following methods:

- Github(): creates the Github object. Nothing in it for now.
- GivePermission(): Used along with WaitForAuthorization(). Starts the process to get user's permission to view/write to their repositories.
- WaitForAuthorization(): Used along with GivePermission(). Polls website for authorization from user.
- GetRepositories(): Retrieve user's repository list. See **repos** member.
- DeleteToken(): Deletes user access token, essentially disassociating user from the API.
- GetRepositories(): Retrieve user's repository into **repos** member variable.

## Pushing a local-only repository to GitHub:
- CreateRepo(String name): Creates a public repository with the specified name on the user's GitHub page. IMPORTANT: use await when calling method to retrieve git clone's url.
In your local repository's folder, use "git remote add origin REMOTE-URL" where REMOTE-URL can just be the clone url returned by CreateRepo().
Then, use "git remote -v" to verify it's correct (optional). Then push via "git push origin main".

## Working with storing credentials/Restoring state:
- SaveUser(): Call this if user wants to save their access token and username into Windows Credential Manager. You do NOT provide any argument. Please ensure
accessToken and username are not null. Return true if saving is successful.
- ReadTokenAndUserName(): Read username and token from Windows Credential Manager. As such, be wary when mixing this call with SaveUser(). It sets accessToken
and username. Return true if accessToken and username are not null.
- DeleteStoredCredential(): Deletes credential from Windows Credential Manager. Return true if credential is deleted. This method is automatically called inside
DeleteToken().