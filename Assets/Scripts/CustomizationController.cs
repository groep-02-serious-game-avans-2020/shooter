using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    /*    public Button scanQrCodeButton; userDisplayNameText.text = userManager.GetUserDisplayName();*/
    public Button backToMenuButton;
    public AudioClip buttonClickSound;
    public GameObject colorPrefab;

    public Material[] playerColors;
    public Material[] eyeColors;
    public Material[] shirtColors;
    public Material[] pantsColors;
    public Material[] shoesColors;

    private AudioSource UiAudioSource;
    private int selectedSlot;
    private Transform grid;
    private CharacterCosmeticsController preview;

    // Start is called before the first frame update
    void Start()
    {
        UiAudioSource = GetComponent<AudioSource>();
        grid = GameObject.Find("ColorScrollRect").transform;
        preview = GameObject.Find("Character").GetComponent<CharacterCosmeticsController>();
        var userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerColor()
    {
        selectedSlot = 1;
        DisplayColors(playerColors);
    }

    public void SetEyeColor()
    {
        selectedSlot = 2;
        DisplayColors(eyeColors);
    }

    public void SetShirtColor()
    {
        selectedSlot = 3;
        DisplayColors(shirtColors);
    }

    public void SetPantsColor()
    {
        selectedSlot = 4;
        DisplayColors(pantsColors);
    }

    public void SetShoesColor()
    {
        selectedSlot = 5;
        DisplayColors(shoesColors);
    }

    private void DisplayColors(Material[] materials)
    {
        //Create a new list of buttons based on user selection.
        float previousPosition = -95f;
        foreach (Material mat in materials)
        {
            GameObject newColorPrefab = Instantiate(colorPrefab) as GameObject;
            ColorController controller = newColorPrefab.GetComponent<ColorController>();
            controller.material = mat;
            newColorPrefab.transform.localPosition = new Vector2(previousPosition, 0f);
            previousPosition += 55;

            Button button = newColorPrefab.GetComponent<Button>();
            button.onClick.AddListener(() => { CharacterSingleton.singleton.SetMaterial(selectedSlot, controller.material); preview.SetMaterial(selectedSlot, controller.material); });
            newColorPrefab.transform.SetParent(grid, false);

        }
    }

/*    public void ScanQrCode()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("qr_scanner");
    }*/

    public void BackToMenu()
    {
        //Send selected customizatons to the server
        PlayButtonClickSound();
        SceneManager.LoadScene("main_menu");
    }

    private void PlayButtonClickSound()
    {
        UiAudioSource.PlayOneShot(buttonClickSound);
    }
}
