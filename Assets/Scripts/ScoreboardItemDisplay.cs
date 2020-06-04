using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ScoreboardItemDisplay : MonoBehaviour
{
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textPosition;
    public TextMeshProUGUI textScore;

    public ScoreboardItem item;

    // Start is called before the first frame update
    void Start()
    {
        if(item != null)
        {
            Prime(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime(ScoreboardItem item)
    {
        this.item = item;
        if (textName != null)
        {
            textName.text = item.userName;
        }
        if(textPosition != null)
        {
            textPosition.text = item.position.ToString();
        }
        if(textScore != null)
        {
            textScore.text = item.score.ToString();
        }
    }
}
