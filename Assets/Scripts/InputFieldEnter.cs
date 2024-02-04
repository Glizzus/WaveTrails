using UnityEngine;
using TMPro;
using WaveTrails.Utilities;

public class InputFieldEnter: MonoBehaviour
{
    public TMP_InputField field;
    // Start is called before the first frame update

    public void OnEnterPressed()
    {
        var inputText = field.text;
        try
        {
            YouTubeDownload.DownloadFile(inputText, ".");
            Debug.Log("Successfully downloaded file somewhere");
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error doing download: {e}");
        }
    }
}
