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
    public void PlayGame() 
    {
        // change when more classes are added to the game, maybe class select screen
        // int clickedButton = int.Parse(UnityEngine.EventSystems.EventSysytem.current.currentSelectedGameObject.name);
        SceneManager.LoadScene("CharSelectScreen");
        
    }

    public void OpenSettings() 
    {
    
    }

    public void ExitGame() 
    {
        Application.Quit();
    }   
}
