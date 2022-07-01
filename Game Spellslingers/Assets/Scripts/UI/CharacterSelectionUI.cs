using System;
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
    public static CharacterSelectionUI instance { get; private set; }
    [SerializeField]
    private GameObject[] characters;
    private int _charIndex;
    public delegate void SelectEventHandler<T, U>(T sender, U eventArgs);
    public event SelectEventHandler<CharacterSelectionUI, EventArgs> ArcherSelected;
    public event SelectEventHandler<CharacterSelectionUI, EventArgs> MageSelected;
    public event SelectEventHandler<CharacterSelectionUI, EventArgs> WarriorSelected;

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
            GameObject character = Instantiate(characters[CharIndex]);

            // Remove once select screen done
            if (character.GetComponent<Mage>() != null)
            {
                OnMageSelected();
            } else if (character.GetComponent<Archer>() != null)
            {
                OnArcherSelected();
            }
        }
    }

    private void OnArcherSelected()
    {
        ArcherSelected?.Invoke(this, EventArgs.Empty);
    }
    private void OnMageSelected()
    {
        MageSelected?.Invoke(this, EventArgs.Empty);
    }
    private void OnWarriorSelected()
    {
        WarriorSelected?.Invoke(this, EventArgs.Empty);
    }
}
