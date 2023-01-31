using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 회전

public class Rotator : MonoBehaviour
{
    public float rot_Speed = 60f;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Rotate(0f, rot_Speed * Time.deltaTime, 0f);


    }
}
