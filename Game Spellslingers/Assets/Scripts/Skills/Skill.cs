using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * <summary>
 * The base skill class.
 * </summary>
 */
public abstract class Skill : MonoBehaviour 
{
    /**
     * <summary>
     * Button that gives player the skill when clicked.
     * </summary>
     */
    private Button button;
    private int level = 1;
    private int maxLevel = 1;
    public static event EventHandler Selected;
    public event EventHandler MaxedOut;

    public Skill(int maxLevel)
    {
        this.maxLevel = maxLevel;
    }

    public virtual void Start()
    {
        this.button = gameObject.GetComponent<Button>();
    }

    public Button Button { get { return this.button; } }

    /**
     * <summary>
     * If this skill is selected, increase its level.
     * If the skill is maxed out, destory the object 
     * holding it.
     * </summary>
     */
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
