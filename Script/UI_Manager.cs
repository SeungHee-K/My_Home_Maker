using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

// UI 전체

public class UI_Manager : MonoBehaviour
{
    public GameObject MiniMap;

    public GameObject[] Btn;
    public GameObject[] Panel;
    public GameObject[] Mode;
    public GameObject[] Ui;
    public GameObject Warring;

    public Floor floor;
    public House house;
    public Slider Saturation; // 포화도
    public GameObject Saturation_Panel;

    public Slider P_Stemina; // 스테미나
    public Slider P_Hp; // 체력
    public Text Coin; // 코인

    public GameObject[] Teleport_Panel;
    public GameObject Setting_Panel;
    public GameObject Garbage_Panel;
    public GameObject Black;
    Image BlackImg;
    float Fade;
        
    public AudioSource BGM_Audio;
    public AudioSource[] EFM_Audio;

    // 스크립트
    public Player player;
    public SceneChange scene;
    public SoundSetting SoundSet;
    public EFM_Manager EFM_manager;

    void Start()
    {
        floor = GameObject.FindObjectOfType<Floor>();
        house = GameObject.FindObjectOfType<House>();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        scene = GameObject.FindObjectOfType<SceneChange>();

        BGM_Audio = GameObject.Find("SoundManager").gameObject.GetComponent<AudioSource>();        
    }

    void Update()
    {
        BlackImg = Black.GetComponent<Image>();
        Setting_Panel = GameObject.Find("SoundCanvas").transform.GetChild(0).gameObject;
        SoundSet = GameObject.FindObjectOfType<SoundSetting>();
        EFM_manager = GameObject.FindObjectOfType<EFM_Manager>();
        EFM_Audio[0] = GameObject.Find("Player").gameObject.GetComponent<AudioSource>();
        EFM_Audio[1] = EFM_manager.audioSource;

        P_Hp.value = player.P_HP;        
                
        Coin.text = player.Coin + " G";

        BGM_Audio.volume = SoundSet.BGM.value;
        EFM_Audio[0].volume = SoundSet.EFM.value;
        EFM_Audio[1].volume = SoundSet.EFM.value;
        
        
        if (SceneManager.GetActiveScene().name == "Lobby") // 로비일 경우
        {
            FadeInOut();
        }

        if (SceneManager.GetActiveScene().name == "Main") // 외부일 경우
        {
            Saturation.value = floor.Obj_count;

            FadeInOut();

            if (Input.GetKeyDown(KeyCode.M)) // 'M' 미니맵
            {
                if (!MiniMap.activeSelf)
                {
                    MiniMap.SetActive(true);
                }
                else
                {
                    MiniMap.SetActive(false);
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "House") // 실내일 경우
        {
            Saturation.value = house.Obj_count;

            FadeInOut();
        }

        if (SceneManager.GetActiveScene().name == "WareHouse")
        {
            Saturation.value = house.Obj_count;

            FadeInOut();
        }      

        if (Saturation.value >= 100) // 포화도 100 이상 시, 설치 불가
        {
            Saturation_Panel.SetActive(true);            
        }
        else
        {
            Saturation_Panel.SetActive(false);
        }
                
        if (Input.GetKeyDown(KeyCode.Escape)) // 'ESC' 모든 팝업창 닫기
        {
            if (Btn[0].activeSelf)
            {
                for (int i = 0; i < Btn.Length; i++)
                {
                    Btn[i].SetActive(false);
                }

                for (int i = 0; i < Mode.Length; i++)
                {
                    Mode[i].SetActive(false);
                }

                for (int i = 0; i < Panel.Length; i++)
                {
                    Panel[i].SetActive(false);
                }                
            }

            fixed_Ui();
        }
    }

    public void Setting_On()
    {
        Setting_Panel.SetActive(true);
    }

    private void fixed_Ui()
    {
        if (!Ui[0].activeSelf)
        {
            for (int i = 0; i < Ui.Length; i++)
            {
                Ui[i].SetActive(true);
            }

            for (int i = 0; i < Panel.Length; i++)
            {
                Panel[i].SetActive(false);
            }
        }

        if (player.P_Cam.activeSelf)
        {
            player.P_Cam.SetActive(false);
        }
    }

    public void Btn_xx()
    {
        for (int i = 0; i < Btn.Length; i++)
        {
            Btn[i].SetActive(false);
        }

        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }

    }

    public void P_Camera_Mode()
    {
        for (int i = 0; i < Btn.Length; i++)
        {
            Btn[i].SetActive(false);
        }

        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
        }

        for (int i = 0; i < Ui.Length; i++)
        {
            Ui[i].SetActive(false);
        }

        Panel[1].SetActive(true);

        player.P_Cam.SetActive(true);
    }

    public void Capture()
    {
        string folderPath = Application.dataPath + "/test";
        string fileName = DateTime.Now.ToString("yyyy-MM-dd-zzz")+".png";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        ScreenCapture.CaptureScreenshot(Path.Combine(folderPath, fileName));


        Invoke("fixed_Ui", 0.7f);
    }

    
    public void STE_plus() // 스테미나 회복포션
    {
        P_Stemina.value += 10;
        player.P_Effect[0].Play(); // 회복 이펙트
        EFM_manager.audioSource.PlayOneShot(EFM_manager.EFM[4]); // 아이템
    }

    public void HP_plus() // 체력 회복 포션
    {
        player.P_HP += 10;
        player.P_Effect[0].Play();
        EFM_manager.audioSource.PlayOneShot(EFM_manager.EFM[4]); // 아이템
    }

    public void Coin_plus() // 코인 습득
    {
        player.Coin += 100;
        player.P_Effect[0].Play();
        EFM_manager.audioSource.PlayOneShot(EFM_manager.EFM[4]); // 아이템
    }

    public void FadeInOut()
    {
        if (Black.activeSelf && BlackImg.color.a <= 0 && scene.isScene)
        {
            StartCoroutine("FadeOut", 0.5f); // 페이드아웃
        }
        if (Black.activeSelf && BlackImg.color.a >= 1 && !scene.isScene)
        {
            StartCoroutine("FadeIn", 0.5f); // 페이드인
        }
    }

    public IEnumerator FadeOut()
    {
        Fade = 0.0f;

        while (Fade <= 1.0f)
        {
            Fade += 0.01f; 
            yield return new WaitForSeconds(0.02f); 

            BlackImg.color = new Color(0, 0, 0, Fade);
        }
    }

    public IEnumerator FadeIn()
    {
        Fade = 1.0f;

        while (Fade >= 0.0f)
        {
            Fade -= 0.01f;

            yield return new WaitForSeconds(0.02f);
            BlackImg.color = new Color(0, 0, 0, Fade);
        }
    }
}
