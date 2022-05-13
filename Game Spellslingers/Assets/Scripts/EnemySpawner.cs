using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemyReference;

    private GameObject spawnedEnemy;

    [SerializeField]
    private Transform pos0, pos1, pos2, pos3, pos4, pos5;

    private int randomIndex;
    private int randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            randomIndex = Random.Range(0, enemyReference.Length);
            Debug.Log(randomIndex);
            randomSide = Random.Range(0, 5);

            spawnedEnemy = Instantiate(enemyReference[randomIndex]);
            if (randomSide == 0)
            {
                spawnedEnemy.transform.position = pos0.position;
            }
            else if (randomSide == 1)
            {
                spawnedEnemy.transform.position = pos1.position;
            }
            else if (randomSide == 2)
            {
                spawnedEnemy.transform.position = pos2.position;
            }
            else if (randomSide == 3)
            {
                spawnedEnemy.transform.position = pos3.position;
            }
            else if (randomSide == 4)
            {
                spawnedEnemy.transform.position = pos4.position;
            }
            else if (randomSide == 5)
            {
                spawnedEnemy.transform.position = pos5.position;
            }
        }
    }
}
