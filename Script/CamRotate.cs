using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 캡쳐모드 카메라 회전

public class CamRotate : MonoBehaviour
{
    public float Speed = 10;

    void Start()
    {

    }

    void Update()
    {
        Cam_Rotate();
    }

    public void Cam_Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 rot = transform.rotation.eulerAngles; // 현재 카메라 각도
            rot.y += Input.GetAxis("Mouse X") * Speed; // 마우스 x 위치 * 회전 스피드
            rot.x += -1 * Input.GetAxis("Mouse Y") * Speed; // 마우스 y 위치 * 회전 스피드
            Quaternion q = Quaternion.Euler(rot); // 카메라 각도 vector => quaternion 변환
            q.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);
        }
    }
}
