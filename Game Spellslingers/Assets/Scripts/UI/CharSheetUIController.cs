using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUIController : MonoBehaviour
{

    [SerializeField]
    private GameObject arrow;

    [SerializeField] 
    private GameObject guc;

    private void Start()
    {


        GameObject playerObject = Archer.instance.gameObject;
        Health playerHealth = playerObject.GetComponent<Health>();
        Movement playerMovement = playerObject.GetComponent<PlayerMovement>();
        Avoidance playerAvoidance = playerObject.GetComponent<Avoidance>();
        PlayerShoot playerShoot = playerObject.GetComponent<PlayerShoot>();

        //Inital values for UI
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[9].text = "1";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[10].text = playerHealth.CurrentHealth.ToString() + " / " + playerHealth.MaxHealth.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[11].text = arrow.GetComponent<Arrow>().GetDamage().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[12].text = playerMovement.GetMoveSpeed().ToString("#.00");
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[13].text = playerAvoidance.GetAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[14].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[15].text = (playerShoot.GetProjectileCount() - 1).ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[17].text = playerShoot.GetRate().ToString() + "s";
        playerHealth.HealthChange += UpdatePlayerHealth;
        playerMovement.MoveSpeedChange += UpdatePlayerMoveSpeed;
        playerAvoidance.AvoidChanceChange += UpdatePlayerAvoidChance;
        playerShoot.PlayerShootChange += UpdatePlayerShoot;
        ExpManager.LevelUp += UpdatePlayerLevel;
        Arrow.ArrowChange += UpdatePlayerArrow;
    }

    private void OnDestroy()
    {
        if (Player.instance != null)
        {
            Player.instance.Health.HealthChange -= UpdatePlayerHealth;   
            Player.instance.Movement.MoveSpeedChange -= UpdatePlayerMoveSpeed;
            Player.instance.Avoidance.AvoidChanceChange -= UpdatePlayerAvoidChance;
            Player.instance.GetComponent<Archer>().Shoot.PlayerShootChange -= UpdatePlayerShoot;
        }
        Arrow.ArrowChange -= UpdatePlayerArrow;
    }

    void Update()
    {
        /*
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[10].text = playercHealth.CurrentHealth.ToString() + " / " + playerHealth.MaxHealth.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[11].text = arrow.GetComponent<Arrow>().GetDamage().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[12].text = playerMovement.GetMoveSpeed().ToString("#.00");
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[13].text = playerAvoidance.GetAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[14].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[15].text = archerProjectiles.ToString();
        */
    }

    private void UpdatePlayerHealth(object sender, HealthArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[10].text = e.CurrentHealth + " / " + e.MaxHealth;
    }

    private void UpdatePlayerMoveSpeed(object sender, MoveSpeedArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[12].text = e.MoveSpeed.ToString("#.00");
    }

    private void UpdatePlayerAvoidChance(object sender, AvoidanceArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[13].text = e.AvoidChance.ToString() + "%";
    }

    private void UpdatePlayerShoot(object sender, PlayerShootArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[15].text = e.ProjectileCount.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[17].text = e.Rate.ToString() + "s";
    }

    private void UpdatePlayerArrow(object sender, ArrowArgs e)
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[11].text = e.Damage.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[14].text = e.Pierce.ToString();
    }

    private void UpdatePlayerLevel(object sender, EventArgs e) 
    {
        int level = Int32.Parse(
            gameObject.GetComponentsInChildren<TextMeshProUGUI>()[9].text);
        level++;
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[9].text = level.ToString();
    }
}
