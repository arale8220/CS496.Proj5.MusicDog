using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sound_Player : MonoBehaviour
{

    AudioSource[] audioSource;
    int[] audioIndexs;
    public int NumberOfBackgroundMusics;

    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    public TextMeshProUGUI soundText_max;
    public TextMeshProUGUI soundText;

    private Slider BackgroundSound;
    private Slider PlayerSound;

    bool isOver5 = false;

    // Use this for initialization
    void Awake()
    {




        clipSampleData = new float[sampleDataLength];
        audioSource = GetComponents<AudioSource>();
        PlayerSound = GameObject.Find("PlayerSound").GetComponent<Slider>();
        BackgroundSound = GameObject.Find("BackgroundSound").GetComponent<Slider>();
        soundText = GameObject.Find("sound").GetComponent<TextMeshProUGUI>();
        audioIndexs = new int[audioSource.Length];
        for (int i = 0; i < audioIndexs.Length; i++)
        {
            audioIndexs[i] = i;
        }
        PlayerSound.value = 0.0F;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (!audioSource[0].isPlaying))
        {
            audioSource[0].Play();

        }

        else if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && (!audioSource[1].isPlaying))
        {
            audioSource[1].Play();


        }
        else if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) && (audioSource[1].isPlaying))
        {
            audioSource[1].Stop();
        }


        if (audioSource[0].isPlaying)
        {
            PlayerSound.value = 0.4F;
        }
        else if (audioSource[1].isPlaying)
        {
            PlayerSound.value = 0.1F;
        }

        else
        {
            PlayerSound.value -= 0.005F;
        }

        if (PlayerSound.value > BackgroundSound.value)
        {
            Debug.Log("Be careful!");
        }

        



        //currentUpdateTime += Time.deltaTime;

        /*
        if (currentUpdateTime >= updateStep) //update for each 0.1 second
        {
            currentUpdateTime = 0f;
            audioSource[0].clip.GetData(clipSampleData, audioSource[0].timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
            clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        }
        */

        /*
        if (currentUpdateTime >= updateStep)
        {
            clipLoudness = 0f;
            AddingSoundsEffect(audioIndexs);
            clipLoudness /= sampleDataLength;
        }
        */





        /*
        soundText.text = clipLoudness.ToString();
        PlayerSound.value = clipLoudness;
        */



    }
    void AddingSoundsEffect(int[] ChooseAudios)
    {//timesamples 는 오디오가 시작된 것을 기준으로 함. 즉, 여러개의 오디오가 있으면 시작한 타이밍에 따라 여러 소리가 겹침. 
        for (int i = 2; i < ChooseAudios.Length; i++)
        {
            if(!audioSource[i].mute)//만약 오디오가 mute 가 아니면 
            audioSource[i].clip.GetData(clipSampleData, audioSource[i].timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.

            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
        }


    }

}