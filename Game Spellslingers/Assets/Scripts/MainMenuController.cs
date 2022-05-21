using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame() {
        // change when more classes are added to the game, maybe class select screen
        // int clickedButton = int.Parse(UnityEngine.EventSystems.EventSysytem.current.currentSelectedGameObject.name);
        int clickedButton = 0;
        SceneManager.LoadScene("Gameplay");
        GameController.instance.CharIndex = clickedButton;
    }
}
