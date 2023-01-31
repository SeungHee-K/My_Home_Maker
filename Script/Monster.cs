using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

// 몬스터 정보 (HP Coin Speed ATK)
// Animation (Idle - Walk - Jump - Attack - Damage - Die)
// 몬스터 위치 기준 (-15 ~ 15) 이내 플레이어 접근 시 느낌표UI, 플레이어 향해 다가오기

public class Monster : MonoBehaviour
{
    public float HP;
    public int Coin;
    private float Speed;
    public float ATK;

    public Animator M_Ani;
    private Rigidbody M_Rigid;
    public ParticleSystem[] M_Effect;

    public float time;
    public float atk_time;
    public bool isDie = false;

    public Vector3 destination;
    private float pos_x;
    private float pos_z;

    public GameObject Target;
    public GameObject Find;
    public Text Damage_text;
        
    // 스크립트
    public UI_Manager ui_Manager;
    public Player player;
    public Monster_Manager monster_manager;
    public EFM_Manager EFM_M;

    void Start()
    {
        M_Rigid = this.GetComponent<Rigidbody>();

        Target = GameObject.Find("Player");
        player = Target.GetComponent<Player>();
        ui_Manager = GameObject.FindObjectOfType<UI_Manager>();
        monster_manager = GameObject.FindObjectOfType<Monster_Manager>();
        M_Ani = this.GetComponent<Animator>();
        EFM_M = GameObject.FindObjectOfType<EFM_Manager>();

        HP = Random.Range(15, 30);
        Coin = Random.Range(100, 300);
        Speed = Random.Range(2, 5);
        ATK = Random.Range(5, 10);

        pos_x = Random.Range(-5, 5);
        pos_z = Random.Range(-5, 5);

        destination = new Vector3(this.transform.position.x + pos_x, this.transform.position.y, this.transform.position.z + pos_z);

        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Monster"), true);

        for (int i = 0; i < M_Effect.Length; i++)
        {
            M_Effect[i].Stop();
        }

    }

    void Update()
    {
        time += Time.deltaTime;

        M_Ai();

        TargetFind();

        if (HP <= 0)
        {
            M_Ani.SetBool("Walk", false);

            if (!isDie)
            {
                M_Die();
            }              
        }       
    }

    private void M_Ai()
    {
        if (time >= 10)
        {                      
            this.transform.LookAt(destination);

            if (Vector3.Distance(destination, transform.position) < 5f)
            {
                M_Ani.SetBool("Walk", true);
                
                Vector3 move = this.transform.forward * Speed * Time.deltaTime;
                M_Rigid.MovePosition(M_Rigid.position + move);

                if (Vector3.Distance(destination, transform.position) <= 0.5f || time >= 20)
                {
                    M_Ani.SetBool("Walk", false);
                    destination = this.transform.position;

                    time = 0;
                }
            }

            if (Vector3.Distance(destination, transform.position) <= 1f)
            {
                M_Ani.SetBool("Walk", false);
                Invoke("Pos_Find", 10f);
                return;
            }          
        }

        if (!M_Ani.GetBool("Die"))
        {
            destination = this.transform.position;
        }
    }

    private void Pos_Find()
    {
        destination = new Vector3(this.transform.position.x + pos_x, this.transform.position.y, this.transform.position.z + pos_z);

        time = 0;
        time += Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision) // 몬스터가 플레이어 이외 구조물에 부딪힐 경우 충돌무시
    {      
        if (collision.gameObject.tag == " Ground" && collision.gameObject.tag == "Building")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (collision.gameObject.tag == "Player")
        {
            M_Attack(ATK);
        }

        if (collision.gameObject.tag == "tree" || collision.gameObject.tag == "animal" || collision.gameObject.tag == "fence" || collision.gameObject.tag == "building" || collision.gameObject.tag == "interior" || collision.gameObject.tag == "Props")
        {
            Debug.Log("충돌무시");
            return;
        }
    }    

    private void M_Attack(float DMG)
    {
        atk_time += Time.deltaTime;

        if (atk_time >= 1f)
        {
            M_Ani.SetTrigger("Attack");

            player.P_HP -= DMG;
            player.P_Ani.SetTrigger("Damage");

            atk_time = 0;
        }        
    }

    private void M_Die()
    {
        isDie = true;

        if (isDie)
        {
            M_Ani.SetBool("Die", true);            
            monster_manager.M_count.Remove(this.gameObject);

            Invoke("M_Destory", 0.5f);            
        }                
    }

    private void M_Destory()
    {
        M_Effect[0].Play();
        Destroy(this.gameObject, 0.4f);
        EFM_M.audioSource.PlayOneShot(EFM_M.EFM[5]); // 몬스터 사라지는 소리
        player.Coin += Coin;

        Debug.Log("코인증가! : " + player.Coin);
    }

    private void TargetFind()
    {
        if (player.P_HP >= 1 || HP > 0)
        {
            if (Vector3.Distance(Target.transform.position, transform.position) < 10f)
            {
                this.transform.LookAt(Target.transform.position);
                this.transform.position += this.transform.forward * Speed * Time.deltaTime;
                M_Ani.SetBool("Walk", true);

                StartCoroutine(Warring(0.5f));
            }
        }

        if (player.P_HP <= 0 || HP <= 0)
        {
            M_Ani.SetBool("Walk", false);            
        }       
    }

    private IEnumerator Warring(float time)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Find.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            Find.SetActive(false);
        }

        yield return new WaitForSeconds(time);
    }
}
