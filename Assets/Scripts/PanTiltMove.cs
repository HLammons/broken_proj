using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanTiltMove : MonoBehaviour
{
    Vector2 move;
    Vector2 p;
    public string panMessage;

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

        if (p[0] > 0.75)
        {
            p[0] = 0.75F;
        }
        else if (p[0] < -0.75)
        {
            p[0] = -0.75F;
        }
        if (p[1] > .75)
        {
            p[1] = .75F;
        }
        else if (p[1] < -.75)
        {
            p[1] = -.75F;
        }

        panMessage = 'p' + p[0].ToString() + ',' + p[1].ToString() + '&';
    }
}
