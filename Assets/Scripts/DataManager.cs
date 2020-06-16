using Assets.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Singleton class for fetching and storing game related data from and to the server
/// </summary>
public class DataManager : MonoBehaviour
{
    public static DataManager singleton;
    public SurveyModel currentSurvey;

    private HttpWebRequest request;

    //public string apiUrl = "http://localhost:3000/api/";
    public string apiUrl = "https://shurvey-server.herokuapp.com/api/";
    public string surveyUrl = "survey/";
    public string userUrl = "user/";
    public string answerUrl = "answer/";

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

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        CheckIfUserIsSignedIn();
    }

    /// <summary>
    /// Checks if a user is signed in and if the token is still valid
    /// </summary>
    private void CheckIfUserIsSignedIn()
    {
        Debug.Log("Game started, checking if user is logged in...", this);
        if (UserIsSignedIn())
        {
            Debug.Log("User is logged in, checking if token is still valid...", this);

            UserManager userManager = new UserManager();

            if (userManager.tokenIsValid())
            {
                Debug.Log("Token is still valid, fetching user data from server...", this);
                userManager.fetchLoggedinUserData(PlayerPrefs.GetString("token"), PlayerPrefs.GetString("userId"));

                gameObject.AddComponent<UserManager>();

                Debug.Log("Fetched user data from server, redirecting to main menu...", this);
                SceneManager.LoadScene("main_menu");
            }
            else
            {
                Debug.Log("Token is expired, redirecting to login scene...", this);

                // Delete the saved user data
                PlayerPrefs.DeleteAll();
                PlayerPrefs.Save();

                SceneManager.LoadScene("login");
            }
        }
        else
        {
            Debug.Log("No user logged in, loading login scene...", this);
            SceneManager.LoadScene("login");
        }
    }

    /// <summary>
    /// Tries to fetch a survey from the server
    /// </summary>
    /// <param name="_id">Survey MongoDB _id</param>
    /// <returns></returns>
    public SurveyModel GetSurvey(string _id)
    {
        Debug.Log("Attempting to fetch survey with _id " + _id + " from server", this);

        request = (HttpWebRequest)WebRequest.Create(apiUrl + surveyUrl + _id);
        request.ContentType = "application/json";
        request.Method = "GET";

        var response = (HttpWebResponse)request.GetResponse();

        string result;
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }

        if (response.StatusCode == HttpStatusCode.OK)
        {
            SurveyModel survey = JsonUtility.FromJson<SurveyModel>(result);
            Debug.Log("Survey `" + survey.title + "` fetched successfully", this);
            Debug.Log(result);
            this.currentSurvey = survey;
            return survey;
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Debug.Log("Survey does not exist");
            return null;
        }
        else
        {
            // Other error
            Debug.Log("Login failed: other error", this);
        }

        return null;
    }

    /// <summary>
    /// Tries to send the answers to the server, returns false if failed
    /// </summary>
    /// <param name="answers">List of answers</param>
    /// <param name="_id">Survey MongoDB _id</param>
    /// <returns></returns>
    public bool SubmitAnswers(List<AnswerModel> answers, string _id, int score)
    {
        Debug.Log("Attempting to send answers to backend...", this);
        request = (HttpWebRequest)WebRequest.Create(apiUrl + surveyUrl + _id);
        request.ContentType = "application/json";
        request.Method = "PUT";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{\"answers\":[";

            for (int i = 0; i < answers.Count; i++)
            {
                if (i < answers.Count - 1)
                {
                    json += JsonUtility.ToJson(answers[i]) + ',';

                }
                else
                {
                    json += JsonUtility.ToJson(answers[i]);
                }
            }

            json += "],\"userId\":\"" + UserManager.singleton.GetUserId() + "\",";
            json += "\"userName\":\"" + UserManager.singleton.GetUserDisplayName() + "\",";
            json += "\"highScore\":" + score + "}";

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
            Debug.Log("Sumbit successful, redirecting to main menu", this);
            Debug.Log(result);
            GameManager.singleton.highScores = JsonUtility.FromJson<HighScoreList>("{\"highScores\":" + result + "}");
            return true;
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            // User not found
            Debug.Log("Login failed: user not found", this);
            return false;
        }
        return false;
    }

    /// <summary>
    /// Checks if a user is signed in
    /// </summary>
    private bool UserIsSignedIn()
    {
        if (PlayerPrefs.HasKey("token"))
        {
            return true;
        }
        return false;
    }
}
