using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public SoundEffectPlayer sound;
    public Keys keys;
    public Material red;
    public Material yellow;
    public Material green;
    public Material blue;
    Color keyColor;
    Light emittedLight;
    GameObject player, ui;
    public bool canPickUp;

    

    // Start is called before the first frame update
    void Start()
    {
        emittedLight = GetComponent<Light>();
        switch (keys)
        {
            case Keys.Red:
                keyColor = Color.red;
                transform.Find("pPlane3").GetComponent<MeshRenderer>().material = red;
                break;
            case Keys.Yellow:
                keyColor = Color.yellow;
                transform.Find("pPlane3").GetComponent<MeshRenderer>().material = yellow;
                break;
            case Keys.Green:
                keyColor = Color.green;
                transform.Find("pPlane3").GetComponent<MeshRenderer>().material = green;
                break;
            case Keys.Blue:
                keyColor = Color.blue;
                transform.Find("pPlane3").GetComponent<MeshRenderer>().material = blue;
                break;
            default:
                keyColor = Color.white;
                break;

        }
        emittedLight.color = keyColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            canPickUp = true;
            if(ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().DisplayE();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickUp = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
    }
    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.E))
        {
            player.GetComponent<Inventory>().AddKey(keys);
            sound.PlayConsumable();
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
            Destroy(this.gameObject);
        }
    }
}
