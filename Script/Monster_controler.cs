using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Monster_controler : MonoBehaviour
{
    public enum M_State
    {
        Idel,
        Walk,
        Attack,
        Damage,
        Jump,
        Die
    }

    [SerializeField]
    M_State m_state = M_State.Idel;

    Animator M_Ani;
    Rigidbody M_Rigid;
    UI_Manager ui_Manager;
    Player player;

    private GameObject Targer;
    public GameObject Find;

    private float M_HP;
    private int M_Coin;
    private float Speed;
    private float M_ATK;
    private bool isDie;

    Vector3 destination;

    private float pos_x;
    private float pos_z;






    public M_State State
    {
        get { return m_state; }

        set
        {
            m_state = value;
                        

            switch (m_state)
            {
                case M_State.Idel:
                    break;

                case M_State.Walk:                    
                    break;

                case M_State.Attack:
                    break;

                case M_State.Damage:
                    break;

                case M_State.Jump:
                    break;

                case M_State.Die:
                    break;

                default:
                    break;
            }

        }

    }

    void Start()
    {
        M_Ani = this.GetComponent<Animator>();
        M_Rigid = this.GetComponent<Rigidbody>();
        ui_Manager = GameManager.FindObjectOfType<UI_Manager>();
        player = GameObject.FindObjectOfType<Player>();
        Targer = GameObject.Find("Player");

        M_HP = Random.Range(15, 30);
        M_Coin = Random.Range(50, 100);
        Speed = Random.Range(0.5f, 2);
        M_ATK = Random.Range(5, 15);

        pos_x = Random.Range(-15, 15);
        pos_z = Random.Range(-15, 15);
    }

    void Update()
    {
        Walk();


        if (M_HP <= 0)
        {
            Die(player.Coin);
        }
    }


    public void Walk()
    {
        // 플레이어가 근처에 있을 경우
        // 몬스터 => 플레이어 다가가기
        if (Vector3.Distance(Targer.transform.position, this.transform.position) <= 10f)
        {
            Debug.Log("Walk!!!!!");

            this.transform.LookAt(Targer.transform.position);

            StartCoroutine("Find_Panel", 0.5f);

            M_Ani.SetBool("Walk", true);

            this.transform.position += this.transform.forward * Speed * Time.deltaTime;

            // 가까워졌을때 멈추기
            if (Vector3.Distance(Targer.transform.position, this.transform.position) <= 1f)
            {
                M_Ani.SetBool("Walk", false);

                Damage(player.P_ATK);
            }
        }


        // 플레이어가 근처에 없을 경우
        // 몬스터 주변 배회

        if (Vector3.Distance(Targer.transform.position, this.transform.position) > 10f)
        {
            destination = new Vector3(this.transform.position.x + pos_x, this.transform.position.y, this.transform.position.z + pos_z);

            this.transform.LookAt(destination);

            M_Ani.SetBool("Walk", true);

            Vector3 move = this.transform.forward * Speed * Time.deltaTime;
            M_Rigid.MovePosition(M_Rigid.position + move);

            if (Vector3.Distance(destination, this.transform.position) <= 1f)
            {
                M_Ani.SetBool("Walk", false);

            }
        }                
    }

    private IEnumerator Find_Panel(float time)
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


    private void Jump()
    {
        M_Ani.SetTrigger("Jump");
    }

    private void Damage(float P_ATK)
    {
        M_Ani.SetTrigger("Damage");

        M_HP -= P_ATK;
    }

    private void Attack(float DMG)
    {
        M_Ani.SetTrigger("Attack");

        ui_Manager.P_Hp.value -= DMG;
    }

    private void Die(float coin)
    {
        M_Ani.SetBool("Die", true);

        coin += M_Coin;

       
        Destroy(this.gameObject, 0.7f);       
    }
}
