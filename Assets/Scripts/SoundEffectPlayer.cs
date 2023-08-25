using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    AudioSource sound;
    public AudioClip consumable;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        if(sound == null)
        {
            print("Get component error: Sound");
        }
    }

    public void PlayConsumable()
    {
        sound.PlayOneShot(consumable);
    }
}
