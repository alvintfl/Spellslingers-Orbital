using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * <summary>
 * A class that manages the character
 * selection UI.
 * </summary>
 */
public class CharacterSelectionUI : MonoBehaviour
{
    public static CharacterSelectionUI instance;

    [SerializeField]
    private GameObject[] characters;


    private int _charIndex;
    public int CharIndex
    {
        get { return _charIndex; }
        set { _charIndex = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);        
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) 
    {
        if (scene.name == "Gameplay") {
            Instantiate(characters[CharIndex]);
        }
    }
} // class
