using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour 
{
    private Button button;
    private int level = 1;
    private int maxLevel = 1;
    public static event EventHandler Selected;
    public event EventHandler MaxedOut;

    public Skill(int maxLevel)
    {
        this.maxLevel = maxLevel;
    }

    public Skill() { }

    public virtual void Start()
    {
        this.button = gameObject.GetComponent<Button>();
    }

    public Button Button { get { return this.button; } }

    protected virtual void OnSelected(EventArgs e)
    {
        Selected?.Invoke(this, e);
        this.level++;
        if (this.level > this.maxLevel)
        {
            OnMaxedOut(EventArgs.Empty);
            GameObject.Destroy(this.gameObject);
        }
    }

    protected virtual void OnMaxedOut(EventArgs e)
    {
        MaxedOut?.Invoke(this, e);
    }

    public override string ToString()
    {
        return this.level.ToString();
    }
}
