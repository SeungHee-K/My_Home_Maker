using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 선택한 오브젝트 생성
// 설치모드일때 클릭한 위치

public class Creat_Manager : MonoBehaviour
{
    public GameObject[] Creat_tree;
    public GameObject[] Creat_fence;
    public GameObject[] Creat_animal;
    public GameObject[] Creat_building;

    public Material[] Creat_interior;
    public GameObject[] Creat_furniture;
    public GameObject[] Creat_props;
    public GameObject[] Creat_Crops;

    public GameObject[] Wall;
    public GameObject Floor;
    public GameObject Coin_X;

    public GameObject Creat_OBJ;
    private Vector3 Creat_Pos;
    public AudioSource Creat_Audio;

    public GameObject[] Btn;

    // 스크립트
    public UI_Manager ui_manager;
    public Player player;
    public EFM_Manager EFM_M;

    private bool Creat_bool;

    void Start()
    {
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        EFM_M = GameObject.FindObjectOfType<EFM_Manager>();

        Creat_Audio = this.GetComponent<AudioSource>();
        Creat_bool = false;

    }

    void Update()
    {      
        if (Creat_bool && ui_manager.Mode[0].activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (player.Coin >= 100)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit = new RaycastHit();

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject.tag == "ground" || hit.collider.gameObject.tag == "furniture")
                        {
                            Creat_Pos = hit.point;

                            Instantiate(Creat_OBJ, Creat_Pos, Quaternion.identity);
                            EFM_M.audioSource.PlayOneShot(EFM_M.EFM[2]); // 설치 소리
                            player.P_Ani.SetTrigger("Make");

                            Creat_OBJ.transform.GetChild(0).gameObject.SetActive(false);                                                        
                        }
                    }

