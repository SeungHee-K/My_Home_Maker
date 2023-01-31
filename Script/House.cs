using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 집 안에 설치된 구조물 = 포화도

public class House : MonoBehaviour
{
    public int Obj_count;
    public List<GameObject> Obj = new List<GameObject>();

    void Start()
    {
        Find();
    }

    void Update()
    {

    }

    public void Find()
    {
        GameObject[] interior = GameObject.FindGameObjectsWithTag("interior");
        GameObject[] props = GameObject.FindGameObjectsWithTag("props");
        GameObject[] ground = GameObject.FindGameObjectsWithTag("ground");

        if (interior != null)
        {
            for (int i = 0; i < interior.Length; i++)
            {
                Obj.Add(interior[i]);
            }
        }

        if (props != null)
        {
            for (int i = 0; i < props.Length; i++)
            {
                Obj.Add(props[i]);
            }
        }

        if (ground != null)
        {
            for (int i = 0; i < ground.Length; i++)
            {
                Obj.Add(ground[i]);
            }
        }

        Obj_count = Obj.Count;
    }

}
