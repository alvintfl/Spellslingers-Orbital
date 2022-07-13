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

    private enum PlayerStats
    {
        CharacterText = 0, 
        ClassText = 1, Class = 2,
        LevelText = 3, Level = 4,
        HealthText = 5, Health = 6,
        MoveSpeedText = 7, MoveSpeed = 8,
        AttackDamageText = 9, AttackDamage = 10,
        AttackSpeedText = 11, AttackSpeed = 12,
    }

    private enum ArcherStats
    {
        AvoidChanceText = 13, AvoidChance = 14,
        PierceText = 15, Pierce = 16,
        ProjectilesText = 17, Projectiles = 18,
        LifeStealText = 19, LifeSteal = 20,
    }

    private enum MageStats
    {
        LightningRangeText = 13, LightningRange = 14, 
        SpellDamageText = 15, SpellDamage = 16,
        DamageTakenText = 17, DamageTaken = 18,
        LightningOrbText = 19, LightningOrb = 20,
        LightningOrbDamageText = 21, LightningOrbDamage = 22,
        LightningOrbRotationSpeedText = 23, LightningOrbRotationSpeed = 24,
        LightningFieldDamageText = 25, LightningFieldDamage = 26,
        LightningFieldRangeText = 27, LightningFieldRange = 28,
        LightningFieldDurationText = 29, LightningFieldDuration = 30,
        HealOnKillText = 31, HealOnKill = 32,
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
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Class].text = player.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Level].text = ExpManager.instance.Level.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.MoveSpeed].text = player.GetMoveSpeed().ToString("#.00");
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
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = archer.GetCurrentHealth().ToString() + " / " + archer.GetMaxHealth().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamageText].text = "Arrow Damage";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamage].text = Arrow.Damage.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackSpeed].text = archer.GetRate().ToString() + "s";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.AvoidChanceText].text = "Avoid Chance";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.AvoidChance].text = archer.GetAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.PierceText].text = "Pierce";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.Pierce].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.ProjectilesText].text = "Additional Projectiles";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.Projectiles].text = (archer.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.LifeStealText].text = "Heal per\nEnemy Hit";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.LifeSteal].text = Lifesteal.healAmount.ToString();   

        archer.HealthChange += UpdatePlayerHealth;
        archer.AvoidChanceChange += UpdateArcherAvoidChance;
        archer.ShootChange += UpdateArcherShoot;
        Arrow.ArrowChange += UpdateArcherArrow;
        this.DestroyArcherStats += UnsubscribeArcher;
    }

    private void InitialiseMageStats(CharacterSelectionUI sender, EventArgs e)
    {
        Mage mage = (Mage)Player.instance;
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = Math.Round(mage.GetCurrentHealth(), 2).ToString() + " / " + mage.GetMaxHealth().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamageText].text = "Lightning Damage";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamage].text = Math.Round(mage.GetLightningDamage(), 2).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackSpeed].text = mage.GetRate().ToString() + "s";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningRangeText].text = "Lightning Range";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningRange].text = "+" + Math.Round((mage.GetLightningRange() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.SpellDamageText].text = "Spell Damage";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.SpellDamage].text = "+" + Math.Round((mage.GetDamageDealtMultiplier() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.DamageTakenText].text = "Damage Taken";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.DamageTaken].text = "+" + Math.Round((mage.GetDamageTakenMultiplier() - 1) * 100, 2).ToString() + "%";

        // Lightning Orb
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbText].text = "Lightning Orbs";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrb].text = mage.GetLightningOrbCount().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbDamageText].text = "Lightning Orb Damage";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbDamage].text = Math.Round(mage.GetLightningOrbDamage(), 2).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbRotationSpeedText].text = "Lightning Orb Rotation Speed";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbRotationSpeed].text = Math.Round(mage.GetLightningOrbRotationSpeed(), 2).ToString();

        // Lightning Field
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDamageText].text = "Lightning Field Damage";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDamage].text = Math.Round(mage.GetLightningStormDamage(), 2).ToString();  
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldRangeText].text = "Lightning Field Range";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldRange].text = "+" + Math.Round((mage.GetLightningStormRange() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDurationText].text = "Lightning Field Duration";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDuration].text = mage.GetLightningStormDuration().ToString() + "s";

        // Others
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.HealOnKillText].text = "Heal on Kill";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.HealOnKill].text = mage.GetHealOnKill().ToString();

        mage.HealthChange += UpdatePlayerHealth;
        mage.CastChange += UpdateMageCast;
        this.DestroyMageStats += UnsubscribeMage;
    }

    private void InitialiseWarriorStats(CharacterSelectionUI sender, EventArgs e)
    {
        Warrior warrior = (Warrior)Player.instance;
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = warrior.GetCurrentHealth().ToString() + " / " + warrior.GetMaxHealth().ToString();


        warrior.HealthChange += UpdatePlayerHealth;
        this.DestroyWarriorStats += UnsubscribeWarrior;
        
    }

    #region Update player stats
    private void UpdatePlayerHealth(Character sender, EventArgs e)
    {
        Player player = Player.instance;
        if (player.ToString() == "Warrior")
        {
            Warrior warrior = (Warrior)Player.instance;
            if (warrior.IsFrenzy())
            {
                gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = "??? / ???";
            }
            else
            {
                gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = sender.GetCurrentHealth() + " / " + sender.GetMaxHealth();
            }
        }
        else
        {
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Health].text = sender.GetCurrentHealth() + " / " + sender.GetMaxHealth();
        }
    }

    private void UpdatePlayerMoveSpeed(Character sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.MoveSpeed].text = sender.GetMoveSpeed().ToString("#.00");
    }
    private void UpdatePlayerLevel(ExpManager sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.Level].text = sender.Level.ToString();
    }
    #endregion

    #region Update archer stats
    private void UpdateArcherAvoidChance(Archer sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.AvoidChance].text = sender.GetAvoidChance().ToString() + "%";
    }

    private void UpdateArcherShoot(Archer sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.Projectiles].text = (sender.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackSpeed].text = sender.GetRate().ToString() + "s";
    }

    private void UpdateArcherArrow(Arrow sender, ArrowArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamage].text = e.Damage.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.Pierce].text = e.Pierce.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)ArcherStats.LifeSteal].text = e.LifeSteal.ToString();
    }
    #endregion

    #region Update warrior stats



    #endregion

    #region Update mage stats
    private void UpdateMageCast(Mage sender, EventArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackDamage].text = Math.Round(sender.GetLightningDamage(), 2).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)PlayerStats.AttackSpeed].text = sender.GetRate().ToString() + "s";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningRange].text = "+" + Math.Round((sender.GetLightningRange() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.SpellDamage].text = "+" + Math.Round((sender.GetDamageDealtMultiplier() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.DamageTaken].text = "+" + Math.Round((sender.GetDamageTakenMultiplier() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrb].text = sender.GetLightningOrbCount().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbDamage].text = Math.Round(sender.GetLightningOrbDamage(), 2).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningOrbRotationSpeed].text = Math.Round(sender.GetLightningOrbRotationSpeed(), 2).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDamage].text = Math.Round(sender.GetLightningStormDamage(), 2).ToString();  
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldRange].text = "+" + Math.Round((sender.GetLightningStormRange() - 1) * 100, 2).ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.LightningFieldDuration].text = sender.GetLightningStormDuration().ToString() + "s";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[(int)MageStats.HealOnKill].text = sender.GetHealOnKill().ToString();
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

        CharacterSelectionUI charSelect = CharacterSelectionUI.instance;
        charSelect.ArcherSelected -= InitialiseArcherStats;
        charSelect.MageSelected -= InitialiseMageStats;
        charSelect.WarriorSelected -= InitialiseWarriorStats;
    }

    private void UnsubscribeArcher(CharSheetUIController sender, EventArgs e)
    {
        Archer archer = (Archer) Player.instance;
        if (archer != null)
        {
            archer.AvoidChanceChange -= UpdateArcherAvoidChance;
            archer.ShootChange -= UpdateArcherShoot;
        }
        Arrow.ArrowChange -= UpdateArcherArrow;
        this.DestroyArcherStats -= UnsubscribeArcher;
    }

    private void UnsubscribeMage(CharSheetUIController sender, EventArgs e)
    {
        Mage mage = (Mage) Player.instance;
        if (mage != null)
        {
            mage.CastChange -= UpdateMageCast;
        }
        this.DestroyMageStats -= UnsubscribeMage;
    }

    private void UnsubscribeWarrior(CharSheetUIController sender, EventArgs e)
    {
        this.DestroyWarriorStats -= UnsubscribeWarrior;
    }
    #endregion
}
