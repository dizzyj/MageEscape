using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KeySpawner : Activatable
{
    public GameObject Key;

    private bool activated;
    
    void Start()
    {
        activated = false;
    }

        

    public override void Activate()
    {
        if (!activated)
        {
            activated = true;
            Key.SetActive(true);
        }
    }
}
