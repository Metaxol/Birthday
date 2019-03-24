using UnityEngine;

public class EnemySpawning : MonoBehaviour {

    public GameObject Enemy;
    public float SpawnRate;

    public int EagleDamage;
    public int OpossumDamage;
    public int FrogDamage;

    public int EagleHealth;
    public int OpossumHealth;
    public int FrogHealth;

    public float EagleSpeed;
    public float OpossumSpeed;
    public float FrogSpeed;

    public float EagleSpawnRate;
    public float FrogSpawnRate;
    public float OpossumSpawnRate;

    void SpawnEnemies()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", 1f, SpawnRate);
    }

    void SetHarderGettingStuff()
    {
        switch (Enemy.name)
        {
            case "Eagle":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = EagleDamage;
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = EagleHealth;
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = EagleSpeed;
                
                break;
            case "Frog":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = FrogDamage;
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = FrogHealth;
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = FrogSpeed;
                break;
            case "Opossum":
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = OpossumDamage;
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = OpossumHealth;
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = OpossumSpeed;
                break;
        }
    }
}
