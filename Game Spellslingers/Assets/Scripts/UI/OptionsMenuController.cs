using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * <summary>
 * A class that manages 
 * the options menu.
 * </summary>
 */
public class OptionsMenuController : MonoBehaviour
{
    public static OptionsMenuController instance { get; private set; }

    [SerializeField] private GameObject guc;
    private GameObject settings;

    public delegate void UIEventHandler<T, U>(T sender, U eventArgs);
    public event UIEventHandler<OptionsMenuController, EventArgs> OpenEvent;
    public event UIEventHandler<OptionsMenuController, EventArgs> CloseEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.settings = SettingsMenuController.instance.gameObject;
            this.settings.SetActive(false);
            this.settings.GetComponent<SettingsMenuController>().EnableCanvas();
            gameObject.GetComponent<Canvas>().enabled = true;
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        guc.SetActive(false);
        OnOpenEvent();
    }

    private void Update()
    {
        GetPlayerInput();
    }

    void OnDisable() 
    {
        guc.SetActive(true);
        OnCloseEvent();
    }

    private void GetPlayerInput()
    {
        if(Input.GetKeyDown("escape"))
        {
            ContinueGame();
        }
    }

    public void ContinueGame() 
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
    }

    public void Settings()
    {
        AudioManager.instance.Play("UI_buttonclick");
        this.settings.SetActive(true);
    }

    public void QuitGame() 
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
        SceneManager.LoadScene("Gameplay");
    }
    private void OnOpenEvent()
    {
        OpenEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnCloseEvent()
    {
        CloseEvent?.Invoke(this, EventArgs.Empty);   
    }
}
