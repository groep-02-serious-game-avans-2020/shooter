using Assets.Models;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;

public class QrScanner : MonoBehaviour
{
    private WebCamTexture camTexture;
    private Rect screenRect;
    private bool qrRecognised = false;

    public Text statusText;
    public Button confirmButton;

    // Start is called before the first frame update
    void Start()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        camTexture = new WebCamTexture();
        camTexture.requestedHeight = Screen.height;
        camTexture.requestedWidth = Screen.width;
        if (camTexture != null)
        {
            camTexture.Play();
        }
    }


    /// <summary>
    /// Render the camera view if the QR-Code is not yet recognised
    /// </summary>
    void OnGUI()
    {
        if (!qrRecognised)
        {
            // Draw the cameraview in the GUI
            GUI.DrawTexture(screenRect, camTexture, ScaleMode.ScaleToFit);

            try
            {
                // Decode the current frame
                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(camTexture.GetPixels32(), camTexture.width, camTexture.height);

                // If the frame recognises a QR-Code
                if (result != null)
                {
                    qrRecognised = true;
                    HandleQrCode(result.Text);
                }
            }
            catch (Exception ex) { Debug.LogWarning(ex.Message); }
        }
    }

    private void OnDestroy()
    {
        camTexture.Stop();
    }


    /// <summary>
    /// Handle the raw data coming from the scanned QR-Code
    /// </summary>
    /// <param name="qrCodeContent">Raw QR-Code content</param>
    void HandleQrCode(string qrCodeContent)
    {
        QrCodeContent qrCode = null;

        // Try to parse the QR-Code content
        try
        {
            qrCode = JsonUtility.FromJson<QrCodeContent>(qrCodeContent);
        }
        catch (System.ArgumentException e)
        {
            Debug.LogError(e);
        }

        if (qrCode == null)
        {
            // The Code is parsable to a Shurvey QR-Code, but the content is not recognised
            statusText.text = "QR-Code not recognised as a Shurvey QR-Code, try again.";
            Debug.Log("Not a Shurvey QR-Code");
        }
        else if (qrCode.type == "survey")
        {
            // The QR-Code represents a Shurvey QR-Code for a survey
            statusText.text = "QR-Code recognised, fetching survey...";
            Debug.Log("Fetching survey with id " + qrCode._id);
            SurveyModel survey = FetchSurvey(qrCode._id);
            if (survey != null)
            {
                if (UserManager.singleton.UserHasScannedQrCode(qrCode._id))
                {
                    statusText.text = "You have already answered this survey.";
                }
                else
                {
                    statusText.text = "Survey\n`" + survey.title + "`\nfound, start survey?";
                    confirmButton.interactable = true;
                }
            }
            else
            {
                statusText.text = "Error while fetching survey, please try again.";
                confirmButton.interactable = false;
            }
        }
        else if (qrCode.type == "cosmetic")
        {
            // The QR-Code represents a Shurvey QR-Code for a cosmetic item
            statusText.text = "QR-Code recognised, do you want to download the cosmetic?";
            confirmButton.interactable = true;
            Debug.Log("Fetching cosmetic with id " + qrCode._id);
        }
        else
        {
            // The QR-Code is not a Shurvey QR-Code
            Debug.Log("Invalid QR-Code");
            statusText.text = "QR-Code is invalid, try again.";
        }
    }

    /// <summary>
    /// Fetch the scanned survey from the back-end
    /// </summary>
    /// <param name="_id">Survey MongoDB _ID</param>
    public SurveyModel FetchSurvey(string _id)
    {
        SurveyModel survey = DataManager.singleton.GetSurvey(_id);
        return survey;
    }


    /// <summary>
    /// Fetch the scanned cosmetic from the back-end
    /// </summary>
    /// <param name="_id">Cosmetic MongoDB _ID</param>
    public void FetchCosmetic(string _id)
    {
        // TODO: fetch cosmetic from back-end
    }

    /// <summary>
    /// Method that gets executed when the exit button is pressed
    /// </summary>
    public void Exit()
    {
        SceneManager.LoadScene("main_menu");
    }

    /// <summary>
    /// Method that gets executed when the confirm button is pressed
    /// </summary>
    public void Confirm()
    {
        SceneManager.LoadScene("survey");
    }

    /// <summary>
    /// Method that gets executed when the cancel button is pressed
    /// </summary>
    public void Cancel()
    {
        qrRecognised = false;
    }

    /// <summary>
    /// Class representing the content of a Shurvey QR-Code
    /// </summary>
    [Serializable]
    public class QrCodeContent
    {
        public string type;
        public string _id;
    }
}
