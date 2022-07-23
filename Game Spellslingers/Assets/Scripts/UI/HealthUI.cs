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
    private GameObject bossHealthCanvas;
    private HealthBar healthbar;

    private void OnEnable()
    {
        Spawner.spawned += EnemyHealthBar;
        Spawner.spawnedBoss += BossHealthBar;
        EnemyBloodMother.SpawnSpidersInfo += EnemyHealthBar;
    }

    private void OnDisable()
    {
        Player.instance.HealthChange -= UpdatePlayerHealth;
        Spawner.spawned -= EnemyHealthBar;
        Spawner.spawnedBoss -= BossHealthBar;
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
        healthbarCanvas.transform.position += new Vector3(0, -1.3f, 0);
        this.healthbar.SetMaxHealth(player.GetMaxHealth());
        this.healthbar.SetHealth(player.GetMaxHealth());
        Player.instance.HealthChange += UpdatePlayerHealth;

        this.bossHealthCanvas = gameObject.transform.GetChild(0).gameObject;
    }

    /**
     * <summary>
     * Create the enemy's healthbar and update it 
     * each time their health changes.
     * </summary>
     */
    private void EnemyHealthBar(GameObject en)
    {
        GameObject healthbarCanvas = Instantiate(enemyHealthbarCanvasPrefab);
        HealthBar enemyHealthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        healthbarCanvas.transform.SetParent(en.transform);
        healthbarCanvas.transform.position = en.transform.position;
        float y = en.GetComponent<SpriteRenderer>().bounds.size.y;
        healthbarCanvas.transform.position += new Vector3(0, -1 * (float) Math.Sqrt(y), 0);
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

    /**
     * <summary>
     * Create the boss's healthbar and update it 
     * each time their health changes.
     * </summary>
     */
    private void BossHealthBar(GameObject boss)
    {
        HealthBar bossHealthbar = this.bossHealthCanvas.GetComponentInChildren<HealthBar>();
        Character character = boss.GetComponent<Character>();
        bossHealthbar.SetMaxHealth(character.GetMaxHealth());
        bossHealthbar.SetHealth(character.GetMaxHealth());
        character.HealthChange +=
            (Character sender, EventArgs e) =>
            {
                bossHealthbar.SetMaxHealth(sender.GetMaxHealth());
                bossHealthbar.SetHealth(sender.GetCurrentHealth());
            };
        character.Death += 
            (Character sender, EventArgs e) =>
            {
                this.bossHealthCanvas.SetActive(false);
            };
        this.bossHealthCanvas.SetActive(true);
    }

    private void UpdatePlayerHealth(Character sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(sender.GetMaxHealth());
        this.healthbar.SetHealth(sender.GetCurrentHealth());
    }

    public void DisableHealthUI()
    {
        gameObject.SetActive(false);
    }
}
