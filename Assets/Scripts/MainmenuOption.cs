using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuOption : MonoBehaviour {

    static public bool[] isStageCleared = new bool[2];
    Image map2Button;
    

    

    private void Start()
    {
        
        if (isStageCleared[0]&& SceneManager.GetActiveScene().buildIndex == 1)
        {
            map2Button = GameObject.Find("LockImage").GetComponent<Image>();
            map2Button.enabled = false;

        }
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);


    }
    public void GoToMap1()
    {
        SceneManager.LoadScene(2);


    }
    public void GoToMap2()
    {
        if (isStageCleared[0])
        {
            SceneManager.LoadScene(3);
        }
        


    }
    public void GoToMapSelectAndEnableMap2()
    {
        isStageCleared[0] = true;
        SceneManager.LoadScene(0);

    }

    public void GotoMain()
    {
        SceneManager.LoadScene(0);

    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
