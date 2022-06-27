using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUIController : MonoBehaviour
{
    //temporary variables
    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private GameObject guc;

    private enum Stats
    {
        Level = 9,
        Health = 10,
        Damage = 11,
        MoveSpeed = 12,
        AvoidChance = 13,
        Pierce = 14,
        Projectiles = 15,
        Rate = 17
    }

    private void Start()
    {
        Archer player = (Archer)Player.instance;

        //Inital values for UI
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Level].text = ExpManager.instance.Level.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Health].text = player.GetCurrentHealth().ToString() + " / " + player.GetMaxHealth().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Damage].text = arrow.GetComponent<Arrow>().GetDamage().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.MoveSpeed].text = player.GetMoveSpeed().ToString("#.00");
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.AvoidChance].text = player.GetAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Pierce].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Projectiles].text = (player.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Rate].text = player.GetRate().ToString() + "s";
        player.HealthChange += UpdatePlayerHealth;
        player.MoveSpeedChange += UpdatePlayerMoveSpeed;
        player.AvoidChanceChange += UpdatePlayerAvoidChance;
        player.ShootChange += UpdatePlayerShoot;
        ExpManager.LevelUp += UpdatePlayerLevel;

    }

    private void OnDestroy()
    {
        Archer player = (Archer)Player.instance;
        if (player != null)
        {
            player.HealthChange -= UpdatePlayerHealth;
            player.MoveSpeedChange -= UpdatePlayerMoveSpeed;
            player.AvoidChanceChange -= UpdatePlayerAvoidChance;
            player.ShootChange -= UpdatePlayerShoot;
        }
        Arrow.ArrowChange -= UpdatePlayerArrow;
        ExpManager.LevelUp -= UpdatePlayerLevel;
    }

    private void UpdatePlayerHealth(Character sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Health].text = sender.GetCurrentHealth() + " / " + sender.GetMaxHealth();
    }

    private void UpdatePlayerMoveSpeed(Character sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.MoveSpeed].text = sender.GetMoveSpeed().ToString("#.00");
    }

    private void UpdatePlayerAvoidChance(Player sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.AvoidChance].text = sender.GetAvoidChance().ToString() + "%";
    }

    private void UpdatePlayerShoot(Archer sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Projectiles].text = (sender.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Rate].text = sender.GetRate().ToString() + "s";
    }

    private void UpdatePlayerArrow(Arrow sender, ArrowArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Damage].text = e.Damage.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Pierce].text = e.Pierce.ToString();
    }

    private void UpdatePlayerLevel(ExpManager sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Level].text = sender.Level.ToString();
    }
}
