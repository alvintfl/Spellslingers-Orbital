using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Camera _camera;
    [SerializeField] private List<GameObject> secondWave;
    //[SerializeField] private int _offsetX;
    //[SerializeField] private int _offsetY;
    private int count;
    private int[] directions;
    private bool isNextWave;

    GameObject spawnedEnemy;

    //private int _randomX;
    //private int _randomY;

    public delegate void SpawnDelegate(GameObject enemy);
    public static event SpawnDelegate spawned;

    private void Start()
    {
        this.count = 0;
        this.directions = new int[] { 0, 1, 2, 3 };
        this.isNextWave = false;
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        StartNextWave();
    }

    private IEnumerator Spawn()
    {
        if (count % 4 == 0)
        {
            //Fisher-Yates shuffle
            for (int i = directions.Length - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = this.directions[i];
                this.directions[i] = this.directions[j];
                this.directions[j] = temp;
            }
        }
        while (true)
        {
            if (Time.timeSinceLevelLoad<90)
            {
                yield return new WaitForSeconds(Random.Range(2, 5));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(1, 3));
            }
            int randomEnemyID = Random.Range(0, enemies.Count);
            int direction = this.directions[count % 4];
            Vector2 coords = direction == 0
                ? LeftSpawn() : direction == 1
                ? RightSpawn() : direction == 2
                ? TopSpawn() : BottomSpawn();
            Vector2 position = _camera.ScreenToWorldPoint(coords);
            spawnedEnemy = Instantiate(enemies[randomEnemyID], position, Quaternion.identity) as GameObject;
            spawned(spawnedEnemy);
            count++;
        }
    }

    private void StartNextWave()
    {
        if (Time.timeSinceLevelLoad > 90 && isNextWave == false)
        {
            this.isNextWave = true;
            this.enemies.AddRange(this.secondWave);
        }
    }

    Vector2 LeftSpawn()
    {
        int x = 0;
        int y = Random.Range(0, Screen.height);
        return new Vector2(x, y);
    }
    Vector2 RightSpawn()
    {
        int x = Screen.width;
        int y = Random.Range(0, Screen.height);
        return new Vector2(x, y);
    }
    Vector2 TopSpawn()
    {
        int x = Random.Range(0, Screen.width);
        int y = Screen.height;
        return new Vector2(x, y);
    }

    Vector2 BottomSpawn()
    {
        int x = Random.Range(0, Screen.width);
        int y = 0;
        return new Vector2(x, y);
    }
}
