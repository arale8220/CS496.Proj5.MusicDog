using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmplitude : MonoBehaviour {
    public float _startScale, _scaleMultiplier;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._Amplitude * _scaleMultiplier) + _startScale, transform.localScale.z);
    }
}
