using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject healthbarCanvasPrefab;
    private HealthBar healthbar;
    private PlayerHealth playerHealth;

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
        this.playerHealth.HealthChange += UpdateHealth;
    }

    public void UpdateHealth(object sender, EventArgs e)
    {
        this.healthbar.SetMaxHealth(PlayerHealth.maxHealth);
        this.healthbar.SetHealth(playerHealth.CurrentHealth);
    }
}
