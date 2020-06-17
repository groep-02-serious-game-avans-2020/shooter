using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{

    public Material material;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Material SelectColor()
    {
        return material;
    }
}
