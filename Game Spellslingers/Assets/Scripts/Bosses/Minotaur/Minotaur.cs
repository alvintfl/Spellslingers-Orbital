using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Enemy
{
    private bool isCasting;
    //private int prev;
    private Rigidbody2D rb;
    private new Collider2D collider;
    private Vector3 playerDirection;
    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        this.isCasting = false;
        //this.prev = -1;
        this.rb = GetComponent<Rigidbody2D>();
        this.collider = GetComponent<Collider2D>();
        this.playerDirection = Vector3.zero;
        this.anim = GetComponent<Animator>();
    }

    private void Update()
    {
        StartCasting();
    }

    private void StartCasting()
    {
        Vector3 playerDirection = Player.instance.transform.position - gameObject.transform.position;
        if ( !isCasting &&
            playerDirection.sqrMagnitude <= 20)
        {
            this.isCasting = true;
            SetMoveSpeed(0);
            CastCharge();
            //playerDirection.Normalize();
            //Invoke("Charge", 1f);
            //StartCoroutine(Charge());
            //rb.AddForce(playerDirection * 10, ForceMode2D.Impulse);
            //Debug.Log("Dashing");
            //Invoke("EndCharge", 2f);
            /*
            int skill = Random.Range(0, 3);
            while (skill == this.prev)
            {
                skill = Random.Range(0, 3);     
            }
            switch(skill)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            this.prev = skill; 
            */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Player.instance.AvoidRoll())
            {
                Player.instance.TakeDamage(10);
            }
            //EndCharge();
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            CancelInvoke();
            EndCharge();
        }
    }

    private void CastCharge()
    {
        this.anim.SetTrigger("StartCharge");
    }

    private void Charge()
    {
        this.playerDirection = Player.instance.transform.position - gameObject.transform.position;
        this.playerDirection.Normalize();
        DisableMovement();
        this.collider.isTrigger = true;
        rb.AddForce(this.playerDirection * 15, ForceMode2D.Impulse);
        this.anim.SetTrigger("Charge");
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
        EnableMovement();
        this.rb.velocity = Vector3.zero;
        if (this.collider.IsTouching(Player.instance.GetComponent<Collider2D>()))
        {
            ReverseCharge(this.playerDirection * -1);
            return;
        }
        this.anim.SetTrigger("EndCharge");
        this.collider.isTrigger = false;
        StopCasting();
    }

    private void StopCasting()
    {
        ResetMoveSpeed();
        this.isCasting = false;
    }
}
