using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * <summary>
 * A class that spawns enemies.
 * </summary>
 */
public class Spawner : MonoBehaviour
{
    /**
     * <summary>
     * The list of enemies that can be spawned in the current wave.
     * </summary>
     */
    [SerializeField] private List<GameObject> currentWave;

    /**
     * <summary>
     * Reserve list of enemies to be spawned when 
     * the player gets stronger.
     * </summary>
     */
    [SerializeField] private List<GameObject> nextWaves;
    [SerializeField] private List<GameObject> bossPrefabs;
    private new Camera camera;

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

    /**
     * <summary>
     * Minimum interval to spawn an enemy.
     * </summary>
     */
    [SerializeField] private int minSpawnTime;

    /**
     * <summary>
     * Maximum interval to spawn an enemy.
     * </summary>
     */
    [SerializeField] private int maxSpawnTime;

    /**
     * <summary>
     * Player level to start spawning faster.
     * </summary>
     */
    [SerializeField] private int requiredLevel;
    [SerializeField] private int startLevel;

    public delegate void SpawnDelegate(GameObject enemy);
    public static event SpawnDelegate spawned;
    public static event SpawnDelegate spawnedBoss;

    private enum Direction
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
    }

    private void Start()
    {
        this.count = 0;
        this.directions = new int[] { 0, 1, 2, 3 };
        this.camera = Camera.main;
        ExpManager.LevelUp += StartNextWave;
        ExpManager.LevelUp += IncreaseSpawnRate;
        Player.instance.Death += StopSpawning;
        SummoningCircle.Summon += SpawnBoss;
        SummoningCircle.Summon += StopSpawning;
        Item.Spawned += Spawn;
        //StartCoroutine(Spawn());
    }

    private void OnDestroy()
    {
        ExpManager.LevelUp -= StartNextWave;
        ExpManager.LevelUp -= IncreaseSpawnRate;
        SummoningCircle.Summon -= SpawnBoss;
        SummoningCircle.Summon -= StopSpawning;
        Player.instance.Death -= StopSpawning;
        Item.Spawned -= Spawn;
    }

    public int StartLevel { get { return this.startLevel; } }

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
            yield return new WaitForSeconds(Random.Range(this.minSpawnTime, this.maxSpawnTime));
            int randomEnemyID = Random.Range(0, currentWave.Count);
            int direction = this.directions[count % 4];
            Vector2 coords;
            switch (direction)
            {
                case (int)Direction.Left:
                    coords = LeftSpawn();
                    break;
                case (int)Direction.Right:
                    coords = RightSpawn();
                    break;
                case (int)Direction.Top:
                    coords = TopSpawn();
                    break;
                case (int)Direction.Bottom:
                    coords = BottomSpawn();
                    break;
                default:
                    coords = LeftSpawn();
                    break;
            }
            Vector2 position = camera.ScreenToWorldPoint(coords);
            GameObject spawnedEnemy = Instantiate(currentWave[randomEnemyID], position, Quaternion.identity);
            spawned(spawnedEnemy);
            count++;
        }
    }

    private void Spawn(Item item, EventArgs e)
    {
        StartCoroutine(Spawn());
    }

    /**
     * <summary>
     * Start spawning enemies for the next wave when the player
     * reaches the required level.
     * </summary>
     */
    private void StartNextWave(ExpManager sender, EventArgs e)
    {
        if (this.nextWaves.Count != 0)
        {
            GameObject waveObject = this.nextWaves[0];
            EnemyWave wave = waveObject.GetComponent<EnemyWave>();
            if (sender.Level == wave.Level)
            {
                this.currentWave.AddRange(wave.Enemies);
                this.nextWaves.Remove(waveObject);
            }
        }
    }

    private void IncreaseSpawnRate(ExpManager sender, EventArgs e)
    {
        if (sender.Level == this.requiredLevel)
        {
            this.minSpawnTime = 1;
            this.maxSpawnTime = 3;
        }
    }

    private void StopSpawning(object sender, EventArgs e)
    {
        StopAllCoroutines();
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

    private void SpawnBoss(SummoningCircle summon, EventArgs e)
    {
        if (this.bossPrefabs.Count != 0)
        {
            GameObject bossObject = this.bossPrefabs[0];
            GameObject boss = Instantiate(bossObject);
            spawnedBoss(boss);
            this.bossPrefabs.Remove(bossObject);
        }
    }
}
