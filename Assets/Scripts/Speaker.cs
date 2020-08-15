using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    float originalX;
    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Sin(Time.realtimeSinceStartup));
        transform.position = new Vector3(originalX + .01f*Mathf.Sin(10*Time.realtimeSinceStartup), transform.position.y, transform.position.z);
    }
}
