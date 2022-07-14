using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that manages 
 * the gameplay UI.
 * </summary>
 */
public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController instance { get; private set; }
    [SerializeField]
    private GameObject omc;
    [SerializeField]
    private GameObject optionsButton;
    [SerializeField]
    private GameObject charSheetButton;
    [SerializeField]
    private GameObject charSheet;

    public delegate void UIEventHandler<T, U>(T sender, U eventArgs);
    public event UIEventHandler<GameplayUIController, EventArgs> OpenEvent;
    public event UIEventHandler<GameplayUIController, EventArgs> CloseEvent;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() 
    {
        GetPlayerInput();
    }

    private void GetPlayerInput()
    {
        if (Input.GetKeyDown("c"))
        {
            ToggleCharSheet();
        }
        if (Input.GetKeyDown("escape"))
        {
            OpenOptionsMenu();
        }
    }

    public void OpenOptionsMenu()
    {
        AudioManager.instance.Play("UI_buttonclick");
        omc.SetActive(true);
    }

    public void ToggleCharSheet()
    {
        AudioManager.instance.Play("UI_buttonclick");
        if (charSheet.activeSelf)
        {
            optionsButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(70, -70);
            charSheetButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(70, -160);
            charSheet.SetActive(false);
            OnCloseEvent();
        }
        else 
        {
            optionsButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, -70);
            charSheetButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, -160);
            charSheet.SetActive(true);
            OnOpenEvent();
        }
    }

    private void OnOpenEvent()
    {
        OpenEvent.Invoke(this, EventArgs.Empty);
    }

    private void OnCloseEvent()
    {
        CloseEvent.Invoke(this, EventArgs.Empty);   
    }
}
