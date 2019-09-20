using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;
    public float SpawnInterval = 0.5f;
    public int SpawnTotal = 5;

    public int SpawnCount = 0;
    private bool SpawningActive = true;

    private double _nextSpawn;

    // Update is called once per frame
    void Update()
    {
        if (!SpawningActive) return;

        if (Time.fixedTime > _nextSpawn && SpawnCount < SpawnTotal)
        {
            Spawn();
            _nextSpawn = Time.fixedTime + SpawnInterval;
            SpawningActive = SpawnCount < SpawnTotal;
        }
    }

    private void Spawn()
    {
        var gameObject = Instantiate(Prefab, transform);
        GridPlacer.Place(gameObject, transform);
        SpawnCount++;
    }
}