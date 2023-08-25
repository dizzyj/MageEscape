using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : Enemy
{
    public GameObject key;
    private bool dropped;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        dropped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!myHealth.IsAlive() && !dropped)
        {
           // Drop key
           key.SetActive(true);
           dropped = true;
        }
    }
}
