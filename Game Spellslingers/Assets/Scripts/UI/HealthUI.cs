using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthbarCanvasPrefab;
    private HealthBar healthbar;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;


    void OnEnable() {
        SpawnManager.spawned += EnemyHealthBar;
    }

    void OnDisable()
    {
        SpawnManager.spawned += EnemyHealthBar;
    }

    private void Start()
    {
        GameObject healthbarCanvas = Instantiate(healthbarCanvasPrefab);
        this.healthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        GameObject playerObject = GameObjectManager.instance.allObjects.Find(x => x.CompareTag("Player"));
        healthbarCanvas.transform.SetParent(playerObject.transform);
        healthbarCanvas.transform.position = playerObject.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        this.playerHealth = playerObject.GetComponent<PlayerHealth>();
        this.healthbar.SetMaxHealth(PlayerHealth.maxHealth);
        this.healthbar.SetHealth(PlayerHealth.maxHealth);
        this.playerHealth.HealthChange += UpdatePlayerHealth;
    }

    void EnemyHealthBar(GameObject en) 
    {
        GameObject healthbarCanvas = Instantiate(healthbarCanvasPrefab);
        this.healthbar = healthbarCanvas.GetComponentInChildren<HealthBar>();
        healthbarCanvas.transform.SetParent(en.transform);
        healthbarCanvas.transform.position = en.transform.position;
        healthbarCanvas.transform.position += new Vector3(0, -1, 0);
        this.enemyHealth = en.GetComponent<EnemyHealth>();
        this.healthbar.SetMaxHealth(EnemyHealth.maxHealth);
        this.healthbar.SetHealth(EnemyHealth.maxHealth);
        this.enemyHealth.HealthChange += UpdateEnemyHealth;

    }

    public void UpdatePlayerHealth(object sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(PlayerHealth.maxHealth);
        this.healthbar.SetHealth(playerHealth.CurrentHealth);
    }

    public void UpdateEnemyHealth(GameObject en)
    {
        this.healthbar.SetMaxHealth(EnemyHealth.maxHealth);
        this.healthbar.SetHealth(EnemyHealth.CurrentHealth);    
    }


}
