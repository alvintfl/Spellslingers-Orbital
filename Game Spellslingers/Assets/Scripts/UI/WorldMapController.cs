using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapController : MonoBehaviour
{

    [SerializeField]
    private GameObject guc;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m") || Input.GetKeyDown(KeyCode.Escape))
        {
            CloseWorldMap();
        }
    }

    void CloseWorldMap()
    {
        AudioManager.instance.Play("UI_buttonclick");
        gameObject.SetActive(false);
    }
}
