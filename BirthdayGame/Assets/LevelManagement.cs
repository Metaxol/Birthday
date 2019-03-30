using UnityEngine;
using System.Collections.Generic;

public class LevelManagement : MonoBehaviour {

    public int Enemies_Killed;

    private PlayerShooting GetPlayerShooting;
    private Player_Monster GetPlayer_Monster;
    private PlayerMovement GetPlayerMovement;

    float choosing;

    [HideInInspector] public float[] RangeChoose = new float[2];

    private Animator UpgradeHolderAnimator;

    private GameObject UpgradeHolder;
    public GameObject Panel;
    [SerializeField] private List<GameObject> UpgradeSprites = new List<GameObject>();

    private bool Control;

    public void ResetVariables()
    {
        Enemies_Killed = 0;
        GetPlayer_Monster.PlayerHealth = 3;
        EnemyFollow.PlayerToEnemy_Damage = 1;
        GetPlayerMovement.PlayerMovSpeed = 100;
        BulletController.BulletRange = 0.5f;
        GetPlayerShooting.BulletSpeed = 0.1f;
        GetPlayerShooting.Player_Bullets.transform.localScale = new Vector3(2, 3, 0);
        GetPlayerShooting.cooldown = 2;
        GetPlayerShooting.Delay = 1;
        EnemySpawning.EagleStuff = new float[] { 2, 1, 1, 2 };
        EnemySpawning.FrogStuff = new float[] { 2, 1, 2, 1 };
        EnemySpawning.OpossumStuff = new float[] { 2, 2, 1, 1 };
    }

    private void Awake()
    {
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (i.name == "UpgradeHolder_Tiles")
            {
                UpgradeSprites.Add(i.gameObject);

                /* 0 = Damage
                 * 1 = Health
                 * 2 = ReduceTime
                 * 3 = IncreaseBulletSize
                 * 4 = IncreaseBulletSpeed/Range
                 * 5 = IncreasePlayerSpeed
                 */
            }
            else if(i.name == "UpgradeHolder")
            {
                UpgradeHolder = i;
                UpgradeHolderAnimator = UpgradeHolder.GetComponent<Animator>();
            }
            else if(i.name == "Panel")
            {
                Panel = i;
            }
        }

        GetPlayerShooting = FindObjectOfType<PlayerShooting>();
        GetPlayer_Monster = FindObjectOfType<Player_Monster>();
        GetPlayerMovement = FindObjectOfType<PlayerMovement>();       
    }

    private void Update()
    {
        switch (Enemies_Killed)
        {
            case 2:
                ChoosingUpgrade(false, true);
                break;
            case 5:
                ChoosingUpgrade(true, false);
                break;
            case 8:
                ChoosingUpgrade(false, true);
                break;
            case 10:
                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if(i.name == "EnemySpawner (1)")
                    {
                        i.SetActive(true);
                    }
                }
                break;
            case 11:
                ChoosingUpgrade(true, false);
                break;
            case 15:
                ChoosingUpgrade(false, true);
                break;
            case 20:
                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if (i.name == "EnemySpawner (2)")
                    {
                        i.SetActive(true);
                    }
                }
                break;
            case 30:
                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if (i.name == "EnemySpawner (3)")
                    {
                        i.SetActive(true);
                    }
                }
                break;
        }
    }

    private void ChoosingUpgrade(bool ControlBool, bool newBool)
    {
        if (Control == ControlBool)
        {
            UpgradeHolder.SetActive(true);
            Panel.SetActive(true);
            if (UpgradeHolder.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    choosing -= 1;
                    if (choosing < RangeChoose[0]) //1
                    {
                        choosing = RangeChoose[0];
                    }
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    choosing += 1;
                    if (choosing > RangeChoose[1]) //6
                    {
                        choosing = RangeChoose[1];
                    }
                }

                foreach (GameObject c in UpgradeSprites)
                {
                    if (c.transform.GetSiblingIndex() == choosing && c.activeInHierarchy)
                    {
                        c.GetComponent<SpriteRenderer>().color = Color.black;
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            switch ((int)choosing)
                            {
                                case 1:
                                    UpgradeHealth(1);
                                    print("Your health increased!");
                                    StopAndContinue(newBool);
                                    break;
                                case 2:
                                    UpgradePlayerSpeed(10f);
                                    print("Your speed increased!");
                                    StopAndContinue(newBool);
                                    break;
                                case 3:
                                    ReduceTime(0.2f, 0.1f);
                                    print("Time Reduced");
                                    StopAndContinue(newBool);
                                    break;
                                case 4:
                                    UpgradeBulletSpeed_Range(0.05f, 0.02f);
                                    print("Faster Bullets");
                                    StopAndContinue(newBool);
                                    break;
                                case 5:
                                    UpgradePlayerDamage(1);
                                    print("Damage increased!");
                                    StopAndContinue(newBool);
                                    break;
                                case 6:
                                    UpgradingSize(new Vector3(0.25f, 0.25f, 0));
                                    print("Bigger bullets!");
                                    StopAndContinue(newBool);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        c.GetComponent<SpriteRenderer>().color = new Color(193f, 193f, 193f, 255);
                    }
                }
            }
        }
    }
    
    private void StopAndContinue(bool newBool)
    {
        UpgradeHolderAnimator.Play("UpgradeHolderBack");
        Panel.GetComponent<Animator>().Play("AfterUpgrade");
        choosing = 0;
        Control = newBool;
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
