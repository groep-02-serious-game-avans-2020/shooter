using Assets.Models;
using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Singleton class for fetching, storing and authorising user related data
/// </summary>
public class UserManager : MonoBehaviour
{
    private static UserManager singleton;
    private UserModel user;
    private HttpWebRequest request;

    public InputField emailInput;
    public InputField passwordInput;
    public Text errorText;

    private void Awake()
    {
        if (singleton)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sign in a user using the input fields on the login screen
    /// </summary>
    public void SignIn()
    {
        Debug.Log("Attempting to sign in user...", this);
        request = (HttpWebRequest)WebRequest.Create(BackendController.apiUrl + BackendController.userUrl + "login");
        request.ContentType = "application/json";
        request.Method = "POST";

        LoginCredentials credentials = new LoginCredentials(emailInput.text.ToString(), passwordInput.text.ToString());

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(credentials);
            streamWriter.Write(json);
        }

        var response = (HttpWebResponse)request.GetResponse();

        string result;
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log("Login successful, redirecting to main menu", this);
            this.user = JsonUtility.FromJson<UserModel>(result.ToString());

            // Write the user credentials to the PlayerPrefs to save login
            PlayerPrefs.SetString("auth", user.auth.ToString());
            PlayerPrefs.SetString("token", user.token);
            PlayerPrefs.SetString("userId", user.userid);
            PlayerPrefs.Save();

            SceneManager.LoadScene("main_menu");
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // User not found
            Debug.Log("Login failed: user not found", this);
            errorText.text = "User not found!";
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Login invalid
            Debug.Log("Login failed: invalid credentials", this);
            errorText.text = "Invalid credentials!";
        }
        else
        {
            // Other error
            Debug.Log("Login failed: other error", this);
            errorText.text = response.ToString();
        }
    }

    /// <summary>
    /// Fetch the user data using the token and id saved in the PlayerPrefs
    /// </summary>
    public UserModel fetchLoggedinUserData(string token, string userId)
    {
        Debug.Log("Fetching the user data for the logged in user...", this);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BackendController.apiUrl + BackendController.userUrl + userId);
        request.ContentType = "application/json";
        request.Method = "GET";

        FetchCredentials credentials = new FetchCredentials(token, userId);

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = JsonUtility.ToJson(credentials);
            streamWriter.Write(json);
        }

        var response = (HttpWebResponse)request.GetResponse();

        string result;
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Debug.Log("User data found", this);

            this.user = JsonUtility.FromJson<UserModel>(result.ToString());

            // Write the user credentials to the PlayerPrefs to save login
            PlayerPrefs.SetString("auth", user.auth.ToString());
            PlayerPrefs.SetString("token", user.token);
            PlayerPrefs.SetString("userId", user.userid);
            PlayerPrefs.Save();

            return user;
        }
        else
        {
            Debug.Log("Error while fetching user data, redirecting to login scene...", this);

            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();

            SceneManager.LoadScene("login");
        }
        return null;
    }

    /// <summary>
    /// Check if the saved token is still valid
    /// </summary>
    public bool tokenIsValid()
    {
        Debug.Log("Checking if the saved token is still valid...", this);
        // TODO: Check if token is still valid in backend
        return false;
    }

    public string getUserDisplayName()
    {
        return user.displayName;
    }

    public class LoginCredentials
    {
        public string email;
        public string password;

        public LoginCredentials(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }

    public class FetchCredentials
    {
        public string token;
        public string userId;

        public FetchCredentials(string token, string userId)
        {
            this.token = token;
            this.userId = userId;
        }
    }
}
