using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public GameObject Enemy;
    public float SpawnRate;

    void SpawnEnemies()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, SpawnRate);
    }
}
