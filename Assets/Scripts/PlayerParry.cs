using UnityEngine;

public class PlayerParry : MonoBehaviour
{
    public bool Unlocked;
    private Animator animator;
    private float time;
    private bool activated;
    private AudioSource audio;

    void Start()
    {
        animator = GetComponent<Animator>();
        time = 0;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Unlocked && !activated)
        {
            activated = true;
            animator.SetTrigger("ActivateParry");
            audio.PlayDelayed(.1f);
        }

        if (activated)
        {
            time += Time.deltaTime;
        }

        if (time > 3)
        {
            activated = false;
            time = 0;
        }
        
    }

    public bool IsParryActive()
    {
        return time >= 0.25 && time <= 1.75;
    }
}
