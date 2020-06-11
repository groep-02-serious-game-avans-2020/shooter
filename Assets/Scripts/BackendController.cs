using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class BackendController : MonoBehaviour
{
    public static string apiUrl = "http://localhost:3000/api/";
    //public static string apiUrl = "https://shurvey-server.herokuapp.com/api/";
    public static string surveyUrl = "survey/";
    public static string userUrl = "user/";
    public static string cosmeticsUrl = "cosmetics/";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SurveyModel getSurveyFromBackend(string surveyId)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl + surveyUrl + surveyId);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        SurveyModel survey = JsonUtility.FromJson<SurveyModel>(jsonResponse);
        return survey;
    }
}
