using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSheetUIController : MonoBehaviour
{
    private GameObject playerObject;
    private Health playerHealth;
    private Movement playerMovement;
    private Avoidance playerAvoidance;
    private int archerProjectiles;

    //temporary variables
    [SerializeField]
    private GameObject arrow;

    [SerializeField] 
    private GameObject guc;


    void Awake()
    {

    }

    void OnEnable() 
    {
        
    }

    void OnDisable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        playerObject = Archer.instance.gameObject;
        playerHealth = playerObject.GetComponent<Health>();
        playerMovement = playerObject.GetComponent<PlayerMovement>();
        playerAvoidance = playerObject.GetComponent<Avoidance>();
        archerProjectiles = playerObject.GetComponent<Archer>().Projectiles;


    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[10].text = playerHealth.CurrentHealth.ToString() + " / " + playerHealth.MaxHealth.ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[11].text = arrow.GetComponent<Arrow>().GetDamage().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[12].text = playerMovement.GetMoveSpeed().ToString("#.00");
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[13].text = playerAvoidance.getAvoidChance().ToString() + "%";
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[14].text = Arrow.getPierceMax().ToString();
        gameObject.GetComponentsInChildren<TextMeshProUGUI>()[15].text = archerProjectiles.ToString();
    }
}
