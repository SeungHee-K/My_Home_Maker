using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 메인 카메라 플레이어 추적

public class FollowCam : MonoBehaviour
{
    public Transform Target;
    Vector3 Cam_Pos;


    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Cam_Pos = this.transform.position - Target.transform.position;
    }

    void Update()
    {
        this.transform.position = Target.transform.position + Cam_Pos;
    }
}
