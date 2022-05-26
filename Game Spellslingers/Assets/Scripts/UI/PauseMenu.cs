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
    public void Start()
    {
        ExpManager.LevelUp += Pause;
        Skill.Selected += Resume;
        SceneManager.sceneLoaded += Resume;
    }

    public void OnDisable()
    {
        ExpManager.LevelUp -= Pause;
        Skill.Selected -= Resume;
        SceneManager.sceneLoaded -= Resume;
    }

    public void Resume(object sender, EventArgs e)
    {
        Time.timeScale = 1f;
    }
    public void Resume(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
    }

    public void Pause(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
    }
}
