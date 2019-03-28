using UnityEngine;
using System.Collections.Generic;

public class LevelManagement : MonoBehaviour {

    public int Enemies_Killed;

    private PlayerShooting GetPlayerShooting;
    private Player_Monster GetPlayer_Monster;
    private PlayerMovement GetPlayerMovement;

    int choosing;

    private GameObject UpgradeHolder;
    [SerializeField] private List<GameObject> UpgradeSprites = new List<GameObject>();

    private void Awake()
    {
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (i.gameObject.name == "UpgradeHolder_Tiles")
            {
                UpgradeSprites.Add(i.gameObject);
            }
        }

        for(int c = 0; c < UpgradeSprites.Count; c++)
        {
            switch (c)
            {
                case 0:
                    //UpgradeSprites[c]
                    break;
            }
        }

        GetPlayerShooting = FindObjectOfType<PlayerShooting>();
        GetPlayer_Monster = FindObjectOfType<Player_Monster>();
        GetPlayerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        //print(choosing);
        switch (Enemies_Killed)
        {
            case 2:
                ChoosingUpgrade();
                break;
        }
    }

    private void ChoosingUpgrade() //This is absolute lidl code, not gonna work at all. (Have to work on it, definitely!)
    {
        foreach (GameObject x in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (x.name == "UpgradeHolder" || x.name == "Panel")
            {
                UpgradeHolder = x;
                UpgradeHolder.SetActive(true);
            }
            else if(x.name == "Panel")
            {
                x.SetActive(true);
            }
            Time.timeScale = 0;
        }

        if (UpgradeHolder.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                choosing -= 1;
                if (choosing < 0)
                {
                    choosing = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                choosing += 1;
                if (choosing > 6)
                {
                    choosing = 6;
                }
            }
        }       
    }           

    #region Use these functions to upgrade the Player's Attributes.
    private void UpgradingSize(Vector3 increaseBulletSize)
    {
        GetPlayerShooting.Player_Bullets.transform.localScale += increaseBulletSize;
        //Default is (2,3,0)
        //Do 0.25 - steps (normal)
    }

    private void UpgradeHealth(int increaseHealth)
    {
        GetPlayer_Monster.PlayerHealth += increaseHealth;
        //Default is 3
        //Do 1 - steps (normal)
    }

    private void UpgradePlayerDamage(int increaseDamage)
    {
        EnemyFollow.PlayerToEnemy_Damage += increaseDamage;
        //Default is 1
        //Do 1 - steps (normal)
    }

    private void UpgradePlayerSpeed(float increaseSpeed)
    {
        GetPlayerMovement.PlayerMovSpeed += increaseSpeed;
        //Default is 100
        //Do 10 - steps (normal)
    }

    private void UpgradeBulletSpeed_Range(float increaseRange, float increaseSpeed)
    {
        BulletController.BulletRange += increaseRange;
        //Default is 0.5f
        //Do 0.05 - steps (normal)
        GetPlayerShooting.BulletSpeed += increaseSpeed;
        //Default is 0.1f
        //Do 0.02 - steps (normal)
    }

    private void ReduceTime(float reduceCooldown, float reduceDelay)
    {
        GetPlayerShooting.cooldown -= reduceCooldown;
        //Default is 2
        //Do 0.2 - steps (normal)
        GetPlayerShooting.Delay -= reduceDelay;
        //Default is 1
        //Do 0.1 - steps (normal)
    }
    #endregion
}
