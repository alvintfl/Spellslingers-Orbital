using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Start()
    {
        ExpManager.LevelUp += Pause;
        Skill.Selected += Resume;

    }

    public void Resume(object sender, EventArgs e)
    {
        Time.timeScale = 1f;
    }

    public void Pause(object sender, EventArgs e)
    {
        Time.timeScale = 0f;
    }
}
