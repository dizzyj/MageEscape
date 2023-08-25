using UnityEngine;

public class BeamController : MonoBehaviour
{
    RedHollowControl rhc;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rhc = GetComponentInChildren<RedHollowControl>();
        audio = GetComponent<AudioSource>();
    }
    public void PlayCharging()
    {
        rhc.Play_Charging();
    }
    public void EndCharging()
    {
        rhc.Finish_Charging();
    }
    public void BurstBeam()
    {
        rhc.Burst_Beam();
        audio.Play();
    }
    public void Dead()
    {
        rhc.Dead();
    }
}
