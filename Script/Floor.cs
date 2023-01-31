using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 땅에 설치물이 추가될때
// 포화도 증가

// 설치물 삭제 시
// 포화도 감소

// 자동 생성되는 풀/돌/가지 제외

public class Floor : MonoBehaviour
{
    public int Obj_count;
    public List<GameObject> Obj = new List<GameObject>();


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "tree" || collision.gameObject.tag == "animal" || collision.gameObject.tag == "fence" || collision.gameObject.tag == "building" || collision.gameObject.tag == "interior" || collision.gameObject.tag == "Props")
        {
            Obj.Add(collision.gameObject);
            Obj_count = Obj.Count;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "tree" || collision.gameObject.tag == "animal" || collision.gameObject.tag == "fence" || collision.gameObject.tag == "building" || collision.gameObject.tag == "interior" || collision.gameObject.tag == "Props")
        {
            Obj.Remove(collision.gameObject);
            Obj_count = Obj.Count;
        }
    }



    void Start()
    {
        
        
    }

    void Update()
    {
               
        

    }
}
