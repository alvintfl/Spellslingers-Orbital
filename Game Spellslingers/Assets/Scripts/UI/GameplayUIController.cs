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
    public static GameplayUIController instance;
    [SerializeField]
    private GameObject omc;
    [SerializeField]
    private GameObject charSheet;
    [SerializeField]
    private GameObject optionsButton;
    [SerializeField]
    private GameObject charSheetButton;

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
    }

    public void OpenOptionsMenu()
    {
        omc.SetActive(true);
    }

    public void OpenCharSheet()
    {


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
}
