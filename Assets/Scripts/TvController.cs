using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvController : MonoBehaviour
{
    public Animator tvAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Up()
    {
        tvAnimator.SetBool("IsActive", true);
    }

    public void Down()
    {
        tvAnimator.SetBool("IsActive", false);
    }
}
