using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardItem
{


    public string userName { get; set; }
    public int position { get; set; }
    public int score { get; set; }

        public override string ToString()
        {
            return $"\"name\": \"{userName}\", \"score\": {score}";
        }
    
}
