using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragonewt : Enemy
{
    [SerializeField] private GameObject ringOfFirePrefab;
    [SerializeField] private GameObject firePillarPrefab;
    private Animator anim;
    private PolygonCollider2D bodyCollider;
    private EdgeCollider2D squatCollider;
    private bool isCasting;
    private int prev;

    public override void Awake()
    {
        base.Awake();
        this.anim = GetComponent<Animator>(); 
        this.bodyCollider = GetComponent<PolygonCollider2D>();
        this.squatCollider = GetComponent<EdgeCollider2D>();
        this.isCasting = false;
        this.prev = -1;
    }

    private void Update()
    {
        StartCasting();
    }

    private void StartCasting()
    {
        if ( !isCasting &&
            (gameObject.transform.position - Player.instance.transform.position).sqrMagnitude <= 20)
        {
            this.isCasting = true;
            SetMoveSpeed(0);
            int skill = Random.Range(0, 3);
            while (skill == this.prev)
            {
                skill = Random.Range(0, 3);     
            }
            switch(skill)
            {
                case 0:
                    CastFirePillar();
                    break;
                case 1:
                    CastFireStomp();
                    break;
                case 2:
                    CastRingOfFire();
                    break;
            }
            this.prev = skill; 
        }
    }

    private void StopCasting()
    {
        ResetMoveSpeed();
        this.isCasting = false;
    }

    private void CastRingOfFire()
    {
        this.anim.SetTrigger("RingOfFire");
    }

    private void CastFireStomp()
    {
        this.anim.SetTrigger("FireStomp");
    }

    private void CastFirePillar()
    {
        this.anim.SetTrigger("FirePillar");
    }

    private void StartSquating()
    {
        this.bodyCollider.enabled = false;
        this.squatCollider.enabled = true;
    }

    private void StopSquating()
    {
        this.bodyCollider.enabled = true;
        this.squatCollider.enabled = false;
    }

    private void SummonRingOfFire()
    {
        GameObject ring = Instantiate(this.ringOfFirePrefab);
        ring.transform.position = gameObject.transform.position;
    }

    private void SummonFirePillar()
    {
        Instantiate(this.firePillarPrefab);
    }
}
