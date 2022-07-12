using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaGround : MonoBehaviour
{
    private bool IsOnStay;
    private float timeLeft;
    private void Start()
    {
        IsOnStay = false;
        timeLeft = 4;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            Destroy(gameObject);
        }
    }


    public virtual IEnumerator OnTriggerStay2D(Collider2D collider)
    {
        if (!IsOnStay && collider.gameObject.CompareTag("Player"))
        {
            IsOnStay = true;
            while (IsOnStay)
            {
                Player.instance.TakeDamage(15);
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }

    public virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            IsOnStay = false;
        }
    }
}
