using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraOfFrost : MonoBehaviour
{
    [SerializeField] Slow slow;
    private WaitForSeconds wait;
    private new Collider2D collider;

    private void Awake()
    {
        this.collider = GetComponent<Collider2D>();
        this.wait = new WaitForSeconds(0.1f);
        StartCoroutine(Activate());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Working");
            StartCoroutine(collision.gameObject.GetComponent<Enemy>().HandleStatusEffect(this.slow));
        }
    }

    private IEnumerator Activate()
    {
        while (true)
        {
            StartSlow();
            yield return this.wait;
            StartSlow();
        }
    }

    private void StartSlow()
    {
        this.collider.enabled = true;
    }
    private void StopSlow()
    {
        this.collider.enabled = false;
    }
}
