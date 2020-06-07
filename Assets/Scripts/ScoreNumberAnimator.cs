using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNumberAnimator : MonoBehaviour
{
    private TextMesh text;
    private float fadeStart = 0;

    private float fadeTime = 2f;
    public float movingUpSpeed = 40;
    public Color startColor = new Color(1, 1, 1, 1);
    public Color endColor = new Color(1, 1, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeStart < fadeTime)
        {
            // Fade out text
            fadeStart += Time.deltaTime * (fadeTime / 3);
            text.color = Color.Lerp(startColor, endColor, fadeStart);

            // Move text up
            transform.Translate(Vector3.up * Time.deltaTime * 2);
        } else
        {
            Destroy(this.gameObject); 
        }

    }
}
