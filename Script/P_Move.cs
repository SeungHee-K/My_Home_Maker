using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class P_Move : MonoBehaviour
{
    private Camera P_Camera; // �÷��̾� ����ī�޶�
    private bool isMove; // �̵� ����
    public Vector3 destination; // �̵��� ��ǥ
    private Animator P_Ani; // �ִϸ��̼�
    private Rigidbody P_Rigid;

    public float Speed = 10f;

    // ��ũ��Ʈ
    public UI_Manager ui_manager;
    public Player player;


    private void Awake()
    {
        P_Ani = this.GetComponent<Animator>();
        P_Rigid = this.GetComponent<Rigidbody>();
    }


    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        P_Camera = Camera.main;
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();


        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "House" || SceneManager.GetActiveScene().name == "WareHouse")
        {
            if (!ui_manager.Mode[0].activeSelf && !ui_manager.Mode[1].activeSelf) // ��ġ + ������尡 �ƴҶ� �÷��̾� �̵�����
            {
                if (Input.GetMouseButton(0) && !isPointerOverUIobj()) // ���콺 ���� Ŭ��, UIŬ���� �ƴҶ�
                {
                    Ray ray = P_Camera.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "interior")
                        {
                            destination = hit.point;
                            isMove = true;
                        }
                    }
                }

                Move();
            }
        }

    }



    private void Move()
    {
        if (isMove)
        {
            this.transform.LookAt(destination); // �÷��̾� -> �̵��� ��ǥ �ٶ󺸱�


            if (Vector3.Distance(destination, transform.position) > 0.1f)
            {
                P_Ani.SetBool("Move", true);

                if (SceneManager.GetActiveScene().name == "Main")
                {
                    if (!player.P_Audio.isPlaying)
                    {
                        player.P_Audio.clip = player.P_clip[0]; // �ܵ� �� �ܹ���
                        player.P_Audio.Play();
                        player.P_Audio.loop = true;

                        Debug.Log("�ܵ�");
                    }
                }

                if (SceneManager.GetActiveScene().name == "House" || SceneManager.GetActiveScene().name == "WareHouse")
                {
                    if (!player.P_Audio.isPlaying)
                    {
                        player.P_Audio.clip = player.P_clip[1]; // �ǳ� �ܹ���
                        player.P_Audio.Play();
                        player.P_Audio.loop = true;

                        Debug.Log("��ũ��Ʈ");
                    }
                }

                Vector3 move = this.transform.forward * Speed * Time.deltaTime;
                P_Rigid.MovePosition(P_Rigid.position + move);
            }

            if (Vector3.Distance(destination, transform.position) <= 2f)
            {
                isMove = false;
                player.P_Audio.Stop();
                return;
            }

        }

        else
        {
            P_Ani.SetBool("Move", false);
            P_Rigid.MoveRotation(Quaternion.identity);
            player.P_Audio.Stop();

            //if (!player.P_Audio.isPlaying)
            //{
            //    player.P_Audio.clip = null;
            //}
        }
    }


    private bool isPointerOverUIobj()
    {
        // UI Ŭ�� �� �ٸ� Ŭ���̺�Ʈ ����X

        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);

        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();

        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;

    }


}
