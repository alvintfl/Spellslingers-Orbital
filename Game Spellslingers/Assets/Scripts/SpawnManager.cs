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

    public delegate void SpawnDelegate(GameObject enemy);
    public static event SpawnDelegate spawned;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // when i spawn enemies , healthUi listens create hp bar



    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            int randomEnemyID = Random.Range(0, _enemies.Length);
            Vector2 position = GetRandomCoordinates();
            _spawnedEnemy = Instantiate(_enemies[randomEnemyID], position, Quaternion.identity) as GameObject;
            spawned(_enemies[randomEnemyID]);
        }
    }

    Vector2 GetRandomCoordinates() {
        _randomX = Random.Range(0 + _offsetX, Screen.width - _offsetX);
        _randomY = Random.Range(0 + _offsetY, Screen.height - _offsetY);
        Vector2 coords = new Vector2(_randomX, _randomY);
        Vector2 screenToWorldPosition = _camera.ScreenToWorldPoint(coords);
        return screenToWorldPosition;
    }
}
