using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� (�ִ�3����)


public class Monster_Manager : MonoBehaviour
{
    public GameObject[] Monsters;

    public Transform MonsterArea_Pos;

    private int minItem = 0; 
    private int maxItem = 2; 
    private float betTime; 
    private float minTime = 10f; // �ּ� �ð� ���� (1��)
    private float maxTime = 60f; // �ִ� �ð� ���� (3��)
    private float lastTime; // ������ ��������
    public List<GameObject> M_count = new List<GameObject>();
    public float count;

    // ��Ʈ��Ʈ
    public UI_Manager uI_Manager;


    void Start()
    {
        betTime = Random.Range(minTime, maxTime);
        uI_Manager = GameObject.FindObjectOfType<UI_Manager>();
        lastTime = 0;
        minItem = 0;
        

    }

    void Update()
    {
        count = M_count.Count;

        if (Time.time >= lastTime + betTime && MonsterArea_Pos != null && count < 3)
        {
            lastTime = Time.time;
            betTime = Random.Range(minTime, maxTime);

            Spawn(MonsterArea_Pos.transform, 1f);
        }
        
    }

    private void Spawn(Transform floor, float time)
    {
        if (minItem <= maxItem)
        {
            minItem++;

            int itemNum = Random.Range(0, Monsters.Length);
            float randomX = Random.Range(-40, 40);
            float randomZ = Random.Range(-40, 40);

            GameObject Monster = Instantiate(Monsters[itemNum], new Vector3(MonsterArea_Pos.position.x + randomX, MonsterArea_Pos.position.y + 1, MonsterArea_Pos.position.z + randomZ), Quaternion.identity);

            M_count.Add(Monster);

            StartCoroutine(Warring(time));
        }
    }

    private IEnumerator Warring(float time)
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            uI_Manager.Warring.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            uI_Manager.Warring.SetActive(false);
        }

        yield return new WaitForSeconds(time);
    }

}
