using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * A class that spawns enemies.
 * </summary>
 */
public class SpawnManager : MonoBehaviour
{
    /**
     * <summary>
     * The list of enemies that can be spawned.
     * </summary>
     */
    [SerializeField] private List<GameObject> enemies;

    /**
     * <summary>
     * Reserve list of enemies to be spawned when 
     * the player gets stronger.
     * </summary>
     */
    [SerializeField] private List<GameObject> secondWave;
    [SerializeField] private Camera _camera;

    /**
     * <summary>
     * Current position with respect to the screen
     * that spawner is spawning in.
     * </summary>
     */
    private int count;

    /**
     * <summary>
     * An array containing numbers representing top, down, left and right.
     * </summary>
     */
    private int[] directions;
    private bool isNextWave;

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

    /**
     * <summary>
     * Spawns an enemy every random few seconds. Every 4 enemies
     * are each spawned on top, bottom, left and right of the screen.
     * However the order of which position to spawn is random.
     * </summary>
     */
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
            GameObject spawnedEnemy = Instantiate(enemies[randomEnemyID], position, Quaternion.identity);
            spawned(spawnedEnemy);
            count++;
        }
    }

    private void StartNextWave()
    {
        if (Time.timeSinceLevelLoad > 50 && isNextWave == false)
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
