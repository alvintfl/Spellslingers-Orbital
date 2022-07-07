using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Enemy
{
    [SerializeField] private GameObject spikesPrefab;
    [SerializeField] private GameObject loot;
    private GameObject spikes;
    private Animator anim;
    private Rigidbody2D rb;
    private new Collider2D collider;
    private Vector3 playerDirection;
    private bool isCasting;
    private int prev;

    public override void Awake()
    {
        base.Awake();
        this.collider = GetComponent<Collider2D>();
        this.playerDirection = Vector3.zero;
        this.anim = GetComponent<Animator>();
        this.isCasting = false;
        this.spikes = Instantiate(spikesPrefab);
        this.spikes.SetActive(false);
        this.prev = -1;
        Vector2 spawnPosition = new Vector2(-24, -438);
        gameObject.transform.position = spawnPosition;
    }

    public override void Start()
    {
        base.Start();
        this.rb = GetComponent<Rigidbody2D>();
        AudioManager.instance.Play("MinotaurSpawn");
    }

    private void Update()
    {
        StartCasting();
    }

    private void StartCasting()
    {
        Vector3 playerDirection = Player.instance.transform.position - gameObject.transform.position;
        if ( !isCasting && playerDirection.sqrMagnitude <= 20)
        {
            this.isCasting = true;
            SetMoveSpeed(0);
            int skill = Random.Range(0, 2);
            while (skill == this.prev)
            {
                skill = Random.Range(0, 2);     
            }
            switch(skill)
            {
                case 0:
                    CastCharge();
                    break;
                case 1:
                    CastSlam();
                    break;
            }
            this.prev = skill; 
        }
    }

    #region Charge Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.instance.TakeDamage(15);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            EndCharge();
        }
    }

    private void CastCharge()
    {
        this.anim.SetTrigger("StartCharge");
        AudioManager.instance.Play("MinotaurFeetDrag");
    }

    private void Charge()
    {
        this.playerDirection = Player.instance.transform.position - gameObject.transform.position;
        this.playerDirection.Normalize();
        DisableMovement();
        this.collider.isTrigger = true;
        rb.AddForce(this.playerDirection * 15, ForceMode2D.Impulse);
        this.anim.SetTrigger("Charge");
        AudioManager.instance.Play("MinotaurCharge");
    }

    private void ReverseCharge(Vector3 direction)
    {
        DisableMovement();
        this.collider.isTrigger = true;
        if (direction.x > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (direction.x < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        rb.AddForce(direction * 15, ForceMode2D.Impulse);
    }

    private void EndCharge()
    {
        this.rb.velocity = Vector3.zero;
        EnableMovement();
        if (this.collider.IsTouching(Player.instance.GetComponent<Collider2D>()))
        {
            ReverseCharge(this.playerDirection * -1);
            return;
        }
        this.collider.isTrigger = false;
        this.anim.SetTrigger("EndCharge");
        AudioManager.instance.Stop("MinotaurCharge");
        StopCasting();
    }
    #endregion

    private void CastSlam()
    {
        this.anim.SetTrigger("Slam");
    }

    private void SummonSpikes()
    {
        AudioManager.instance.Play("MinotaurSlam");
        AudioManager.instance.Play("MinotaurRumble");
        Vector3 playerDirection = Player.instance.transform.position - gameObject.transform.position;
        this.spikes.transform.up = playerDirection;
        playerDirection.Normalize();
        this.spikes.transform.position = gameObject.transform.position + playerDirection * 15;
        this.spikes.SetActive(true);
    }

    private void StopCasting()
    {
        ResetMoveSpeed();
        this.isCasting = false;
    }

    public override void Die()
    {
        AudioManager.instance.Stop("MinotaurCharge");
        AudioManager.instance.Play("MinotaurDeath");
        GameObject loot = Instantiate(this.loot);
        loot.transform.position = this.transform.position;
        base.Die();
    }

    public override IEnumerator HandleStatusEffect(StatusEffect statusEffect)
    {
        if (statusEffect is Slow || statusEffect is Stun)
        {
            yield return null;
        } else
        {
            yield return base.HandleStatusEffect(statusEffect);
        }
    }
}
