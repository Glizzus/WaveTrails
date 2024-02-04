using UnityEngine;
using TMPro;
using WaveTrails.Utilities;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class InputFieldEnter: MonoBehaviour
{
    public TMP_InputField field;
    public AudioSource audioSource;
    // Start is called before the first frame update

    public void OnEnterPressed()
    {
        var inputText = field.text;
        try
        {
            const string mp3Dest = "Assets/Downloaded/Audio/Audio.mp3";
            YouTubeDownload.DownloadFile(inputText, "Assets/Downloaded/Video/Video.mp4", mp3Dest);
            var fullDest = Path.GetFullPath(mp3Dest);
            Debug.Log("Successfully downloaded file somewhere");
            StartCoroutine(PlayAudio(fullDest));    
        }
        catch (System.Exception e)
        {
            Debug.Log($"Error downloading: {e}");
        }
    }

    private IEnumerator PlayAudio(string mp3Dest)
    {
        var audioUrl = $"file://{mp3Dest}";
        using var uvr = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.MPEG);
        Debug.Log("Created client to get audio clip");
        yield return uvr.SendWebRequest();
        Debug.Log($"Got result: {uvr.result}");
        if (uvr.result == UnityWebRequest.Result.Success)
        {
            var clip = DownloadHandlerAudioClip.GetContent(uvr);
            audioSource.clip = clip;
            Debug.Log("Created audio clip");

            // Comment this out to not play
            audioSource.Play();
        }
        else
        {
            Debug.Log("Web client was not successful. This is bad");
        }
    }
}
