using System;
using System.Collections.Generic;

namespace Assets.Models
{
    [Serializable]
    public class HighScore
    {
        public string _id;
        public string userName;
        public int score;
    }

    [Serializable]
    public class HighScoreList
    {
        public HighScore[] highScores;
    }
}
