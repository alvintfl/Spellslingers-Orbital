using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vigor : Skill
{
    public Vigor() : base(10) { }
    private void Start()
    { 
        Button.onClick.AddListener(() =>
        {
            OnSelected(EventArgs.Empty);
            Player player = PlayerObject.GetComponent<Player>();
            int increase = 20;
            Player.maxHealth += increase;
            player.CurrentHealth += increase;
        });
    }
}
