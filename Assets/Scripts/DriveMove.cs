using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveMove : MonoBehaviour
{
    Vector2 move;
    Vector2 p;
    public string driveMessage;

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
        if (p[1] > 0.75F)
        {
            p[1] = 0.75F;
        }
        else if (p[1] < -0.75F)
        {
            p[1] = -0.75F;
        }
        driveMessage = 'd' + p[0].ToString() + ',' + p[1].ToString() + '&';
    }
}
