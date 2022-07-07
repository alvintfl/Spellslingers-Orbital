using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour 
{
    [SerializeField] private string soundName;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool loop;
    private AudioSource source;

    public void InitialiseSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = this.clip;
        this.source.volume = this.volume;
        this.source.pitch = this.pitch;
        this.source.loop = this.loop;
    }

    public void Play()
    {
        this.source?.Play();
    }

    public void Stop()
    {
        this.source?.Stop(); 
    }

    public override string ToString()
    {
        return this.soundName;
    }
}
