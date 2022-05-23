using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private Camera _camera;
    [SerializeField] private int _offsetX;
    [SerializeField] private int _offsetY;

    GameObject _spawnedEnemy;

    private int _randomX;
    private int _randomY;
    private bool next;

    public delegate void SpawnDelegate(GameObject enemy);
    public static event SpawnDelegate spawned;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.timeSinceLevelLoad);
        if (!next && Time.timeSinceLevelLoad > 10  )
        {
            next = true;
            //StartCoroutine(Spawn());
        }

        
    }
    // when i spawn enemies , healthUi listens create hp bar



    IEnumerator Spawn()
    {
        while (true)
        {
            if (Time.timeSinceLevelLoad<40)
            {
                yield return new WaitForSeconds(Random.Range(1, 5));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 2));
            }
            //yield return new WaitForSeconds(Random.Range(1, 5));
            int randomEnemyID = Random.Range(0, _enemies.Length);
            Vector2 position = GetRandomCoordinates();
            _spawnedEnemy = Instantiate(_enemies[randomEnemyID], position, Quaternion.identity) as GameObject;
            spawned(_spawnedEnemy);
        }
    }

    Vector2 GetRandomCoordinates() {
        /*
        _randomX = Random.Range(0 + _offsetX, Screen.width + _offsetX);
        _randomY = Random.Range(0 + _offsetY, Screen.height + _offsetY);
        Vector2 coords = new Vector2(_randomX, _randomY);
        */
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        _randomX = Random.Range(-1, 2);
        _randomY = Random.Range(-1, 2);
        while (_randomX == 0 && _randomY == 0)
        {
            _randomX = Random.Range(-1, 2);
            _randomY = Random.Range(-1, 2);
        }
        x += _randomX == 0 ? 0 : _randomX == 1 ? x : -x;
        y += _randomY == 0 ? 0 : _randomY == 1 ? y : -y;
        Vector2 coords = new Vector2(x, y);
        Vector2 screenToWorldPosition = _camera.ScreenToWorldPoint(coords);
        return screenToWorldPosition;
    }
}
