using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public Keys key;
    bool playerCanOpen;
    public GameObject player, ui;
    private Animator anim;
    private AudioSource audio;
    private bool open;
    public GameObject[] objectsToActivate;
    

    
    // Start is called before the first frame update
    void Start()
    {
         
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCanOpen && player.GetComponent<Inventory>().CheckKey(key) && Input.GetKeyDown(KeyCode.E) && !open)
        {
            open = true;
            anim.SetBool("isOpen", true);
            audio.Play();
            
            foreach (var item in objectsToActivate)
            {
                item.GetComponent<Activatable>().Activate();
            }

            foreach (var boxCollider in GetComponentsInChildren<BoxCollider>())
            {
                if (boxCollider.gameObject.name == "Barrier")
                {
                    boxCollider.enabled = false;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanOpen = true;
            player = other.gameObject;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().DisplayE();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCanOpen = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
    }
}
