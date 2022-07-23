using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/** 
 * <summary>
 * A class that manages 
 * the main menu.
 * </summary>
 */
public class MainMenuController : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.instance.Play("bgm_mainmenu");
    }
    public void PlayGame() 
    {
        SceneManager.LoadScene("CharSelectScreen");
        AudioManager.instance.Play("UI_buttonclick");
    }

    public void OpenSettings() 
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("Settings");
    }

    public void ExitGame() 
    {
        AudioManager.instance.Play("UI_buttonclick");
        Application.Quit();
    }

    public void OpenCredits()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("CreditPage");
    }
}
