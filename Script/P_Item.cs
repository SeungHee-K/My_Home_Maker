using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����
// ����(HP, STE)
// ���� => ��ġ�� ���� / ��ȭ�� ����

public class P_Item : MonoBehaviour
{
    
   public GameObject XX_item;
    
 
    // ��ũ��Ʈ
    public UI_Manager ui_manager;
    public Item_Manager item_manager;
    public Player player;


    void Start()
    {
    }

    void Update()
    {
        ui_manager = GameObject.FindObjectOfType<UI_Manager>();
        item_manager = GameObject.FindObjectOfType<Item_Manager>();
        player = GameObject.FindObjectOfType<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject item_collision = collision.gameObject;

        if (collision.gameObject.tag == "Coin")
        {
            ui_manager.Coin_plus();
            item_manager.itemList.Remove(item_collision);
            Destroy(item_collision);
        }

        if (collision.gameObject.tag == "HP")
        {
            ui_manager.HP_plus();
            item_manager.itemList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "STE")
        {
            ui_manager.STE_plus();
            item_manager.itemList.Remove(item_collision);
            Destroy(item_collision);
        }

        if (collision.gameObject.tag == "garbage")
        {
            ui_manager.Garbage_Panel.SetActive(true);            
            XX_item = item_collision;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "garbage")
        {
            ui_manager.Garbage_Panel.SetActive(false);
        }
    }

}
