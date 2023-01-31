using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoint : MonoBehaviour
{

    public GameObject[] Point;

    int RandomPoints;




    void Start()
    {
        RandomPoints = Random.Range(0, Point.Length);


        
        Debug.Log(RandomPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
