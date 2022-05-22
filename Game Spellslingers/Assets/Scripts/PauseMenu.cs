using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Start()
    {
        ExpManager.LevelUp += (sender, e) => Pause();
        Skill.Selected += (sender, e) => Resume();
        Player.instance.Health.DiedInfo += Pause;
        SceneManager.sceneLoaded += (scene, mode) => Resume();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
}
