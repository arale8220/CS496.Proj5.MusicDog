using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class falling : MonoBehaviour {

    public AudioSource audio;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnCollisionEnter(Collision others)  //Plays Sound Whenever collision detected
    {
        if (others.gameObject.tag=="floor")  audio.Play();
    }
}
