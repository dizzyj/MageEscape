using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]int health;
    public int maxHealth;
    public AudioClip DamageSoundEffect;
    public AudioClip DeathSoundEffect;

    private AudioSource audio;
    

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
        audio = GetComponent<AudioSource>();
    }

    public bool IsAlive()
    {
        if (health <= 0)
        {
            health = 0; // just for ez calculations for respawn or whatever. We can just heal for a specific amount.
            return false;
        }
        return true;
    }
    public void Damage(int damage, bool playSound = true)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            if (playSound)
            {
                audio.clip = DeathSoundEffect;
                audio.Play();
            }
        }
        else if (playSound)
        {
            audio.clip = DamageSoundEffect;
            audio.Play();
        }
    }
    public int GetHealth()
    {
        return health;
    }
    public void Heal(int healAmmount)
    {
        health += healAmmount;
    }
    public void SetHealth(int x)
    {
        health = x;
    }

}
