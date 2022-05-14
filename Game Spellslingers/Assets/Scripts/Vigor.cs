using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vigor : Skill
{
    private void Start()
    {
        Button.onClick.AddListener(() =>
        {
            Player player = PlayerObject.GetComponent<Player>();
            int increase = (int) (Player.maxHealth * 0.2);
            Player.maxHealth += increase;
            player.SetHealth(player.CurrentHealth + increase);
            Delete();
        });
    }
}
