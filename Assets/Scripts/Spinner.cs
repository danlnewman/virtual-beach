using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Rigidbody spinny;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spin()
    {
        spinny.transform.Rotate(0f, 0f, Random.Range(0f, 359.99f));
        GetComponent<Animator>().SetBool("spinnerIsOn", true);
        yield return new WaitForSeconds(5);
        spinny.AddRelativeTorque(0f, 0f, 2000f);
        yield return new WaitForSeconds(15);
        GetComponent<Animator>().SetBool("spinnerIsOn", false);
    }
}
