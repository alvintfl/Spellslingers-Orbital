using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject levelUpMenuUI;

    public void Resume()
    {
        levelUpMenuUI.SetActive(false);
        Time.timeScale = 1f;
        LevelUpMenu.gameIsPaused = false;
    }
    public void Pause()
    {
        levelUpMenuUI.SetActive(true);
        Time.timeScale = 0f;
        LevelUpMenu.gameIsPaused = true;
    }
}
