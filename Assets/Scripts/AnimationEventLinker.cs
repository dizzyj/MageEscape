using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventLinker : MonoBehaviour
{
    Player player;
    AudioSource sound;
    public HitBox hb;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        player = transform.parent.gameObject.GetComponent<Player>();
        if(player == null)
        {
            print("failed to grab player in animation linker");
        }
    }

    public void Footstep()
    {
        sound.Play();
    }
    public void TargetAnimPlay()
    {
        GameObject target = player.GetTarget();
        target.GetComponent<Enemy>().Die();
    }
    public void UnlockMovement()
    {
        player.blockMovement = false;
        player.GetAnimator().SetBool("KO", false);
    }
    public void CheckHitLand()
    {
        hb.DetectHit();
        if (hb.getHit())
        {
            hb.Damage();
        }
    }
    public void AskReplay()
    {
        player.DeathUI();
    }
}
