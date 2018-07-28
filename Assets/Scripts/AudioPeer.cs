using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour {
    AudioSource _audiosource;
    public float[] _samplesSee = new float[512];
    public static float[] _samples = new float[512];// takes all the hertz in the total spectrum of the audio playing, 20000 samples, and put´s them into 512 samples
    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    public static float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    public static float _Amplitude, _AmplitudeBuffer;
    float _AmplitudeHighest=0.01f;

    // Use this for initialization
    void Start () {
        _audiosource = GetComponent<AudioSource>();
	}

    void Update()
    {
        GetSpectrumAudioSource();//소스 분리
        MakeFrequencyBands();//8개 HZ범위 지정
        Bandbuffer();//부드럽게 전환
        CreateAudioBands();//위에서 범위 지정한거에 따라서 값
        GetAmplitude();//하나로 합한거  -->_Amplitude

    }

    void GetAmplitude()
    {
        float _CurrentAmp = 0;
        float _CurrentAmpBuf = 0;
        for(int i = 0; i < 8; i++)
        {
            _CurrentAmp += _audioBand[i];
            _CurrentAmpBuf += _audioBandBuffer[i];
        }

        if (_CurrentAmp > _AmplitudeHighest) _AmplitudeHighest = _CurrentAmp;
        _Amplitude = _CurrentAmp / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmpBuf / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {// create values between zero and one that can be apllied to a lot of different outputs
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audiosource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);// takes audio sources spectrum data and puts them into samples
        _audiosource.GetSpectrumData(_samplesSee, 0, FFTWindow.Blackman);
    }
    void Bandbuffer()//buffer to the value which creates a smooth down when the amplitude is lower than its previous value
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }

    }

    void MakeFrequencyBands()// divides the 512(각 범위 43Hz) samples into eight frequency bands(범위 2제곱 써서 적당히 나눔 0~87~258~602~1290~2666~5418~10922~21930)
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
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _freqBand[i] = average * 10;

        }

    }

}
