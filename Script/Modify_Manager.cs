using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// ������Ʈ ��ġ ����
// ������Ʈ ���� (Ŭ��)
// ȸ�� (90��)
//

public class Modify_Manager : MonoBehaviour
{

    public GameObject Select_OBJ;
    public GameObject ModifyTool;
    public Vector3 Modify_Pos;

    public AudioSource Modify_Audio;
    public ParticleSystem P_Effet;

    private bool Modify_bool;
    private bool isMove;
    private int RotateNum = 90;

    // ��ũ��Ʈ
    public UI_Manager ui_manager;
    public Floor floor;
    public House house;
    public Player player;
    public EFM_Manager EFM_M;



    void Start()
    {
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();
        floor = GameObject.Find("Floor").gameObject.GetComponent<Floor>();
        house = GameObject.FindObjectOfType<House>();
        player = GameObject.FindObjectOfType<Player>();
        EFM_M = GameObject.FindObjectOfType<EFM_Manager>();

        Modify_bool = false;
        isMove = false;
    }

    void Update()
    {
        if (ui_manager.Mode[1].activeSelf)
        {
            Modify();
        }

        if (ui_manager.Mode[0].activeSelf)
        {
            ui_manager.Mode[1].SetActive(false);
            ModifyTool.SetActive(false);
            Modify_bool = false;
        }


        if (Modify_bool && ui_manager.Mode[1].activeSelf && Select_OBJ == null) // ��������϶�
        {
            if (Input.GetMouseButtonDown(0) && !isPointerOverUIobj())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit)) // ������ ������Ʈ ����
                {
                    if (hit.collider.gameObject.tag == "tree" || hit.collider.gameObject.tag == "fence" || hit.collider.gameObject.tag == "building" || hit.collider.gameObject.tag == "animal" || hit.collider.gameObject.tag == "interior" || hit.collider.gameObject.tag == "props")
                    {
                        Select_OBJ = hit.collider.gameObject;

                        ModifyTool.SetActive(true);
                        Select_OBJ.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Modify_Exit();
            }
        }

        if (!Modify_bool) // ������� �ƴҶ� ��� ��ġ�� ����ǥ��(������) ��Ȱ��ȭ
        {
            if (SceneManager.GetActiveScene().name == "Main") // �ܺ��� ���
            {
                for (int i = 0; i < floor.Obj_count; i++)
                {
                    if (floor.Obj[i].transform.GetChild(0).gameObject.activeSelf)
                    {
                        floor.Obj[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }

            if (SceneManager.GetActiveScene().name == "House") // �ǳ��� ���
            {
                for (int i = 0; i < house.Obj_count; i++)
                {
                    if (house.Obj[i].transform.childCount == 0) continue; // �ڽ��� ���ٸ� �Ʒ� �ڵ� ����x

                    if (house.Obj[i].transform.GetChild(0).gameObject.activeSelf)
                    {
                        house.Obj[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
            }
        }

        if (isMove) // move �������϶�
        {
            if (Select_OBJ != null)
            {
                if (Input.GetMouseButtonDown(0) && !isPointerOverUIobj())
                {
                    Debug.Log("ok2");

                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit))
                    {
                        Modify_Pos = hit.point;
                        Select_OBJ.GetComponent<Transform>().position = Modify_Pos;

                        EFM_M.audioSource.PlayOneShot(EFM_M.EFM[3]); // ���� �Ҹ�
                        player.P_Ani.SetTrigger("Make");

                        Select_OBJ.transform.GetChild(0).gameObject.SetActive(false);
                        
                    }
                    Select_OBJ = null;
                    isMove = false;
                }
            }
        }
    }

    private void Modify()
    {
        ModifyTool.SetActive(true);
        Modify_bool = true;
    }

    private void Modify_Exit()
    {
        ui_manager.Btn_xx();
        ui_manager.Mode[1].SetActive(false);
        ModifyTool.SetActive(false);
        Modify_bool = false;
    }

    public void Move() // ������Ʈ �̵�
    {
        if (Select_OBJ != null)
        {
            isMove = true;
        }
    }

    public void Rotate() // ������Ʈ ȸ��
    {

        Select_OBJ.transform.Rotate(0f, Select_OBJ.transform.rotation.y + RotateNum, 0f);

        Select_OBJ.transform.GetChild(0).gameObject.SetActive(false);

        EFM_M.audioSource.PlayOneShot(EFM_M.EFM[3]); // ���� �Ҹ�
        player.P_Ani.SetTrigger("Make");

        Select_OBJ = null;
    }

    public void Destroy() // ������Ʈ ����
    {
        Select_OBJ.transform.GetChild(0).gameObject.SetActive(false);

        EFM_M.audioSource.PlayOneShot(EFM_M.EFM[3]); // ���� �Ҹ�
        player.P_Ani.SetTrigger("Make");

        Destroy(Select_OBJ, 0.2f);
        Select_OBJ = null;

        Modify_Exit();
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
