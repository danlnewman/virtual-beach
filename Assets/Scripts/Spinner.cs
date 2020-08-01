﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Rigidbody spinny;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spin()
    {
        yield return new WaitForSeconds(5);
        spinny.AddRelativeTorque(0f, 0f, 1000f);
    }
}
