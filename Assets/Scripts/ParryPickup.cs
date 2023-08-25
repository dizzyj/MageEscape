using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryPickup : MonoBehaviour
{
    private bool canPickup;
    private bool pickedUp;
    private Player player;
    GameObject ui;
    SphereCollider col;

    private void Start()
    {
        col = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            canPickup = true;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().DisplayE();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
    }

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && !pickedUp)
        {
            pickedUp = true;
            player.Parry.Unlocked = true;
            GetComponent<AudioSource>().Play();
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = false;
            }
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
            col.enabled = false;
        }
    }
}
