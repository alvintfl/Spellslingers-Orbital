using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * <summary>
 * A class that manages 
 * the options menu.
 * </summary>
 */
public class OptionsMenuController : MonoBehaviour
{
    public static OptionsMenuController instance;

    [SerializeField]
    private GameObject guc;

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
    // Start is called before the first frame update
    void OnEnable()
    {
        guc.SetActive(false);
        Time.timeScale = 0f;
    }

    void OnDisable() 
    {
        Time.timeScale = 1f;
        guc.SetActive(true);
    }

    public void ContinueGame() 
    {
        gameObject.SetActive(false);
    }

    public void QuitGame() 
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Gameplay");
    }
}
