using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Rigidbody spinny;

    // Start is called before the first frame update
    void Start()
    {
        spinny.AddRelativeTorque(0f, 0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
