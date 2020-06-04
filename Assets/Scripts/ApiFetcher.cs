using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System;
using System.Text;
using System.Diagnostics;
using UnityEngine.UI;
using System.Collections.Specialized;

public class ApiFetcher : MonoBehaviour {
    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    static readonly HttpClient client = new HttpClient();
    public GameObject hsEntryPrefab; // gameObject in Hierarchy
    Text ourComponent;           // Our reference to text component

    // Start is called before the first frame update
    void Start() {
        GetExample();
/*
        // Find gameObject with name "MyText"
        myTextgameObject = GameObject.Find("HSEntry");
        // Get component Text from that gameObject
        ourComponent = myTextgameObject.GetComponent<Text>();
        // Assign new string to "Text" field in that component
        ourComponent.text = "Hey, I'm some randoms score !!";*/
    }

    // Update is called once per frame
    void Update() { }

   //example model, with api-ready ToString method
   [Serializable]
    class ScoreEntry {
        public string name;
        public int score;
       //public string _id {get; set;}

       public override string ToString()
        {
            return $"\"name\": \"{name}\", \"score\": {score}";
        }
         // var entry = new ScoreEntry();
         // entry.name = "Test-entry";
         // entry.price = 10;
         // Debug.Log(entry);
    }

    [Serializable]
    class ScoreList {
        public ScoreEntry[] scores;
    }

    static async void PostExample() {
      try {
         var body = new StringContent("{\"name\":\"John Doe\",\"score\":33}", Encoding.UTF8, "application/json");
         var response = await client.PostAsync("http://localhost:3002/api/products/", body);
         string result = response.Content.ReadAsStringAsync().Result;
            UnityEngine.Debug.Log(result);
      }
      catch(HttpRequestException e) {
            UnityEngine.Debug.Log("\nException Caught!");
            UnityEngine.Debug.Log(e);
      }
   }

   async void GetExample() {
      // Call asynchronous network methods in a try/catch block to handle exceptions.
      try {
         string responseBody = await client.GetStringAsync("http://localhost:3002/api/scores/");
            UnityEngine.Debug.Log(responseBody);
            ScoreList scorelist = JsonUtility.FromJson<ScoreList>("{\"scores\":" + responseBody + "}");
            UnityEngine.Debug.Log(scorelist.scores[0]);
            prefabCreation(scorelist);
        }
      catch(HttpRequestException e) {
            UnityEngine.Debug.Log("\nException Caught!");	
      }
   }

    private void prefabCreation(ScoreList list)
    {
        Transform grid = GameObject.Find("FirstChildPanel").transform;

        for (int i = 0; i < list.scores.Length; i++)
        {
            ScoreboardItem tempitem = new ScoreboardItem();
            tempitem.userName = list.scores[i].name;
            tempitem.position = (i + 1);
            tempitem.score = list.scores[i].score;
            /*Instantiate(prepfabPool[i]);*/
            GameObject newHSEntry = Instantiate(hsEntryPrefab) as GameObject;
            newHSEntry.GetComponent<ScoreboardItemDisplay>().item = tempitem;

            /*  newHSEntry.GetComponent<Text>().text = (i + 1) + ": " + list.scores[i].name + " -> " + list.scores[i].score;*/
/*            newHSEntry.transform.localScale = new Vector3(1, 1, 1);
*/            newHSEntry.transform.SetParent(grid, false);

        }
    }
}