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

    //2018년 8월 1일 추가한 부분
    private Image fadeImage;

    public float animTime = 2f;
    private float start = 0f;
    private float end = 1f;
    private float time = 0f;

    private bool isPlaying = false;

    private Button RestartGame;


    // Use this for initialization
    void Awake()
    {




        clipSampleData = new float[sampleDataLength];
        audioSource = GetComponents<AudioSource>();
        PlayerSound = GameObject.Find("PlayerSound").GetComponent<Slider>();
        BackgroundSound = GameObject.Find("BackgroundSound").GetComponent<Slider>();
        PlayerSound.value = 0.0F;

        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();

        RestartGame = GameObject.Find("ReviveButton").GetComponent<Button>();
        RestartGame.gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (!audioSource[0].isPlaying))audioSource[0].Play();
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && (!audioSource[1].isPlaying))audioSource[1].Play();
        else if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) && (audioSource[1].isPlaying))audioSource[1].Stop();
        //audiosource[0] : jumpsound
        //audiosource[0] : walking sound
        //audiosource[0] : bowl sound
        if (audioSource[0].isPlaying && audioSource[2].isPlaying)
        {
            PlayerSound.value = 0.6F;
        }else if (audioSource[1].isPlaying && audioSource[2].isPlaying)
        {
            PlayerSound.value = 0.4F;
        }else if (audioSource[0].isPlaying )
        {
            PlayerSound.value = 0.2F;
        }else if (audioSource[1].isPlaying)
        {
            PlayerSound.value = 0.05F;
        }else
        {
            PlayerSound.value -= 0.05F;
        }

        

        if (PlayerSound.value > BackgroundSound.value)
        {
            Debug.Log("Be careful!");
            startFadeOutAnim();
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
        for (int i = 0; i < ChooseAudios.Length; i++)
        {
            audioSource[i].clip.GetData(clipSampleData, audioSource[i].timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.

            foreach (var sample in clipSampleData)
            {
                clipLoudness += Mathf.Abs(sample);
            }
        }


    }

    void startFadeOutAnim()
    {
        if (isPlaying)
        {
            return;

        }
        StartCoroutine("PlayFadeOut");
    }

    void startFadeInAnim()
    {
        if (isPlaying)
        {
            return;

        }
        StartCoroutine("PlayFadeIn");
    }
    IEnumerator PlayFadeOut()
    {
        isPlaying = true;

        Color color = fadeImage.color;
        time = 0f;
        color.a = Mathf.Lerp(start, end, time);

        while (color.a < 1f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(start, end, time);
            fadeImage.color = color;
            yield return null;
        }
        RestartGame.gameObject.SetActive(true);
        isPlaying = false;

    }

    IEnumerator PlayFadeIn()
    {
        isPlaying = true;

        Color color = fadeImage.color;
        time = 0f;
        color.a = Mathf.Lerp(end, start, time);

        while (color.a > 0f)
        {
            time += Time.deltaTime / animTime;

            color.a = Mathf.Lerp(end, start, time);
            fadeImage.color = color;
            yield return null;
        }
        isPlaying = false;
    }
    IEnumerable WaitFor1Second()
    {

        Debug.Log("hahahaha");
        yield return null;
    }


}