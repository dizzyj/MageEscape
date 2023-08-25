using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicLoopTransition : MonoBehaviour
{
    public AudioSource musicIntro;
    public AudioSource musicLoop;
    private bool musicPlaying;

    // Start is called before the first frame update
    void Start()
    {
        musicPlaying = false;
    }

    // Update is called once per frame
    void OnEnable()
    {
        if (!musicPlaying) {
            musicIntro.Play();
            musicLoop.PlayDelayed(musicIntro.clip.length);
            musicPlaying = true;
        }
    }

    void OnDisable()
    {
        if (musicPlaying) {
            musicIntro.Stop();
            musicLoop.Stop();
            musicPlaying = false;
        }
    }
}
