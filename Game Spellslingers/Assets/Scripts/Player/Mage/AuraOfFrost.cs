using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraOfFrost : MonoBehaviour
{
    [SerializeField] Slow slow;

    private void Awake()
    {
        gameObject.transform.SetParent(Camera.main.transform);
        gameObject.transform.position = Player.instance.transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.StartHandleStatusEffect(this.slow);
        }
    }
}
