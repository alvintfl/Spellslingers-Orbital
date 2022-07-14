using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsSceneController : MonoBehaviour
{
    [SerializeField] private Slider BgmSlider;
    [SerializeField] private Slider SfxSlider;
    [SerializeField] private Toggle screenToggle;

    private void Awake()
    {
        SettingsMenuController settings = SettingsMenuController.instance;
        this.BgmSlider.value = settings.GetBgmVolume();
        this.SfxSlider.value = settings.GetSfxVolume();
        this.screenToggle.isOn = settings.GetFullScreen();
    }

    public void SetBgmVolume(float volume)
    {
        SettingsMenuController.instance.SetBgmVolume(volume);
    }

    public void SetSfxVolume(float volume)
    {
        SettingsMenuController.instance.SetSfxVolume(volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        AudioManager.instance.Play("UI_buttonclick");
        SettingsMenuController.instance.SetFullScreen(isFullScreen);
    }

    public void ResetSettings()
    {
        SettingsMenuController settings = SettingsMenuController.instance;
        AudioManager.instance.Play("UI_buttonclick");
        settings.SetBgmVolume(0);
        this.BgmSlider.value = 0;
        settings.SetSfxVolume(0);
        this.SfxSlider.value = 0;
        settings.SetFullScreen(true);
        this.screenToggle.isOn = true;
    }

    public void Back()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("MainMenu");
    }
}
