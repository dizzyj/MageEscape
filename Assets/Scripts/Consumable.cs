using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    bool playerCanInteract;
    GameObject player, parent, ui;
    public int healAmount;
    public SoundEffectPlayer sound;
    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    void Update()
    {
        if (playerCanInteract && Input.GetKeyDown(KeyCode.E))
        {
            sound.PlayConsumable();
            player.GetComponent<Health>().Heal(healAmount);
            player.GetComponent<Player>().UpdateHealthBar();
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
            Destroy(parent);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanInteract = true;
            player = other.gameObject;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().DisplayE();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanInteract = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
    }
}
