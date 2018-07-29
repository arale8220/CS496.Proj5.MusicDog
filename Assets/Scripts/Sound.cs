using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{

    AudioSource[] audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    public TextMeshProUGUI soundText_max;
    public TextMeshProUGUI soundText;

    private Slider BackgroundSound;
    private Slider PlayerSound;

    bool isOver5= false;

    // Use this for initialization
    void Awake()
    {

        
        clipSampleData = new float[sampleDataLength];
        audioSource = GetComponents<AudioSource>();
        BackgroundSound =  GameObject.Find("BackgroundSound").GetComponent<Slider>();
        PlayerSound = GameObject.Find("PlayerSound").GetComponent<Slider>();


    }

    // Update is called once per frame
    void Update()
    {

        currentUpdateTime += Time.deltaTime;

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
        if ((Time.time > 5)&& !isOver5) //5초가 지날 때 새로운 music 을 turn on 
        {
            audioSource[1].Play();
            isOver5 = true;
        }
        int[] sample = { 0,1 };
        if (currentUpdateTime >= updateStep)
        {
            clipLoudness = 0f;
            AddingSoundsEffect(sample);
            clipLoudness /= sampleDataLength;
        }
           

        



        soundText.text = clipLoudness.ToString();
        BackgroundSound.value = clipLoudness;
        
       

    }
    void AddingSoundsEffect(int[] ChooseAudios)
    {//timesamples 는 오디오가 시작된 것을 기준으로 함. 즉, 여러개의 오디오가 있으면 시작한 타이밍에 따라 여러 소리가 겹침. 
        for (int i=0; i < ChooseAudios.Length; i++)
        {
            audioSource[i].clip.GetData(clipSampleData, audioSource[i].timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            
            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
        }
        

    }

}