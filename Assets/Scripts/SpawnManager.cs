using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnObjects;
    private float _spawnTimer;

    void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= 2)
        {
            Spawn();
            _spawnTimer = 0;
        }
    }

    private void Spawn()
    {
        var spawnObject = spawnObjects[Random.Range(0, spawnObjects.Length)];
        Instantiate(spawnObject, spawnObject.transform.position, spawnObject.transform.rotation);
    }
}