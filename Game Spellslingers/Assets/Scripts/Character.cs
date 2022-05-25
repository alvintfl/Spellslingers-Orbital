using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Movement movement;
    private Health health;
    private Avoidance avoid;
    public virtual void Awake()
    {
        this.movement = gameObject.GetComponent<Movement>();
        this.health = gameObject.GetComponent<Health>();
        this.avoid = gameObject.GetComponent<Avoidance>();
    }
    public Movement Movement { get { return this.movement; } }
    public Health Health { get { return this.health; } }
    public Avoidance Avoidance { get { return this.avoid; } }
}
