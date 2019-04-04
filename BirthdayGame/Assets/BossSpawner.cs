using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

    public GameObject EagleBoss;
    public GameObject OpossumBoss;
    public GameObject FrogBoss;

    private void Start()
    {
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
    }

    private void SpawnEnemy()
    {
        float random = Random.Range(1f, 3f);

        if(random == 1)
        {
            
        }
    }

    private IEnumerator IncreaseBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
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
    }
}
