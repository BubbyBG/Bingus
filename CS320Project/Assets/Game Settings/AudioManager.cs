using UnityEngine;
using UnityEngine.Audio;
using System;

/*
    This audio manager was provided courtesy of Brackeys on youtube, Using System to allow Array.Find method to search the program for the sound array
    link to video containing code/tutorial https://www.youtube.com/watch?v=6OT43pvUyfY&t=633s
*/
public class AudioManager : MonoBehaviour
{
    
    public Sound[] sounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    // Update is called once per frame
    public void Play( string name) {
            Sound s = Array.Find(sounds, sound => sound.name == name); //sound where sound.name == name find the audio clip we are looking to play
            s.source.Play();
    }
}
