using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreShots : Skill
{
    private Shoot shoot;
    [SerializeField] private GameObject projectile;
    void Start()
    {
        this.shoot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shoot>();
        Button.onClick.AddListener(() =>
        {
            Select();
            this.shoot.AddProjectiles();
        });
    }
}
