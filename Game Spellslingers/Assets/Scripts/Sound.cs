using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sound : MonoBehaviour 
{
    [SerializeField] private string soundName;
    [SerializeField] private AudioClip clip;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool loop;
    [SerializeField] private AudioMixerGroup output;
    private AudioSource source;

    public void InitialiseSource(AudioSource source)
    {
        this.source = source;
        this.source.clip = this.clip;
        this.source.volume = this.volume;
        this.source.pitch = this.pitch;
        this.source.loop = this.loop;
        this.source.outputAudioMixerGroup = this.output;
    }

    public void Play()
    {
        this.source?.Play();
    }

    public void Stop()
    {
        this.source?.Stop();
    }

    public bool IsPlaying()
    {
        return this.source.isPlaying;
    }

    public override string ToString()
    {
        return this.soundName;
    }
}
