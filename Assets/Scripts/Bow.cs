using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    public float chargeMax;
    public float chargeRate;
    public Slider chargeSlider;
    public AudioSource bowAudioSource;
    public AudioClip bowLoad;
    public AudioClip bowShoot;

    public KeyCode fireButton;

    public Transform arrowSpawn;
    public Rigidbody arrowPrefab;

    private float _charge;
    private Ray ray;
    private RaycastHit raycastHit;
    private bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            // Shoot a ray from the center of the main camera
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            // If the ray hit's something, rotate the arrow spawnpoint towards it
            if (Physics.Raycast(ray, out raycastHit))
            {
                arrowSpawn.transform.LookAt(raycastHit.point);
            }

            chargeSlider.value = _charge;

            // Play bow load sound effect
            if (Input.GetKeyDown(fireButton))
            {
                bowAudioSource.PlayOneShot(bowLoad);
            }

            // Increase the charge until it reaches the max set value
            if (Input.GetKey(fireButton) && _charge < chargeMax)
            {
                _charge += Time.deltaTime * chargeRate;
            }

            // Spawn and shoot the arrow once the fire button is released
            if (Input.GetKeyUp(fireButton))
            {
                // Play the shoot sound effect
                bowAudioSource.PlayOneShot(bowShoot);

                // Spawn arrow
                Rigidbody arrow = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as Rigidbody;

                // Give arrow forward force
                arrow.AddForce(arrowSpawn.forward * _charge, ForceMode.Impulse);

                // Reset charge
                _charge = 0;
            }
        }
    }

    public void SetCanShoot(bool canShoot)
    {
        this.canShoot = canShoot;
    }
}
