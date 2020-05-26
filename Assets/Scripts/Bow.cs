using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    float _charge;

    public float chargeMax;
    public float chargeRate;

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
        if(Input.GetKey(fireButton) && _charge < chargeMax)
        {
            _charge += Time.deltaTime * chargeRate;
        }

        if(Input.GetKeyUp(fireButton))
        {
            Rigidbody arrow = Instantiate(arrowPrefab, arrowSpawn.transform.position, arrowSpawn.rotation) as Rigidbody;
            arrow.AddForce(arrowSpawn.forward * _charge, ForceMode.Impulse);
            _charge = 0;
        }
    }
}
