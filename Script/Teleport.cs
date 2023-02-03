using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 마당 -> 집 안/창고 안
// 집 안/창고 안 -> 마당
// 씬 전환 텔레포트 기능 및 팝업창(UI)

public class Teleport : MonoBehaviour
{
    // 스크립트
    public UI_Manager ui_manager;


    void Start()
    {
        ui_manager = FindObjectOfType<UI_Manager>();

    }
    private void OnCollisionEnter(Collision collision)
    {                
        if (collision.gameObject.tag == "Player")
        {
            ui_manager.Teleport_Panel[0].SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ui_manager.Teleport_Panel[0].SetActive(false);
        }
    }

    void Update() { }
}
