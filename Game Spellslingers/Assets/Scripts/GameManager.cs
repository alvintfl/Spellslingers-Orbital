using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    private ExpManager expManager;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        this.expManager = GameObject.FindGameObjectWithTag("ExpManager").GetComponent<ExpManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
