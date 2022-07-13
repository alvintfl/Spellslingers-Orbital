using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningField : MonoBehaviour
{
    public float Damage { get; set; }
    public float ActiveTime { get; private set; }
    private WaitForSeconds rate;
    private new Collider2D collider;
    private bool isActivated;

    private void Awake()
    {
        this.Damage = 2f;
        this.ActiveTime = 5f;
        this.isActivated = false;
        this.rate = new WaitForSeconds(1f);
        this.collider = GetComponent<Collider2D>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Activate());
        Invoke("Deactivate", this.ActiveTime);
        AudioManager.instance.Play("LightningField");
    }

    private void OnDestroy()
    {
        AudioManager.instance.Stop("LightningField");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(this.Damage);
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
        AudioManager.instance.Stop("LightningField");
        this.isActivated = false;
        StopDealDamage();
        gameObject.SetActive(false);
    }

    public void IncreaseRange()
    {
        gameObject.transform.localScale *= 1.05f;
    }

    public void IncreaseDuration()
    {
        this.ActiveTime++;
    }

    public float GetRange()
    {
        return gameObject.transform.localScale.x;
    }
}
