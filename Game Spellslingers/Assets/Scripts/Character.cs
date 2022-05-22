using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Movement movement;
    private Health health;
    public virtual void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();    
    }
    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }
}
