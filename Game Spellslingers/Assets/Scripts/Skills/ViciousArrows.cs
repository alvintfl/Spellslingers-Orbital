using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViciousArrows : Skill
{
    private Shoot shoot;
    public ViciousArrows() : base(10) { }
    private void Start()
    {
        this.shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseDamage(10);
        });
    }
}
