using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPageUI : MonoBehaviour
{
    public void MainMenu()
    {
        AudioManager.instance.Play("UI_buttonclick");
        SceneManager.LoadScene("MainMenu");
    }
}
