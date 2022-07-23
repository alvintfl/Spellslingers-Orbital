using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that manages the
 * world map.
 * </summary>
 */
public class WorldMapController : MonoBehaviour
{
    public static WorldMapController instance { get; private set; }

    [SerializeField]
    private GameObject guc;

    public delegate void UIEventHandler<T, U>(T sender, U eventArgs);
    public event UIEventHandler<WorldMapController, EventArgs> OpenEvent;
    public event UIEventHandler<WorldMapController, EventArgs> CloseEvent;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    void OnDisable()
    {
        guc.SetActive(true);
        OnCloseEvent();
    }

    void Update()
    {
        if (Input.GetKeyDown("m") || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWorldMap();
        }
    }

    void CloseWorldMap()
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
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
