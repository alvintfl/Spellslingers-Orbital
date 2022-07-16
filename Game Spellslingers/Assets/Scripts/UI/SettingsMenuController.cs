using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public static SettingsMenuController instance { get; private set; }
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BgmSlider;
    private float bgmVolume;
    [SerializeField] private Slider SfxSlider;
    private float sfxVolume;
    [SerializeField] private Toggle screenToggle;
    private bool isFullScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.bgmVolume = 0;
            this.sfxVolume = 0;
            this.isFullScreen = Screen.fullScreen;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        this.BgmSlider.value = this.bgmVolume;
        this.SfxSlider.value = this.sfxVolume;
        this.screenToggle.isOn = this.isFullScreen;
    }

    public void SetBgmVolume(float volume)
    {
        this.bgmVolume = volume;
        this.audioMixer.SetFloat("BgmVolume", volume);
    }

    public float GetBgmVolume()
    {
        return this.bgmVolume;
    }

    public void SetSfxVolume(float volume)
    {
        this.sfxVolume = volume;
        this.audioMixer.SetFloat("SfxVolume", volume);
    }
    public float GetSfxVolume()
    {
        return this.sfxVolume;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        AudioManager.instance.Play("UI_buttonclick");
        this.isFullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public bool GetFullScreen()
    {
        return this.isFullScreen;
    }

    public void ResetSettings()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SetBgmVolume(0);
        this.BgmSlider.value = 0;
        SetSfxVolume(0);
        this.SfxSlider.value = 0;
        SetFullScreen(true);
        this.screenToggle.isOn = true;
    }

    public void Back()
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
    }

    public void EnableCanvas()
    {
        gameObject.GetComponent<Canvas>().enabled = true;
    }
}
