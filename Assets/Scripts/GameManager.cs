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
    [SerializeField]
    TvController tv;


    public bool activateRtxConfetti = false;
    public bool activateNgcConfetti = false;
    public bool activateRaffleUp = false;
    public bool activateRaffleDown = false;
    public bool activateRaffleSpin = false;
    public bool activateTvUp = true;
    public bool activateTvDown = true;


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
        Random.InitState((int)System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
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
        if (activateRaffleUp)
        {
            spinner.Up();
            activateRaffleUp = false;
        }
        if (activateRaffleDown)
        {
            spinner.Down();
            activateRaffleDown = false;
        }
        if (activateRaffleSpin)
        {
            spinner.Spin();
            activateRaffleSpin = false;
        }
        if (activateTvUp)
        {
            tv.Up();
            activateTvUp = false;
        }
        if (activateTvDown)
        {
            tv.Down();
            activateTvDown = false;
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
            case "raffleup":
                instance.activateRaffleUp = true;
                break;
            case "raffledown":
                instance.activateRaffleDown = true;
                break;
            case "rafflespin":
                instance.activateRaffleSpin = true;
                break;
            case "tvup":
                instance.activateTvUp = true;
                break;
            case "tvdown":
                instance.activateTvDown = true;
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
