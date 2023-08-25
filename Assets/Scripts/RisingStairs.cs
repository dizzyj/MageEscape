using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingStairs : Activatable
{
    public GameObject[] Conditionals;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void CheckCondition()
    {
        foreach (var item in Conditionals)
        {
            if (!item.GetComponent<Torch>().isLit)
            {
                print("In conditionals");
                return;
            }
            
        }
        print("Past conditionals");
        Activate();
    }
    public override void Activate()
    {
        print("Animating");
        anim.SetBool("isActive",true);
        audioSource.Play();
    }
}
