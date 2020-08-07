using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance = null;
    [SerializeField]
    ParticleSystem[] ngcConfetti;
    [SerializeField]
    ParticleSystem[] rtxConfetti;
    [SerializeField]
    Spinner spinner;


    public bool activateRtxConfetti = false;
    public bool activateNgcConfetti = false;
    public bool activateRaffle = false;

    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activateRtxConfetti)
        {
            ShootConfetti(rtxConfetti);
            activateRtxConfetti = false;
        }
        if (activateNgcConfetti)
        {
            ShootConfetti(ngcConfetti);
            activateNgcConfetti = false;
        }
        if (activateRaffle)
        {
            StartCoroutine(spinner.Spin());
            activateRaffle = false;
        }
    }

    static public void OnMessage(UnityMessage message)
    {
        if (!message.mtype.Equals("heartbeat"))
            Debug.Log("Got a message: " + message.mtype);

        switch(message.mtype)
        {
            case "rtxconfetti":
                instance.activateRtxConfetti = true;
                break;
            case "ngcconfetti":
                instance.activateNgcConfetti = true;
                break;
            case "raffle":
                instance.activateRaffle = true;
                break;
        }
    }

    public void ShootConfetti(ParticleSystem[] confettiShooters)
    {
        foreach(ParticleSystem confettiShooter in confettiShooters)
        {
            confettiShooter.Play();
        }
        StartCoroutine(StopConfetti(confettiShooters));
    }

    IEnumerator StopConfetti(ParticleSystem[] confettiShooters)
    {
        yield return new WaitForSeconds(15);
        foreach (ParticleSystem confettiShooter in confettiShooters)
        {
            confettiShooter.Stop();
        }
    }
}
