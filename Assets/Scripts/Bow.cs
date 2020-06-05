using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    float _charge;

    public float chargeMax;
    public float chargeRate;
    public Slider chargeSlider;

    public KeyCode fireButton;

    public Transform arrowSpawn;
    public Rigidbody arrowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chargeSlider.value = _charge;

        // Increase the charge until it reaches the max set value
        if(Input.GetKey(fireButton) && _charge < chargeMax)
        {
            _charge += Time.deltaTime * chargeRate;
        }

        // Spawn and shoot the arrow once the fire button is released
        if(Input.GetKeyUp(fireButton))
        {
            // Spawn arrow
            Rigidbody arrow = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as Rigidbody;

            // Give arrow forward force
            arrow.AddForce(arrowSpawn.forward * _charge, ForceMode.Impulse);

            // Reset charge
            _charge = 0;
        }
    }
}
