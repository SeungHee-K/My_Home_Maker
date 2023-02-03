using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 텔레포트
// 창고 안

public class Teleport_WareHouse : MonoBehaviour
{
    public UI_Manager ui_manager;


    void Start()
    {
        ui_manager = FindObjectOfType<UI_Manager>();
    }

    void Update() {}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ui_manager.Teleport_Panel[1].SetActive(true);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ui_manager.Teleport_Panel[1].SetActive(false);
        }
    }
}
