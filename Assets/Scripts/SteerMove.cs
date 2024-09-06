using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerMove : MonoBehaviour

{
    Vector2 move;
    Vector2 p;
    public string steerMessage;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pan = new Vector2(move.x, move.y) * Time.deltaTime;

        transform.Translate(pan, Space.World);
        p = transform.localPosition;
        if (p[0] > 1.5F)
        {
            p[0] = 1.5F;
        }
        else if (p[0] < 0.0F)
        {
            p[0] = 0.0F;
        }
        //else
        //{
        //    p[0] = p[0] - 1.0F;
        //    //if (p[0] > 1.0)
        //    //    {
        //    //    p[0] = p[0] - 1.0F;
        //    //}
        //    //if (p[0] < 1.0)
        //    //{
        //    //    p[0] = p[0] - 1.0F;
        //    //}
        //}
        steerMessage = 's' + p[0].ToString() + ',' + p[1].ToString() + '&';
    }
}
