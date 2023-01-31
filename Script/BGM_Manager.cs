using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 별 배경음악 설정

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
        if (SceneManager.GetActiveScene().name == "Lobby") // 게임시작화면 Lobby 배경음악
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[0];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "Main") // 마당 Main 배경음악
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[1];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "House") // 집 안 House 배경음악
        {
            if (isAudio)
            {
                BGM.clip = BGM_clip[2];
                BGM.Play();
                isAudio = false;
            }
        }

        if (SceneManager.GetActiveScene().name == "WareHouse") // 창고 안 WareHouse 배경음악
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
