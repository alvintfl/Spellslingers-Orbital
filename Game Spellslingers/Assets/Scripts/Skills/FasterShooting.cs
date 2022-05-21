using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterShooting : Skill
{
    private Shoot shoot;
    public FasterShooting() : base(10) { }
    private void Start()
    {
        this.shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            this.shoot.IncreaseRate(0.05f);
        });
    }

}
