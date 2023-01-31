using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 효과음 (공격/씬전환/설치/편집/아이템)
// - 플레이어 귀속

public class EFM_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] EFM; // 이펙트 사운드


    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update() {}
    
}
