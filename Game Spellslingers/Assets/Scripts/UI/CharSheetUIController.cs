using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUIController : MonoBehaviour
{
    // Unsubscribe event
    private delegate void DestroyEventHandler<T, U>(T sender, U eventArgs);
    private event DestroyEventHandler<CharSheetUIController, EventArgs> DestroyArcherStats;
    private event DestroyEventHandler<CharSheetUIController, EventArgs> DestroyMageStats;
    private event DestroyEventHandler<CharSheetUIController, EventArgs> DestroyWarriorStats;

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

    private void Awake()
    {
        CharacterSelectionUI charSelect = CharacterSelectionUI.instance;
        charSelect.ArcherSelected += InitialiseArcherStats;
        charSelect.MageSelected += InitialiseMageStats;
        charSelect.WarriorSelected += InitialiseWarriorStats;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        Player player = Player.instance;

        //Inital values for player of any class
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Level].text = ExpManager.instance.Level.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Health].text = player.GetCurrentHealth().ToString() + " / " + player.GetMaxHealth().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Damage].text = Arrow.Damage.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.MoveSpeed].text = player.GetMoveSpeed().ToString("#.00");
        player.HealthChange += UpdatePlayerHealth;
        player.MoveSpeedChange += UpdatePlayerMoveSpeed;
        ExpManager.LevelUp += UpdatePlayerLevel;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void InitialiseArcherStats(CharacterSelectionUI sender, EventArgs e)
    {
        Archer archer = (Archer) Player.instance;

        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.AvoidChance].text = archer.GetAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Pierce].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Projectiles].text = (archer.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Rate].text = archer.GetRate().ToString() + "s";

        archer.AvoidChanceChange += UpdatePlayerAvoidChance;
        archer.ShootChange += UpdatePlayerShoot;
        Arrow.ArrowChange += UpdatePlayerArrow;
        this.DestroyArcherStats += UnsubscribeArcher;
    }

    private void InitialiseMageStats(CharacterSelectionUI sender, EventArgs e)
    {
        this.DestroyMageStats += UnsubscribeMage;
    }

    private void InitialiseWarriorStats(CharacterSelectionUI sender, EventArgs e)
    {
        this.DestroyWarriorStats += UnsubscribeWarrior;
    }

    #region Update player stats
    private void UpdatePlayerHealth(Character sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Health].text = sender.GetCurrentHealth() + " / " + sender.GetMaxHealth();
    }

    private void UpdatePlayerMoveSpeed(Character sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.MoveSpeed].text = sender.GetMoveSpeed().ToString("#.00");
    }
    private void UpdatePlayerLevel(ExpManager sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)Stats.Level].text = sender.Level.ToString();
    }
    #endregion

    #region Update archer stats
    private void UpdatePlayerAvoidChance(Archer sender, EventArgs e)
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
    #endregion

    #region Unsubscribe
    private void Unsubscribe()
    {
        Player player = Player.instance;
        if (player != null)
        {
            player.HealthChange -= UpdatePlayerHealth;
            player.MoveSpeedChange -= UpdatePlayerMoveSpeed;
        }
        ExpManager.LevelUp -= UpdatePlayerLevel;
        DestroyArcherStats?.Invoke(this, EventArgs.Empty);
        DestroyMageStats?.Invoke(this, EventArgs.Empty);
        DestroyWarriorStats?.Invoke(this, EventArgs.Empty);
    }

    private void UnsubscribeArcher(CharSheetUIController sender, EventArgs e)
    {
        Archer archer = (Archer) Player.instance;
        if (archer != null)
        {
            archer.AvoidChanceChange -= UpdatePlayerAvoidChance;
            archer.ShootChange -= UpdatePlayerShoot;
        }
        Arrow.ArrowChange -= UpdatePlayerArrow;
        this.DestroyArcherStats -= UnsubscribeArcher;
    }

    private void UnsubscribeMage(CharSheetUIController sender, EventArgs e)
    {
        this.DestroyMageStats -= UnsubscribeMage;
    }

    private void UnsubscribeWarrior(CharSheetUIController sender, EventArgs e)
    {
        this.DestroyWarriorStats -= UnsubscribeWarrior;
    }
    #endregion
}
