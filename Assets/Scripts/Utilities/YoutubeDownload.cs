using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace WaveTrails.Utilities
{
    public static class YouTubeDownload
    {
        private static readonly VideoLibrary.YouTube youtube = VideoLibrary.YouTube.Default;

        public static void DownloadFile(string url, string destination)
        {
            // Download the video
            var video = youtube.GetVideo(url);
            var fullDest = Path.Combine(destination, video.FullName);

            // Write video to an MP4
            File.WriteAllBytes(fullDest, video.GetBytes());

            // Using FFmpeg, convert MP4 to MP3
            var ffmpeg = new Process();
            var ffmpegPath = Path.Combine(Application.dataPath, "FFmpeg", "ffmpeg.exe");
            Console.WriteLine($"Looking for ffmpeg at {ffmpegPath}");
            ffmpeg.StartInfo.FileName = ffmpegPath;

            var mp3Path = $"{Path.GetDirectoryName(fullDest)}\\{Path.GetFileNameWithoutExtension(fullDest)}.mp3";

            ffmpeg.StartInfo.Arguments = $"-y -i \"{fullDest}\" \"{mp3Path}\"";

            ffmpeg.Start();
            ffmpeg.WaitForExit();
        }
    }
}