                    ui_manager.Btn_xx();
                    ui_manager.P_Stemina.value -= 1;                                        
                }

                else if(player.Coin < 100)
                {
                    Coin_X.SetActive(true);

                    Invoke("Panel_x", 0.5f);

                    player.Coin += 100;
                }

                Invoke("Creat_Exit", 0.5f);
            }
        }        

        if (!Creat_bool)
        {
            Creat_OBJ = null;            
        }
    }

    public void Creat_Tree(string name)
    {
        if (name == "Tree1")
        {
            Creat_OBJ = Creat_tree[0];
        }
        
        if (name == "Tree2")
        {
            Creat_OBJ = Creat_tree[1];
        }

        if (name == "Tree3")
        {
            Creat_OBJ = Creat_tree[2];
        }

        if (name == "Tree4")
        {
            Creat_OBJ = Creat_tree[3];
        }

        if (name == "Tree5")
        {
            Creat_OBJ = Creat_tree[4];
        }

        if (name == "Tree6")
        {
            Creat_OBJ = Creat_tree[5];
        }

        if (name == "Tree7")
        {
            Creat_OBJ = Creat_tree[6];
        }

        if (name == "Tree8")
        {
            Creat_OBJ = Creat_tree[7];
        }

        Creat();
    }

    public void Creat_Fence(string name)
    {
        if (name == "fence1")
        {
            Creat_OBJ = Creat_fence[0];
        }

        if (name == "fence2")
        {
            Creat_OBJ = Creat_fence[1];
        }

        if (name == "fence3")
        {
            Creat_OBJ = Creat_fence[2];
        }

        if (name == "fence4")
        {
            Creat_OBJ = Creat_fence[3];
        }

        Creat();
    }

    public void Creat_Animal(string name)
    {
        if (name == "Sheep")
        {
            Creat_OBJ = Creat_animal[0];
        }

        if (name == "Duck")
        {
            Creat_OBJ = Creat_animal[1];
        }

        if (name == "Pig")
        {
            Creat_OBJ = Creat_animal[2];
        }

        if (name == "Cow")
        {
            Creat_OBJ = Creat_animal[3];
        }

        Creat();
    }

    public void Creat_Building(string name)
    {
        if (name == "House1")
        {
            Creat_OBJ = Creat_building[0];
        }

        if (name == "House2")
        {
            Creat_OBJ = Creat_building[1];
        }

        if (name == "WareHouse")
        {
            Creat_OBJ = Creat_building[2];
        }

        if (name == "WoodCase")
        {
            Creat_OBJ = Creat_building[3];
        }

        if (name == "Wood1")
        {
            Creat_OBJ = Creat_building[4];
        }

        if (name == "Wood2")
        {
            Creat_OBJ = Creat_building[5];

        }

        if (name == "Chair1")
        {
            Creat_OBJ = Creat_building[6];
        }

        if (name == "Chair2")
        {
            Creat_OBJ = Creat_building[7];
        }

        Creat();
    }

    public void Creat_Interior_wall(Material mat) // 벽 재질 변경
    {               
        for (int i = 0; i < Wall.Length; i++)
        {
            Wall[i].GetComponent<MeshRenderer>().material = mat;
        }                
    }

    public void Creat_Interior_floor(Material mat) // 바닥 재질 변경
    {
        Floor.GetComponent<MeshRenderer>().material = mat;
    }

    public void Creat_Furniture(string name)
    {
        if (name == "Bed")
        {
            Creat_OBJ = Creat_furniture[0];
        }

        if (name == "Nightstand")
        {
            Creat_OBJ = Creat_furniture[1];
        }

        if (name == "Footstool")
        {
            Creat_OBJ = Creat_furniture[2];
        }

        if (name == "Dresser")
        {
            Creat_OBJ = Creat_furniture[3];
        }

        if (name == "Table")
        {
            Creat_OBJ = Creat_furniture[4];
        }

        if (name == "Chair_in")
        {
            Creat_OBJ = Creat_furniture[5];
        }

        if (name == "Carpet1")
        {
            Creat_OBJ = Creat_furniture[6];
        }

        if (name == "Carpet2")
        {
            Creat_OBJ = Creat_furniture[7];
        }

        Creat();
    }

    public void Creat_Props(string name)
    {
        if (name == "Cat")
        {
            Creat_OBJ = Creat_props[0];
        }        

        if (name == "Chicken")
        {
            Creat_OBJ = Creat_props[1];
        }

        if (name == "Plate")
        {
            Creat_OBJ = Creat_props[2];
        }

        if (name == "Cup")
        {
            Creat_OBJ = Creat_props[3];
        }

        if (name == "Candle")
        {
            Creat_OBJ = Creat_props[4];
        }                

        Creat();
    }

    public void Creat_Crop(string name)
    {
        if (name == "Tomato")
        {
            Creat_OBJ = Creat_Crops[0];
        }

        if (name == "Cabbage")
        {
            Creat_OBJ = Creat_Crops[1];
        }

        if (name == "TomatoBox")
        {
            Creat_OBJ = Creat_Crops[2];
        }

        if (name == "CabbeageBox")
        {
            Creat_OBJ = Creat_Crops[3];
        }

        if (name == "Box")
        {
            Creat_OBJ = Creat_Crops[4];
        }

        if (name == "Mud")
        {
            Creat_OBJ = Creat_Crops[5];
        }

        if (name == "Mud_Tomato")
        {
            Creat_OBJ = Creat_Crops[6];
        }

        if (name == "Mud_Cabbage")
        {
            Creat_OBJ = Creat_Crops[7];
        }

        Creat();

    }

    private void Creat()
    {
        Debug.Log("설치모드");

        for (int i = 0; i < Btn.Length; i++)
        {
            Btn[i].SetActive(false);
        }
        Debug.Log("0000");

        Creat_bool = true;

    }

    private void Creat_Exit()
    {
        player.Coin -= 100;
        ui_manager.Mode[0].SetActive(false);
        Creat_bool = false;
    }

    private void Panel_x()
    {
        Coin_X.SetActive(false);
    }
}
