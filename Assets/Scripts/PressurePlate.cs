using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    private Animator anim;
    private AudioSource audio;
    public bool Activated;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

    }

    private void Update()
    {
        anim.SetBool("isActive", Activated);
        if (Activated)
        {
            time += Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Activated || time > 1f) // this is to prevent more goblins from spawning after stepping on switch again
            return;

        time = 0f;
        
        if (collision.transform.CompareTag("Player"))
        {
            Activated = true;
            //activate activatable
            foreach (var item in objectsToActivate)
            {
                item.GetComponent<Activatable>().Activate();
            }
            audio.Play();
        }
    }
}
