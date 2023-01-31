using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// �÷��̾� �⺻ ����
public class Player : MonoBehaviour
{
    public GameObject Player_obj;
    public float P_HP;
    public float P_ATK;
    public float Coin;
    public GameObject HP_Panel;

    public Vector3 PlayerPos;
    public Animator P_Ani;
    public AudioSource P_Audio;
    public AudioClip[] P_clip; // �ȱ�(�ܵ�/�ǳ�)/����/�̵�(������)/����(��ġ/����)
    public ParticleSystem[] P_Effect;
    public GameObject P_Cam;
    private bool isDie = false;


    // ��ũ��Ʈ
    Monster monster;
    EFM_Manager EFM_manager; // ȿ����


    void Start()
    {
        P_HP = 100;
        P_ATK = Random.Range(5, 10);
        //Coin = 1000;

        P_Ani = this.GetComponent<Animator>();
        P_Audio = this.GetComponent<AudioSource>();
        EFM_manager = GameObject.FindObjectOfType<EFM_Manager>();

        P_Audio.volume = 0.1f;

        for (int i = 0; i < P_Effect.Length; i++)
        {
            P_Effect[i].Stop();
        }

    }

    void Update()
    {
        Player_obj = GameObject.Find("Player");

        if (P_HP <= 0)
        {
            if (!isDie)
            {
                P_Die();
            }
        }

        if (SceneManager.GetActiveScene().name == "House")
        {
            P_Ani.SetBool("Die", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("����!");

            P_Ani.SetTrigger("Attack");
            EFM_manager.audioSource.PlayOneShot(EFM_manager.EFM[0]); // ����
           
        }

    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            monster = collision.gameObject.GetComponent<Monster>();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                P_Attack(P_ATK);
            }
        }
    }

    private void P_Attack(float DMG)
    {
        P_Ani.SetTrigger("Attack");
        EFM_manager.audioSource.PlayOneShot(EFM_manager.EFM[0]); // ����

        Debug.Log("����!");

        monster.HP -= DMG;

        monster.M_Ani.SetTrigger("Damage");
    }

    private void P_Die()
    {
        isDie = true;

        P_Ani.SetTrigger("Dead");
        P_Effect[1].Play(); // �ذ� ����Ʈ

        HP_Panel.SetActive(true);

        Invoke("Panel_xx", 1f);

    }

    private void Panel_xx() // �׾����� �� ������ �̵�, HP 80 ȸ��
    {
        if (HP_Panel == null)
        {
            HP_Panel = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        }

        HP_Panel.SetActive(false);

        SceneManager.LoadScene("House");
        P_Audio.PlayOneShot(P_clip[3]); // �� �Ҹ�               

        P_HP = 80;

        Player_obj.transform.position = new Vector3(18, 3, 5);
        Player_obj.transform.rotation = new Quaternion(0, -180, 0, 0);

        Invoke("Die_x", 5f);
    }

    private void Die_x()
    {
        if (P_HP <= 0)
        {
            P_Ani.SetBool("Die", false);
            isDie = false;
        }
    }
}
