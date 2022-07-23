using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * <summary>
 * A class that manages the credits page UI.
 * </summary>
 */
public class CreditsPageUI : MonoBehaviour
{
    public void MainMenu()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("MainMenu");
    }
}
