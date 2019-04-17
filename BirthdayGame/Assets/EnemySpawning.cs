using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour {

    public GameObject Enemy;
    private float SpawnRate;

    static public float[] EagleStuff = new float[4]; //Spawn, Damage, Health, Speed
    static public float[] FrogStuff = new float[4];
    static public float[] OpossumStuff = new float[4];

    public GameObject SpawnedEnemy;


    void SpawnEnemies()
    {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

    private void Update()
    {
        SetHarderGettingStuff();
    }

    private void Start()
    {
        EagleStuff = new float[] { 8f, 1, 1, 0.7f };
        FrogStuff = new float[] { 8, 1, 2, 0.25f };
        OpossumStuff = new float[] { 8, 2, 1, 0.5f };
        StartCoroutine(Spawn());
    }

    void SetHarderGettingStuff()
    {
        switch (Enemy.name)
        {
            case "Eagle":
                SpawnRate = EagleStuff[0];
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = EagleStuff[1];
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = EagleStuff[2];
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = EagleStuff[3];
                break;
            case "Frog":
                SpawnRate = FrogStuff[0];
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = FrogStuff[1];
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = FrogStuff[2];
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = FrogStuff[3];
                break;
            case "Opossum":
                SpawnRate = OpossumStuff[0];
                Enemy.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = OpossumStuff[1];
                Enemy.GetComponent<EnemyFollow>().Enemy_Health = OpossumStuff[2];
                Enemy.GetComponent<EnemyFollow>().EnemySpeed = OpossumStuff[3];
                break;
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnRate);
            switch (Enemy.name)
            {
                case "Eagle":
                    EagleStuff[1] += EagleStuff[0] / 20f;
                    EagleStuff[2] += EagleStuff[0] / 10f;
                    EagleStuff[3] += EagleStuff[0] / (25f / 0.08f);
                    break;
                case "Opossum":
                    OpossumStuff[1] += OpossumStuff[0] / 10f;
                    OpossumStuff[2] += OpossumStuff[0] / 10f;
                    OpossumStuff[3] += OpossumStuff[0] / (25f / 0.05f);
                    break;
                case "Frog":
                    FrogStuff[1] += FrogStuff[0] / 20f;
                    FrogStuff[2] += FrogStuff[0] / 5f;
                    FrogStuff[3] += FrogStuff[0] / (25f / 0.05f);
                    break; //fix this shit jesus christ
            }
            SpawnEnemies();
        }
    }
}
