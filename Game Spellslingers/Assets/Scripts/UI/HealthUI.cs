using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthbarCanvasPrefab;
    [SerializeField] private GameObject enemyHealthbarCanvasPrefab;
    private HealthBar healthbar;
    private Health playerHealth;


    void OnEnable() {
        SpawnManager.spawned += EnemyHealthBar;
    }

    void OnDisable()
    {
        this.playerHealth.HealthChange -= UpdatePlayerHealth;
        SpawnManager.spawned -= EnemyHealthBar;
    }

    private void Start()
    {
        GameObject healthbarCanvas = Instantiate(healthbarCanvasPrefab);
        this.healthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        GameObject playerObject = GameObjectManager.instance.allObjects.Find(x => x.CompareTag("Player"));
        healthbarCanvas.transform.SetParent(playerObject.transform);
        healthbarCanvas.transform.position = playerObject.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        this.playerHealth = playerObject.GetComponent<Health>();
        this.healthbar.SetMaxHealth(this.playerHealth.MaxHealth);
        this.healthbar.SetHealth(this.playerHealth.MaxHealth);
        this.playerHealth.HealthChange += UpdatePlayerHealth;
    }

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
            (object sender, EventArgs e) =>
            {
                enemyHealthbar.SetMaxHealth(enemyHealth.MaxHealth);
                enemyHealthbar.SetHealth(enemyHealth.CurrentHealth);
            };
    }

    public void UpdatePlayerHealth(object sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(this.playerHealth.MaxHealth);
        this.healthbar.SetHealth(playerHealth.CurrentHealth);
    }
}
