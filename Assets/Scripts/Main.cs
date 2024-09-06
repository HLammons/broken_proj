using System.Web;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static Main Instance;
    public WebData Web;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Web = GetComponent<WebData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
