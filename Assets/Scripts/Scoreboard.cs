using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;

public class Scoreboard : MonoBehaviour
{
    static readonly HttpClient client = new HttpClient();
    public GameObject scoreboard;
    public GameObject highscoreEntry;
    private bool toggle;
    private ScoreList scorelist;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard.SetActive(false);
        GetScores(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (toggle)
            {
                var clones = GameObject.FindGameObjectsWithTag("highscore_entry");
                foreach (var clone in clones)
                {
                    Destroy(clone);
                }
                scoreboard.SetActive(false);
                toggle = false;
            }
            else
            {
                scoreboard.SetActive(true);
                PrefabCreation(scorelist);
                toggle = true;
            }
        }
    }

    async void GetScores()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            string responseBody = await client.GetStringAsync("http://localhost:3002/api/scores/");
            UnityEngine.Debug.Log(responseBody);
            scorelist = JsonUtility.FromJson<ScoreList>("{\"scores\":" + responseBody + "}");
        }
        catch (HttpRequestException e)
        {
            UnityEngine.Debug.Log("\nException Caught!");
            UnityEngine.Debug.Log(e);
        }
    }

    private void PrefabCreation(ScoreList list)
    {       
        Transform grid = GameObject.Find("FirstChildPanel").transform;

        for (int i = 0; i < list.scores.Length; i++)
        {
            ScoreboardItem tempitem = new ScoreboardItem();
            tempitem.userName = list.scores[i].name;
            tempitem.position = (i + 1);
            tempitem.score = list.scores[i].score;

            GameObject newHSEntry = Instantiate(highscoreEntry) as GameObject;
            newHSEntry.GetComponent<ScoreboardItemDisplay>().item = tempitem;
            newHSEntry.transform.SetParent(grid, false);
        }
    }

    //example model, with api-ready ToString method
    [Serializable]
    class ScoreEntry
    {
        public string name;
        public int score;

        public override string ToString()
        {
            return $"\"name\": \"{name}\", \"score\": {score}";
        }
    }

    [Serializable]
    class ScoreList
    {
        public ScoreEntry[] scores;
    }
}
