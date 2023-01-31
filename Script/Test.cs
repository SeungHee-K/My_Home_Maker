using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(audioClip);
        }


        
    }
}
