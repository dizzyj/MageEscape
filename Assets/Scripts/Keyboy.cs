using UnityEngine;

public class Keyboy : Enemy
{
    public SoundEffectPlayer sound;
    int deadhash;
    GameObject ui;
    bool locked = false;
    [SerializeField] private bool playerInTrigger = false;
    [SerializeField] private bool dead = false;
    private AudioSource audio;
    
    // Start is called before the first frame update
    public override void  Start()
    {
        anim = GetComponent<Animator>();
        deadhash = Animator.StringToHash("IsDead");
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( playerInTrigger && Input.GetKeyDown(KeyCode.E) && !dead)
        {
            player.GetComponent<Player>().KOTarget(this.gameObject);
        }
        if(dead && !locked && Input.GetKeyDown(KeyCode.E))
        {
            //give key
            player.GetComponent<Inventory>().AddKey(Keys.Red);
            sound.PlayConsumable();
            //ui message
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
            //delete goblin
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            player = other.gameObject;
            if(player.GetComponent<Player>().crouching == true)
            {
                if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
                ui.GetComponent<HUD>().DisplayE();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            if (ui == null) ui = GameObject.FindGameObjectWithTag("HUD");
            ui.GetComponent<HUD>().HideE();
        }
    }
    public void Unlock()
    {
        locked = false;
    }
    override public void Die()
    {
        //play death animation
        locked = true;
        dead = true;
        anim.SetBool(deadhash, dead);
        audio.Play();
    }
}
