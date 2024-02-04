using NUnit.Framework;
using UnityEngine;
using WaveTrails.Utilities;

public class YoutubeDownloadTests
{
    [Test]
    public void YoutubeDownloadTest()
    {
        const string bruhUrl = "https://www.youtube.com/watch?v=qBFLYizpb5I";
        string currentDirectory = Application.dataPath;
        YouTubeDownload.DownloadFile(bruhUrl, currentDirectory);
    }
}