using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour 
{
    private GameObject playerObject;
    private Button button;
    public static event EventHandler Selected;

    public void Awake()
    {
        this.playerObject = GameObject.FindGameObjectWithTag("Player");
        this.button = gameObject.GetComponent<Button>();
    }

    public GameObject PlayerObject { get { return this.playerObject; } }
    public Button Button { get { return this.button; } }

    protected virtual void OnSelected(EventArgs e)
    {
        Selected?.Invoke(this, e);
    }
}
