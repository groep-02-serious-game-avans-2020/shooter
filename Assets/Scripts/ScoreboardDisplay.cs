using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardDisplay : MonoBehaviour
{
    public Transform targetTransform;
    public ScoreboardItemDisplay itemDisplayPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime(List<ScoreboardItem> items)
    {
        foreach(ScoreboardItem item in items)
        {
            ScoreboardItemDisplay display = (ScoreboardItemDisplay)Instantiate(itemDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Prime(item);
        }
    }
}
