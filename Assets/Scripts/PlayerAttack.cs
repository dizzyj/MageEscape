using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    private Animator animator;
    private Player player;
    private AudioSource audio;
    public AudioClip SoundEffect;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponentInChildren<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            player.blockMovement = true;
            animator.SetTrigger("Punch");
            audio.clip = SoundEffect;
            audio.PlayDelayed(0.5f);
        }
        
    }
}
