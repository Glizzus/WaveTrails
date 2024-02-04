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
            YouTubeDownload.DownloadFile(inputText, "Assets/Downloaded/Video/Video.mp4", "Assets/Downloaded/Audio/Audio.mp3");
            Debug.Log("Successfully downloaded file somewhere");
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error downloading: {e}");
        }
    }
}
