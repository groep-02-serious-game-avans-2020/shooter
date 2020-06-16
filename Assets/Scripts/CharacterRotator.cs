using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotator : MonoBehaviour
{
    public GameObject character;
    public float rotationSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        character.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}
