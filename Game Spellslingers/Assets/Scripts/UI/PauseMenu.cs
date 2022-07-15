using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * <summary>
 * A class that is responsible for pausing
 * and resuming the game.
 * </summary>
 */
public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance { get; private set; }
    private int pauseCount;

    public delegate void UIEventHandler<T, U>(T sender, U eventArgs);
    public event UIEventHandler<PauseMenu, EventArgs> PauseEvent;
    public event UIEventHandler<PauseMenu, EventArgs> ResumeEvent;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ExpManager.LevelUp += Pause;
            GameplayUIController.instance.OpenEvent += Pause;
            GameplayUIController.instance.CloseEvent += Resume;
            OptionsMenuController.instance.OpenEvent += Pause;
            OptionsMenuController.instance.CloseEvent += Resume;
            WorldMapController.instance.OpenEvent += Pause;
            WorldMapController.instance.CloseEvent += Resume;
            Skill.Selected += Resume;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void OnDisable()
    {
        Reset();
        ExpManager.LevelUp -= Pause;
        GameplayUIController.instance.OpenEvent -= Pause;
        GameplayUIController.instance.CloseEvent -= Resume;
        OptionsMenuController.instance.OpenEvent -= Pause;
        OptionsMenuController.instance.CloseEvent -= Resume;
        WorldMapController.instance.OpenEvent -= Pause;
        WorldMapController.instance.CloseEvent -= Resume;
        Skill.Selected -= Resume;
    }

    public void Resume(object sender, EventArgs e)
    {
        this.pauseCount--;
        if (this.pauseCount == 0)
        {
            Time.timeScale = 1f;
            OnResumeEvent();
        }
    }

    public void Pause(object sender, EventArgs e)
    {
        this.pauseCount++;
        Time.timeScale = 0f;
        OnPauseEvent();
    }

    private void Reset()
    {
        this.pauseCount = 0;
        Time.timeScale = 1f;
    }

    private void OnPauseEvent()
    {
        PauseEvent?.Invoke(this, EventArgs.Empty);
    }
    private void OnResumeEvent()
    {
        ResumeEvent?.Invoke(this, EventArgs.Empty);
    }
}
