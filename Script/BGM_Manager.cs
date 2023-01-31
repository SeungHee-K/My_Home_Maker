using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// æ¿ ∫∞ πË∞Ê¿Ωæ«

public class BGM_Manager : MonoBehaviour
{
    public AudioSource BGM;
    public AudioClip[] BGM_clip;

    public bool isAudio = true;


    void Start()
    {
        BGM = this.GetComponent<AudioSource>();

        BGM.volume = 0.1f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[0];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "Main")
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[1];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "House")
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[2];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "WareHouse")
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[3];
                BGM.Play();
                isAudio = false;
            }
        }
    }
}
