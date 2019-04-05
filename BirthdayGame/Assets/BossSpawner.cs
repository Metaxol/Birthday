﻿using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

    public GameObject EagleBoss;
    public GameObject OpossumBoss;
    public GameObject FrogBoss;

    private LevelManagement GetLevelManagement;

    private float BossSpawnTime = 15f;

    private void Update()
    {
        if (GetLevelManagement.Enemies_Killed > 50)
        {
            BossSpawnTime = 10f;
        }
    }

    private void Start()
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();

        EagleBoss.transform.localScale = new Vector3(5, 5, 1);
        OpossumBoss.transform.localScale = new Vector3(5, 5, 1);
        FrogBoss.transform.localScale = new Vector3(5, 5, 1);

        EagleBoss.GetComponent<EnemyFollow>().Enemy_Health = 2;
        EagleBoss.GetComponent<EnemyFollow>().EnemySpeed = 1;
        EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 1;

        OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health = 3;
        OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.5f;
        OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 1;

        FrogBoss.GetComponent<EnemyFollow>().Enemy_Health = 2;
        FrogBoss.GetComponent<EnemyFollow>().EnemySpeed = 0.5f;
        FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage = 2;

        StartCoroutine(IncreaseBoss());
        StartCoroutine(Spawning());
    }

    private void SpawnEnemy()
    {
        int random = Mathf.RoundToInt(Random.Range(1f, 3f));

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

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(BossSpawnTime);
            SpawnEnemy();
        }
    }
    
    private void UpgradeBoss()
    {
        if (gameObject.activeInHierarchy)
        {
            EagleBoss.GetComponent<EnemyFollow>().Enemy_Health += 1;
            EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 1;
            EagleBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.05f;

            OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health += 1;
            OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 1;
            OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.05f;

            FrogBoss.GetComponent<EnemyFollow>().Enemy_Health += 1;
            FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage += 1;
            FrogBoss.GetComponent<EnemyFollow>().EnemySpeed += 0.05f;
        }
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
