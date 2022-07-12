using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class BombKingAttack : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private int poolSize;
    private ObjectPool<GameObject> bombPool;
    private int prev;
    private WaitForSeconds wait;
    private Vector2[] cardinalDirections;
    private Vector2[] ordinalDirections;

    private void Start()
    {
        this.bombPool = new ObjectPool<GameObject>(
        () => {
            GameObject bombObject = Instantiate(bombPrefab);
            Bomb bomb = bombObject.GetComponent<Bomb>();
            bomb.Exploded +=
                (Bomb sender, EventArgs e) => this.bombPool.Release(bombObject);
            return bombObject;
        },
        x => x.SetActive(true),
        x => x.SetActive(false),
        x => Destroy(x),
        false, this.poolSize, this.poolSize + 1);
        this.prev = -1;
        this.wait = new WaitForSeconds(3f);
        this.cardinalDirections = new Vector2[] 
        {
            new Vector2(-1,0), new Vector2(1,0), new Vector2(0,-1), new Vector2(0,1)
        };
        this.ordinalDirections = new Vector2[] 
        {
            new Vector2(-1,-1), new Vector2(-1,1), new Vector2(1,-1), new Vector2(1,1)
        };
        StartCoroutine(Attack());

        GetComponent<Character>().Death += StopThrowBomb;
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            int skill = Random.Range(0, 3);
            while (skill == this.prev)
            {
                skill = Random.Range(0, 3);     
            }
            switch(skill)
            {
                case 0:
                    ThrowBombAtPlayer();
                    break;
                case 1:
                    ThrowBombsEverywhere(this.cardinalDirections);
                    break;
                case 2:
                    ThrowBombsEverywhere(this.ordinalDirections);
                    break;
            }
            this.prev = skill; 
            yield return this.wait;
        }
    }

    private void ThrowBombAtPlayer()
    {
        Vector3 playerDirection = Player.instance.transform.position - gameObject.transform.position;
        playerDirection.Normalize();
        GameObject bomb = this.bombPool.Get();
        if (bomb != null)
        {
            bomb.transform.position = gameObject.transform.position;
            Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
            rb.AddForce(playerDirection * 8, ForceMode2D.Impulse);
            bomb.GetComponent<Bomb>().SlowDown();
        }
    }

    private void ThrowBombsEverywhere(Vector2[] arr)
    {
        foreach (Vector2 vector in arr)
        {
            GameObject bomb = this.bombPool.Get();
            if (bomb != null)
            {
                bomb.transform.position = gameObject.transform.position;
                Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
                rb.AddForce(vector * 4, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDisable()
    {
        GetComponent<Character>().Death -= StopThrowBomb;
    }

    private void StopThrowBomb(Character sender, EventArgs e)
    {
        StopAllCoroutines();
    }
}
