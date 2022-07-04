using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningField : MonoBehaviour
{
    private float damage;
    private float activeTime;
    private WaitForSeconds rate;
    private new Collider2D collider;
    private bool isActivated;

    private void Awake()
    {
        this.damage = 2f;
        this.activeTime = 5f;
        this.isActivated = false;
        this.rate = new WaitForSeconds(1f);
        this.collider = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Activate());
        Invoke("Deactivate", this.activeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(this.damage);
        }
    }

    private void DealDamage()
    {
        this.collider.enabled = true;
    }
    private void StopDealDamage()
    {
        this.collider.enabled = false;
    }
    private IEnumerator Activate()
    {
        this.isActivated = true;
        while (isActivated)
        {
            DealDamage();
            yield return this.rate;
            StopDealDamage();
        }
        yield return null;
    }

    private void Deactivate()
    {
        this.isActivated = false;
        StopDealDamage();
        gameObject.SetActive(false);
    }

    public void IncreaseRange()
    {
        gameObject.transform.localScale *= 1.05f;
    }

    public void IncreaseDamage()
    {
        this.damage++;
    }

    public void IncreaseRate(float secs)
    {
        this.activeTime += secs;
    }
}
