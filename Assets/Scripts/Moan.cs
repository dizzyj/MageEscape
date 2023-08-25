using System;
using UnityEngine;
using Random = System.Random;

public class Moan : MonoBehaviour
{
        public AudioClip SoundEffect;
        private float time;
        private AudioSource audio;
        private Random rng;

        public void Start()
        {
                rng = new Random();
                audio = GetComponent<AudioSource>();
                time = (float) rng.NextDouble() * 15;
        }

        public void Update()
        {
                time += Time.deltaTime;

                if (time > 30)
                {
                        audio.clip = SoundEffect;
                        audio.Play();
                        time = (float) rng.NextDouble() * 15;
                }
        }
}
