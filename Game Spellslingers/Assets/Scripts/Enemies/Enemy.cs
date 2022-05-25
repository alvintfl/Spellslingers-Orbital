using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float _enemyDamage;
    private int exp;
    private bool IsCollidedStay;
    public static event EventHandler<DropExpEventArgs> DropExp;

    private void Start()
    {
        this.Health.DiedInfo += OnDropExp;
    }

    public Enemy(float ed, int exp)
    {
        this._enemyDamage = ed;
        this.exp = exp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!Player.instance.Avoidance.avoidRoll())
            {
                Player.instance.Health.TakeDamage(this._enemyDamage);
            }
        }
    }

    private IEnumerator OnCollisionStay2D(Collision2D collision)
    {
        if (!IsCollidedStay && collision.gameObject.CompareTag("Player"))
        {
            IsCollidedStay = true;
            while (IsCollidedStay)
            {
                if (!Player.instance.Avoidance.avoidRoll()) 
                { 
                    Player.instance.Health.TakeDamage(this._enemyDamage);
                }
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IsCollidedStay = false;
        }
    }

    public void OnDropExp()
    {
        DropExpEventArgs args = new DropExpEventArgs();
        args.Exp = this.exp;
        DropExp?.Invoke(this, args);
        Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

public class DropExpEventArgs : EventArgs
{
    public int Exp { get; set; }
}
