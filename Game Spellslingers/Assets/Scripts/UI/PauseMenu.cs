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
    private int pauseCount;

    public void Awake()
    {
        ExpManager.LevelUp += Pause;
        GameplayUIController.instance.OpenEvent += Pause;
        GameplayUIController.instance.CloseEvent += Resume;
        OptionsMenuController.instance.OpenEvent += Pause;
        OptionsMenuController.instance.CloseEvent += Resume;
        Skill.Selected += Resume;
    }

    public void OnDisable()
    {
        Reset();
        ExpManager.LevelUp -= Pause;
        GameplayUIController.instance.OpenEvent -= Pause;
        GameplayUIController.instance.CloseEvent -= Resume;
        OptionsMenuController.instance.OpenEvent -= Pause;
        OptionsMenuController.instance.CloseEvent -= Resume;
        Skill.Selected -= Resume;
    }

    public void Resume(object sender, EventArgs e)
    {
        this.pauseCount--;
        if (this.pauseCount == 0)
        {
            Time.timeScale = 1f;
        }
    }

    public void Pause(object sender, EventArgs e)
    {
        this.pauseCount++;
        Time.timeScale = 0f;
    }

    private void Reset()
    {
        this.pauseCount = 0;
        Time.timeScale = 1f;
    }
}
