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

    void OnEnable()
    {
        SpawnManager.spawned += EnemyHealthBar;
        EnemyBloodMother.SpawnSpidersInfo += EnemyHealthBar;
    }

    void OnDisable()
    {
        Player.instance.HealthChange -= UpdatePlayerHealth;
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
        Player player = Player.instance;
        GameObject playerObject = player.gameObject;
        healthbarCanvas.transform.SetParent(playerObject.transform);
        healthbarCanvas.transform.position = playerObject.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        this.healthbar.SetMaxHealth(player.GetMaxHealth());
        this.healthbar.SetHealth(player.GetMaxHealth());
        Player.instance.HealthChange += UpdatePlayerHealth;
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
        Character character = en.GetComponent<Character>();
        enemyHealthbar.SetMaxHealth(character.GetMaxHealth());
        enemyHealthbar.SetHealth(character.GetMaxHealth());
        character.HealthChange +=
            (Character sender, EventArgs e) =>
            {
                enemyHealthbar.SetMaxHealth(sender.GetMaxHealth());
                enemyHealthbar.SetHealth(sender.GetCurrentHealth());
            };
    }

    public void UpdatePlayerHealth(Character sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(sender.GetMaxHealth());
        this.healthbar.SetHealth(sender.GetCurrentHealth());
    }
}
