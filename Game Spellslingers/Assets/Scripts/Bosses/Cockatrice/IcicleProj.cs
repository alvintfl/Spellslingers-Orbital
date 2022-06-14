using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleProj : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Transform player;
    private Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        player = Player.instance.gameObject.transform;
    }

    void Update()
    {

        if (startPos.y - transform.position.y > 40)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!Player.instance.Avoidance.avoidRoll())
            {
                Player.instance.Health.TakeDamage(30f);
            }
        }
        Invoke("DestroyProjectile", 1.5f);
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
