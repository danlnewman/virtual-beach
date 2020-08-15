using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Rigidbody spinny;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spin()
    {
        spinny.AddRelativeTorque(0f, 0f, 2000f);
    }

    public void Up()
    {
        spinny.transform.Rotate(0f, 0f, Random.Range(0f, 359.99f));
        animator.SetBool("spinnerIsOn", true);

    }

    public void Down()
    {
        animator.SetBool("spinnerIsOn", false);

    }
}
