using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public GameObject Enemy;
    public float SpawnRate;

    public int EagleDamage;
    public int OpossumDamage;
    public int FrogDamage;

    void SpawnEnemies()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, SpawnRate);
    }

    void SetEnemyDamage()
    {
        switch (Enemy.name)
        {
            case "Eagle":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = EagleDamage;
                break;
            case "Frog":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = FrogDamage;
                break;
            case "Opossum":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = OpossumDamage;
                break;
        }
    }
}
