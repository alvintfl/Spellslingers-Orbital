using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * <summary>
 * A class that manages the death
 * screen UI.
 * </summary>
 */
public class DeathScreenMenuManager : MonoBehaviour
{
    public static DeathScreenMenuManager instance { get; private set; }

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
    public void RestartGame()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("Gameplay");
    }
    public void ReturnToMainMenu()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("MainMenu");
    }
}