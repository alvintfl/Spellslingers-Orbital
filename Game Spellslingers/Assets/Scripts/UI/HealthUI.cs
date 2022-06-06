using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class responsible for creating and updating 
 * the healthbar for all characters.
 * </summary>
 */
public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthbarCanvasPrefab;
    [SerializeField] private GameObject enemyHealthbarCanvasPrefab;
    private HealthBar healthbar;
    private Health playerHealth;


    void OnEnable() {
        SpawnManager.spawned += EnemyHealthBar;
        EnemyBloodMother.SpawnSpidersInfo += EnemyHealthBar;
    }

    void OnDisable()
    {
        this.playerHealth.HealthChange -= UpdatePlayerHealth;
        SpawnManager.spawned -= EnemyHealthBar;
        EnemyBloodMother.SpawnSpidersInfo -= EnemyHealthBar;
    }

    /**
     * <summary>
     * Create the player's healthbar.
     * </summary>
     */
    private void Start()
    {
        GameObject healthbarCanvas = Instantiate(healthbarCanvasPrefab);
        this.healthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        GameObject playerObject = Player.instance.gameObject;
        healthbarCanvas.transform.SetParent(playerObject.transform);
        healthbarCanvas.transform.position = playerObject.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        this.playerHealth = playerObject.GetComponent<Health>();
        this.healthbar.SetMaxHealth(this.playerHealth.MaxHealth);
        this.healthbar.SetHealth(this.playerHealth.MaxHealth);
        this.playerHealth.HealthChange += UpdatePlayerHealth;
    }

    /**
     * <summary>
     * Create the enemy's healthbar and update it 
     * each time their health changes.
     * </summary>
     */
    void EnemyHealthBar(GameObject en) 
    {
        GameObject healthbarCanvas = Instantiate(enemyHealthbarCanvasPrefab);
        HealthBar enemyHealthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        healthbarCanvas.transform.SetParent(en.transform);
        healthbarCanvas.transform.position = en.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        Health enemyHealth = en.GetComponent<Health>();
        enemyHealthbar.SetMaxHealth(enemyHealth.MaxHealth);
        enemyHealthbar.SetHealth(enemyHealth.MaxHealth);
        enemyHealth.HealthChange +=
            (object sender, HealthArgs e) =>
            {
                enemyHealthbar.SetMaxHealth(e.MaxHealth);
                enemyHealthbar.SetHealth(e.CurrentHealth);
            };
    }

    public void UpdatePlayerHealth(object sender, HealthArgs e)
    {
        this.healthbar.SetMaxHealth(e.MaxHealth);
        this.healthbar.SetHealth(e.CurrentHealth);
    }
}
