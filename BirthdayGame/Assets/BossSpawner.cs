using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossSpawner : MonoBehaviour {

    public GameObject EagleBoss;
    public GameObject OpossumBoss;
    public GameObject FrogBoss;

    private LevelManagement GetLevelManagement;

    private float BossSpawnTime = 15f;

    private static float TestEagleHealth = 3f;

    private void Update()
    {
        if (GetLevelManagement.Enemies_Killed > 50)
        {
            BossSpawnTime = 10f;
        }
    }

    private void Start() //Fix the BossEnemy adding stuff
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();

        EagleBoss.transform.localScale = new Vector3(5, 5, 1);
        OpossumBoss.transform.localScale = new Vector3(5, 5, 1);
        FrogBoss.transform.localScale = new Vector3(5, 5, 1);

        EagleBoss.GetComponent<EnemyFollow>().Enemy_Health = 3;
        EagleBoss.GetComponent<EnemyFollow>().EnemySpeed = 1;
        EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 2;

        OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health = 3;
        OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.7f;
        OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 3;

        FrogBoss.GetComponent<EnemyFollow>().Enemy_Health = 5;
        FrogBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.4f;
        FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 2;

        StartCoroutine(Spawning());
    }
    
    private void SpawnEnemy()
    {
        int random = Mathf.RoundToInt(Random.Range(1f, 3f));

        UpgradeBoss(); 

        if (random == 1)
        {
            Instantiate(EagleBoss, transform.position, transform.rotation);
        }
        else if (random == 2)
        {
            Instantiate(OpossumBoss, transform.position, transform.rotation);
        }
        else if (random == 3)
        {
            Instantiate(FrogBoss, transform.position, transform.rotation);
        }      
    }

    public void SetOriginalState()
    {
        EagleBoss.GetComponent<EnemyFollow>().Enemy_Health = 3;
        EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 2;
        EagleBoss.GetComponent<EnemyFollow>().EnemySpeed = 1f;

        OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health = 3;
        OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 3;
        OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.05f;

        FrogBoss.GetComponent<EnemyFollow>().Enemy_Health = 5;
        FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 2;
        FrogBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.04f;
    }

    private void SetHarderGettingStuff_Boss()
    {
        EagleBoss.GetComponent<EnemyFollow>().Enemy_Health = TestEagleHealth;
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(BossSpawnTime);
            SpawnEnemy();
        }
    }
    
    private void UpgradeBoss() //Fix EnemyBoss Upgrading, then items, then menu/intro + quirky stuff then adjustments to course of game.
    { 
        EagleBoss.GetComponent<EnemyFollow>().Enemy_Health += 0.6f;
        EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 0.6f;
        EagleBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.003f;

        OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health += 0.6f;
        OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 0.6f;
        OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.003f;

        FrogBoss.GetComponent<EnemyFollow>().Enemy_Health += 0.6f;
        FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 0.6f;
        FrogBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.003f;
    }

    private IEnumerator IncreaseBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            UpgradeBoss();
        }
    }
}
