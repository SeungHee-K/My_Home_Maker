using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ȿ���� (����/����ȯ/��ġ/����/������)
// - �÷��̾� �ͼ�

public class EFM_Manager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] EFM;




    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
}
