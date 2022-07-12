using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBubble : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject lavaGroundPrefab;

    private Transform player;
    private Vector2 target;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.gameObject.transform;
        target = new Vector2(player.position.x, player.position.y);
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Player.instance.TakeDamage(40f);
        }
        anim.SetBool("Hit", true);
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Instantiate(lavaGroundPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
