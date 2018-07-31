using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackGroundMusic : MonoBehaviour {

    // Use this for initialization
    static int PlayerCollectMusicDisk = 0;
    GameObject BGM;


	void Start () {
        BGM = GameObject.Find("AudioSource_BackGroundMusic");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerCollectMusicDisk++;
            BGM.GetComponents<AudioSource>()[PlayerCollectMusicDisk].mute = false;
            
            


            
            Destroy(this.gameObject);
        }
    }
}
