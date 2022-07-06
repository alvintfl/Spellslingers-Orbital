using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] private List<Sound> soundsList;
    private Dictionary<string, Sound> soundsDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.soundsDictionary = new Dictionary<string, Sound>();
            foreach (Sound sound in soundsList)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                sound.InitialiseSource(source);
                this.soundsDictionary.Add(sound.ToString(), sound);
            }
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public void Play(string name)
    {
        Sound sound = this.soundsDictionary[name];
        if (sound != null)
        {
            sound.Play();
        }
    }
}
