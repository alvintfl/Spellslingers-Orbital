using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private float damage;
    private float damageReduction;
    private new Collider2D collider;
    private float directionMagnitude;
    private Animator anim;
    private bool isUpgraded;
    private bool isFirstAttack;

    public SpriteRenderer sr;

    private float critMulti;

    private void Awake()
    {
        this.critMulti = 1f;
        this.damage = 10;
        this.damageReduction = 1;
        this.collider = GetComponent<Collider2D>();
        this.collider.enabled = false;
        this.directionMagnitude = 3.5f;
        this.isUpgraded = false;
        this.isFirstAttack = true;
        this.sr = GetComponent<SpriteRenderer>();
        this.anim = GetComponent<Animator>();
        transform.SetParent(Camera.main.transform);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(this.damage * critMulti);
        }
    }

    private void OnEnable()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = Player.instance.transform.position;
        Vector2 mouseDirection = mousePosition - playerPosition;
        mouseDirection.Normalize();
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        gameObject.transform.position = playerPosition + mouseDirection * this.directionMagnitude;
        if (isFirstAttack)
        {
            this.anim.SetBool("isFirstAttack", true);
        } else
        {
            this.anim.SetBool("isFirstAttack", false);
        }
        AudioManager.instance.Play("Lightning");
    }

    private void Activate()
    {
        this.collider.enabled = true;
    }

    private void Stop()
    {
        this.collider.enabled = false;
        gameObject.SetActive(false);
        if (isUpgraded)
        {
            if (isFirstAttack)
            {
                isFirstAttack = false;
                gameObject.SetActive(true);
            } else
            {
                isFirstAttack = true;
            }
        }
    }

    public void IncreaseRange()
    {
        gameObject.transform.localScale *= 1.1f;
        this.directionMagnitude += 0.25f;
    }

    public void DecreaseRange()
    {
        gameObject.transform.localScale /= 1.1f;
        this.directionMagnitude -= 0.25f;
    }

    public float GetRange()
    {
        return gameObject.transform.localScale.x;
    }

    public void Upgrade()
    {
        this.isUpgraded = true;
        this.damageReduction = 0.8f;
        this.Damage = this.damage;
    }

    public float Damage { 
        get { return this.damage; } 
        set { this.damage = value * this.damageReduction; }
    }

    public float GetBaseDamage()
    {
        return this.damage / this.damageReduction; 
    }

    public void DecideCrit(bool isCrit)
    {
        if (isCrit)
        {
            this.critMulti = 2f;
        }
        else 
            this.critMulti = 1f;
    }

    public void CritColor()
    {
        sr.color = Color.cyan;
    }

    public void NonCritColor()
    {
        sr.color = Color.white;
    }
}
