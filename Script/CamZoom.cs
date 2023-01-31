using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamZoom : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject Cam_frame; // ī�޶� ����
    public Text CamText;

    // ��ũ��Ʈ
    UI_Manager uI_Manager;
    Player player;


    void Start()
    {


    }

    void Update()
    {
        uI_Manager = FindObjectOfType<UI_Manager>();
        player = FindObjectOfType<Player>();

        Cam_frame = uI_Manager.Panel[1];
        CamText = Cam_frame.transform.GetChild(2).gameObject.GetComponent<Text>();

        if(!player.P_Cam.activeSelf)
        {
            M_CameraZoom();
        }

        if (player.P_Cam.activeSelf)
        {
            P_CamZoom();
        }

    }

    private void M_CameraZoom()
    {        

        float scroll = Input.GetAxis("Mouse ScrollWheel") * -Speed;
        //                            �����콺 ��ũ�� ��


        // ī�޶� Ȯ��/��� �� �� �ִ� ���� 20 ~ 60 ���� ��ũ�� ��

        if (Camera.main.fieldOfView <= 20f && scroll < 0)
        {
            Camera.main.fieldOfView = 20f;
        }

        else if (Camera.main.fieldOfView >= 60f && scroll > 0)
        {
            Camera.main.fieldOfView = 60f;
        }

        else
        {
            Camera.main.fieldOfView += scroll;
        }

        CamText.text = "x " + Camera.main.fieldOfView.ToString();

    }

    private void P_CamZoom()
    {
        Camera p_cam = player.P_Cam.GetComponent<Camera>();
        float scroll = Input.GetAxis("Mouse ScrollWheel") * -Speed;
        //                            �����콺 ��ũ�� ��


        // ī�޶� Ȯ��/��� �� �� �ִ� ���� 20 ~ 60 ���� ��ũ�� ��

        if (p_cam.fieldOfView <= 20f && scroll < 0)
        {
            p_cam.fieldOfView = 20f;
        }

        else if (p_cam.fieldOfView >= 60f && scroll > 0)
        {
            p_cam.fieldOfView = 60f;
        }

        else
        {
            p_cam.fieldOfView += scroll;
        }

        CamText.text = "x " + p_cam.fieldOfView.ToString();
    }

    
}
