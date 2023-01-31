using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public GameObject Player;
    public GameObject Black;

    public float SceneTime;

    public bool isScene;
    public bool isHouse;
    public bool isMain;
    public bool isWarehouse;

    // 스크립트
    public Player player;
    public BGM_Manager BGM_M;
    public EFM_Manager EFM_M;

    public void House() // 집
    {
        isScene = true;

        isHouse = true;
        SceneTime = 0;        
    }

    public void Main() // 마당
    {
        isScene = true;

        Black.SetActive(true);

        isMain = true;
        SceneTime = 0;
    }

    public void WareHouse() // 창고
    {
        isScene = true;

        Black.SetActive(true);

        isWarehouse = true;
        SceneTime = 0;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    void Start()
    {
        isScene = false;
        SceneTime = 0;
        isHouse = false;
        isMain = false;
    }

    void Update()
    {
        Player = GameObject.Find("Player");
        player = Player.GetComponent<Player>();
        BGM_M = GameObject.FindObjectOfType<BGM_Manager>();
        EFM_M = GameObject.FindObjectOfType<EFM_Manager>();

        SceneTime += Time.deltaTime;

        if (SceneTime >= 1.5f && isHouse)
        {
            SceneManager.LoadScene("House");
            EFM_M.audioSource.PlayOneShot(EFM_M.EFM[1]); // 문 소리
            Player.transform.position = new Vector3(0, 1, -10);

            BGM_M.isAudio = true;
            isScene = false;
            isHouse = false;
            SceneTime = 0;
        }

        if (SceneTime >= 1.5f && isMain)
        {
            SceneManager.LoadScene("Main");
            EFM_M.audioSource.PlayOneShot(EFM_M.EFM[1]); // 문 소리
            Player.transform.position = new Vector3(0, 3.4f, 0);

            BGM_M.isAudio = true;
            isScene = false;
            isMain = false;
            SceneTime = 0;
        }

        if (SceneTime >= 1.5f && isWarehouse)
        {
            SceneManager.LoadScene("WareHouse");
            EFM_M.audioSource.PlayOneShot(EFM_M.EFM[1]); // 문 소리
            Player.transform.position = new Vector3(-18, 1, 0);

            BGM_M.isAudio = true;
            isScene = false;
            isWarehouse = false;
            SceneTime = 0;


        }
    }        
}
