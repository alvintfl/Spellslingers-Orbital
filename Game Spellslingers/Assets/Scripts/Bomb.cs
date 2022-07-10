using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that represents the bomber enemy.
 * </summary>
 */
public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;
    private Rigidbody2D rb;
    private float stoppingMagnitude;
    private bool isSlowDown;
    private WaitForSeconds wait;
    public delegate void ExplodeEventHandler<T, U>(T sender, U eventArgs);
    public event ExplodeEventHandler<Bomb, EventArgs> Exploded;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.stoppingMagnitude = 1f;
        this.isSlowDown = false;
        this.wait = new WaitForSeconds(0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.isSlowDown && collision.gameObject.CompareTag("Player"))
        {
            this.isSlowDown = false;
            StartCoroutine(SlowingDown());
        }
    }

    private IEnumerator SlowingDown()
    {
        while (this.rb.velocity.magnitude > this.stoppingMagnitude)
        {
            this.rb.velocity *= 0.9f;
            yield return this.wait;
        }
        yield return null;
    }

    public void SlowDown()
    {
        this.isSlowDown = true;
    }

    /**
     * <summary>
     * Create an explosion.
     * </summary>
     */
    private void Explode()
    {
        StopAllCoroutines();
        GameObject ex = Instantiate(explosionPrefab);
        ex.transform.position = gameObject.transform.position;
        OnExploded();
    }

    protected virtual void OnExploded()
    {
        Exploded?.Invoke(this, EventArgs.Empty);
    }
}
