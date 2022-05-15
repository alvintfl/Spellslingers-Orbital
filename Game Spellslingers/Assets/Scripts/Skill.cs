using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour 
{
    private GameObject playerObject;
    private Button button;
    private bool isSelected;

    public void Awake()
    {
        this.playerObject = GameObject.FindGameObjectWithTag("Player");
        this.button = gameObject.GetComponent<Button>();
    }

    public GameObject PlayerObject { get { return this.playerObject; } }
    public Button Button { get { return this.button; } }
    public void Delete()
    {
        GameObject.FindWithTag("ExpManager").GetComponent<ExpManager>().Delete();
    }
}
