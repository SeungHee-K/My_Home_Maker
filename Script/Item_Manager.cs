using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 랜덤 아이템 / 구조물 생성


public class Item_Manager : MonoBehaviour
{
    public GameObject[] item;

    public Transform ItemArea_Pos;

    private int minItem = 0; // 아이템 갯수 0
    private int maxItem = 10; // 아이템 최대 생성 갯수
    public float betTime; // 생성주기
    private float minTime = 10f; // 최소 시간 간격 (1분)
    private float maxTime = 30f; // 최대 시간 간격 (3분)
    public float lastTime; // 마지막 생성시점

    public List<GameObject> itemList = new List<GameObject>();
    private float itemCount;

    // 스크립트
    public P_Item p_item;
    public Player player;
    public EFM_Manager EFM_M;


    void Start()
    {
        betTime = Random.Range(minTime, maxTime);
        lastTime = 0;
        minItem = 0;
    }

    void Update()
    {
        itemCount = itemList.Count;
        p_item = GameObject.FindObjectOfType<P_Item>();
        player = GameObject.FindObjectOfType<Player>();
        EFM_M = GameObject.FindObjectOfType<EFM_Manager>();

        if (Time.time >= lastTime + betTime && ItemArea_Pos != null && itemCount <= 10)
        {
            lastTime = Time.time;
            betTime = Random.Range(minTime, maxTime);

            Spawn(ItemArea_Pos.transform);
        }
    }

    private void Spawn(Transform floor)
    {
        if (minItem <= maxItem)
        {
            minItem++;

            int itemNum = Random.Range(0, item.Length);
            float randomX = Random.Range(-40, 40);
            float randomZ = Random.Range(-40, 40);

            GameObject Item = Instantiate(item[itemNum], new Vector3(ItemArea_Pos.position.x + randomX, ItemArea_Pos.position.y + 1, ItemArea_Pos.position.z + randomZ), Quaternion.identity);

            itemList.Add(Item);

        }     
        
    }

    public void Garbage_x(string Delete)
    {
        if (Delete == "삭제")
        {
            itemList.Remove(p_item.XX_item);
            player.P_Ani.SetTrigger("Make");
            EFM_M.audioSource.PlayOneShot(EFM_M.EFM[3]); // 편집 소리
            Destroy(p_item.XX_item, 0.2f);
        }
    }


}
