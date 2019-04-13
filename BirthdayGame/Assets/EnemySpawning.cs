using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySpawning : MonoBehaviour {

    public GameObject Enemy;
    private float SpawnRate;

    static public float[] EagleStuff = new float[4]; //Spawn, Damage, Health, Speed
    static public float[] FrogStuff = new float[4];
    static public float[] OpossumStuff = new float[4];

    void SpawnEnemies()
    {
        GameObject Enemy_ = Instantiate(Enemy, transform.position, transform.rotation);

        switch (Enemy.name)
        {
            case "Eagle":
                GameObject.Find("EagleDamage_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemyToPlayer_Damage.ToString();
                GameObject.Find("EagleHealth_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().Enemy_Health.ToString();
                GameObject.Find("EagleSpeed_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemySpeed.ToString();
                break;
            case "Frog":
                GameObject.Find("FrogDamage_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemyToPlayer_Damage.ToString();
                GameObject.Find("FrogHealth_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().Enemy_Health.ToString();
                GameObject.Find("FrogSpeed_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemySpeed.ToString();
                break;
            case "Opossum":
                GameObject.Find("OpossumDamage_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemyToPlayer_Damage.ToString();
                GameObject.Find("OpossumHealth_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().Enemy_Health.ToString();
                GameObject.Find("OpossumSpeed_Text").GetComponent<Text>().text = Enemy_.GetComponent<EnemyFollow>().EnemySpeed.ToString();
                break;
        }
    }

    private void Update()
    {
        SetHarderGettingStuff();
    }

    private void Start() //Make "Unlimited-Gamemode" Option.
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
            SpawnEnemies();
        }
    }
}
