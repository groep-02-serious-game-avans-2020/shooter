using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button scanQrCodeButton;
    public Button customiseCharacterButton;
    public Button quitGameButton;
    public Text userDisplayNameText;
    public AudioClip buttonClickSound;

    private AudioSource UiAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        UiAudioSource = GetComponent<AudioSource>();
        var userManager = GameObject.Find("UserManager").GetComponent<UserManager>();
        userDisplayNameText.text = userManager.getUserDisplayName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScanQrCode()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("qr_scanner");
    }

    public void CustomiseCharacter()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene("character_customizer");
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
    }

    private void PlayButtonClickSound()
    {
        UiAudioSource.PlayOneShot(buttonClickSound);
    }
}
