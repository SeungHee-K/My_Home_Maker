using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ĸ�ĸ�� ī�޶� ȸ��

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
            Vector3 rot = transform.rotation.eulerAngles; // ���� ī�޶� ����
            rot.y += Input.GetAxis("Mouse X") * Speed; // ���콺 x ��ġ * ȸ�� ���ǵ�
            rot.x += -1 * Input.GetAxis("Mouse Y") * Speed; // ���콺 y ��ġ * ȸ�� ���ǵ�
            Quaternion q = Quaternion.Euler(rot); // ī�޶� ���� vector => quaternion ��ȯ
            q.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, 2f);
        }
    }
}
