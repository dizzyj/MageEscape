using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : Activatable
{
    // if you make this an array, you can add as many thingsToActivate from Unity.
    public GameObject[] thingsToActivate;
    public GameObject fire;
    public bool isLit;
    Light emittedLight;

    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        emittedLight = transform.Find("Point Light").gameObject.GetComponent<Light>();
        audio = GetComponent<AudioSource>();

        if (isLit)
        {
            Activate();
        }
    }

    override public void  Activate()
    {
        //turn on torch
        isLit = true;
        emittedLight.enabled = true;

        foreach (var item in thingsToActivate)
        {
            item.GetComponent<Activatable>().CheckCondition();
        }
        fire.SetActive(true);
        audio.Play();
    }
}
