using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_AI : MonoBehaviour
{
    private Animator cat_ani;

    public float time;
    private float minTime = 0f;
    private float maxTime = 60f;




    void Start()
    {
        cat_ani = this.GetComponent<Animator>();

        time = Random.Range(minTime, maxTime);
            
        minTime = 0f;
    }

    void Update()
    {
        if (Time.deltaTime >= time)
        {
            cat_idle();
        }

        if (Time.deltaTime >= maxTime)
        {
            cat_move();            
        }


    }

    private void cat_move()
    {
        cat_ani.SetBool("Move", true);

        if (time >= 5f)
        {
            cat_ani.SetBool("Move", false);                        
        }

    }

    private void cat_idle()
    {
        cat_ani.SetBool("Rotate", true);

        if (time >= 5f)
        {
            cat_ani.SetBool("Rotate", false);
        }

    }
}
