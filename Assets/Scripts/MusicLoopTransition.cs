using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoopTransition : MonoBehaviour
{
    public AudioSource musicIntro;
    public AudioSource musicLoop;
    private bool musicPlaying;

    // Update is called every frame
    public void ResumeMusic()
    {
        musicIntro.Play();
        musicLoop.PlayDelayed(musicIntro.clip.length - musicIntro.time);
    }

    public void PauseMusic()
    {
        musicIntro.Pause();
        musicLoop.Pause();
    }

    public void RestartMusic()
    {
        musicIntro.Stop();
        musicLoop.Stop();
        musicIntro.Play();
        musicLoop.PlayDelayed(musicIntro.clip.length);
    }
}
