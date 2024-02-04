using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bands : MonoBehaviour
{
    AudioSource audioSource;
    // internal values
    float[] samples = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    float[] freqBandHighest = new float[8];

    // usable values
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    // import audio source
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // call methods
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeBandBuffer();
        MakeAudioBands();
    }

    // get 512 spectrum samples
    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    // seperate 512 samples into 8 averaged bands
    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            freqBand[i] = average * 10;
        }
    }

    // stabilizes band changes
    void MakeBandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }

            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }

    // converts bands to 0-1 range
    void MakeAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }
            audioBand[i] = freqBand[i] / freqBandHighest[i];
            audioBandBuffer[i] = freqBand[i] / freqBandHighest[i];
        }
    }
}

