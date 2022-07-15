using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** 
 * <summary>
 * A class that manages 
 * the gameplay UI.
 * </summary>
 */
public class GameplayUIController : MonoBehaviour
{
    public static GameplayUIController instance { get; private set; }
    [SerializeField]
    private GameObject omc;
    [SerializeField]
    private GameObject optionsButton;
    [SerializeField]
    private GameObject charSheetButton;
    [SerializeField]
    private GameObject charSheet;
    [SerializeField]
    private GameObject worldMap;

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

    private void Update() {
        if (Input.GetKeyDown("c")) 
        {
            OpenCharSheet();
        }
        if (Input.GetKeyDown("m"))
        {
            OpenWorldMap();
        }
    }

    public void OpenOptionsMenu()
    {
        AudioManager.instance.Play("UI_buttonclick");
        omc.SetActive(true);
    }

    public void OpenCharSheet()
    {
        AudioManager.instance.Play("UI_buttonclick");

        if (charSheet.activeSelf)
        {
            optionsButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(70, -70);
            charSheetButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(70, -160);
            charSheet.SetActive(false);
        }
        else 
        {
            optionsButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, -70);
            charSheetButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(800, -160);

            charSheet.SetActive(true);
        }
    }

    public void OpenWorldMap()
    {
        AudioManager.instance.Play("UI_buttonclick");
        worldMap.SetActive(true);
    }
}
