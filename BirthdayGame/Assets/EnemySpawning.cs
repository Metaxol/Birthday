using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    [HideInInspector] public GameObject Enemy;
    [HideInInspector] public float SpawnRate;

    void SpawnEnemies()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, SpawnRate);
    }
}
