using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {

    public int Enemies_Killed;

    private PlayerShooting GetPlayerShooting;
    private Player_Monster GetPlayer_Monster;
    private PlayerMovement GetPlayerMovement;

    float choosing;

    private Animator UpgradeHolderAnimator;

    private GameObject UpgradeHolder;
    public GameObject Panel;
    [SerializeField] private List<GameObject> UpgradeSprites = new List<GameObject>();

    private bool Control;

    private GameObject EnemSpawner1;
    private GameObject EnemSpawner2;

    private float UpgradeCall_Delay = 10f;
    private bool CallUpgrade = false;

    private GameObject ShowAtt;
    private int ShowAttChoosing = -1;
    private Text[] AttTexts = new Text[3];

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
            }else if(i.name == "EnemySpawner (1)")
            {
                EnemSpawner1 = i;
            }else if(i.name == "EnemySpawner (2)")
            {
                EnemSpawner2 = i;
            }else if(i.name == "ShowStats")
            {
                ShowAtt = i;
            }
        }

        GetPlayerShooting = FindObjectOfType<PlayerShooting>();
        GetPlayer_Monster = FindObjectOfType<Player_Monster>();
        GetPlayerMovement = FindObjectOfType<PlayerMovement>();       
    }

    private void Start()
    {
        StartCoroutine(Strengthen_Enemies(10f, 0, -0.05f, -0.05f, -0.05f));
        StartCoroutine(Strengthen_Enemies(20f, 1, 1f, 1f, 2f));
        StartCoroutine(Strengthen_Enemies(10f, 2, 1f, 2f, 1f));
        StartCoroutine(Strengthen_Enemies(25f, 3, 0.08f, 0.05f, 0.05f));

        StartCoroutine(Call_Upgrades());
    }

    private void Update()
    {
        ShowEnt_Attributes();
        print(ShowAttChoosing);
        if (CallUpgrade)
        {
            ChoosingUpgrade();
        }

        switch (Enemies_Killed) //Come up with a better way of calling the "Upgrade - Function".
        {
            case 10:
                EnemSpawner1.SetActive(true);
                break;
            case 20:
                EnemSpawner2.SetActive(true);
                break;
            case 30:
                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if (i.name == "SpecialSpawner")
                    {
                        i.SetActive(true);
                    }
                }
                UpgradeCall_Delay = 7f;
                break;
        }
    }

    private void ChoosingUpgrade()
    {
        if (!GetPlayer_Monster.TryAgainQuit.activeInHierarchy && !ShowAtt.activeInHierarchy)
        {
            UpgradeHolder.SetActive(true);
            Panel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                choosing -= 1;
                if (choosing < 1) //1
                {
                    choosing = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                choosing += 1;
                if (choosing > 6) //6
                {
                    choosing = 6;
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
                                UpgradeHealth(5);
                                StopAndContinue();
                                break;
                            case 2:
                                UpgradePlayerSpeed(10f);
                                StopAndContinue();
                                break;
                            case 3:
                                ReduceTime(0.4f, 0.2f);
                                StopAndContinue();
                                break;
                            case 4:
                                UpgradeBulletSpeed_Range(0.2f, 0.04f);
                                StopAndContinue();
                                break;
                            case 5:
                                UpgradePlayerDamage(2);
                                StopAndContinue();
                                break;
                            case 6:
                                UpgradingSize(new Vector3(0.25f, 0.25f, 0));
                                StopAndContinue();
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
    
    private void ShowEnt_Attributes()
    {
        if (Input.GetKeyDown(KeyCode.O) && !UpgradeHolder.activeSelf && !GetPlayer_Monster.TryAgainQuit.activeInHierarchy && !ShowAtt.activeInHierarchy)
        {
            ShowAtt.SetActive(true);
            Time.timeScale = 0;
            foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (i.name == "NormalMonster_Text")
                {
                    AttTexts[0] = i.GetComponent<Text>();
                    i.SetActive(true);
                }
                else if (i.name == "BossMonster_Text")
                {
                    AttTexts[1] = i.GetComponent<Text>();
                    i.SetActive(true);
                }
                else if (i.name == "PlayerAttButton_Text")
                {
                    AttTexts[2] = i.GetComponent<Text>();
                    i.SetActive(true);
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.O) && !UpgradeHolder.activeSelf && !GetPlayer_Monster.TryAgainQuit.activeInHierarchy && ShowAtt.activeInHierarchy)
        {
            foreach(Text i in AttTexts)
            {
                i.gameObject.SetActive(false);
            }
            Time.timeScale = 1;
            ShowAttChoosing = -1; //Stores previous text settings, fix it. (You know what that means)
            ShowAtt.SetActive(false);
        }

        if (ShowAtt.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShowAttChoosing += 1;
                if (ShowAttChoosing > 3)
                {
                    ShowAttChoosing = 3;
                }else if(ShowAttChoosing < 1)
                {
                    ShowAttChoosing = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShowAttChoosing -= 1;
                if (ShowAttChoosing < 1)
                {
                    ShowAttChoosing = 1;
                }
            }

            foreach(Transform c in ShowAtt.transform)
            {
                if(c.name != "Tiles")
                {
                    if (c.GetSiblingIndex() == ShowAttChoosing)
                    {
                        c.GetComponent<SpriteRenderer>().color = new Color(99f / 255f, 97f / 255f, 97f / 255f, 255f / 255f);

                        if(c.GetComponent<SpriteRenderer>().color == new Color(99f / 255f, 97f / 255f, 97f / 255f, 255f / 255f))
                        {
                            foreach(Transform x in c.transform)
                            {
                                x.gameObject.SetActive(true);

                                switch (c.name)
                                {
                                    case "NormalMobs":
                                        foreach (Transform o in AttTexts[0].transform)
                                        {
                                            o.gameObject.SetActive(true);
                                        }
                                        foreach (Transform o in AttTexts[1].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        foreach (Transform o in AttTexts[2].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        break;
                                    case "BossMobs":
                                        foreach (Transform o in AttTexts[1].transform)
                                        {
                                            o.gameObject.SetActive(true);
                                        }
                                        foreach (Transform o in AttTexts[0].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        foreach (Transform o in AttTexts[2].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        break;
                                    case "Player":
                                        foreach (Transform o in AttTexts[2].transform)
                                        {
                                            o.gameObject.SetActive(true);
                                        }
                                        foreach (Transform o in AttTexts[1].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        foreach (Transform o in AttTexts[0].transform)
                                        {
                                            o.gameObject.SetActive(false);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        c.GetComponent<SpriteRenderer>().color = new Color(184f / 255f, 177f / 255f, 177f / 255f, 255f / 255f);
                        foreach (Transform x in c.transform)
                        {
                            x.gameObject.SetActive(false);
                        }
                    }
                }
            }           
        }
    }

    private void StopAndContinue()
    {
        UpgradeHolderAnimator.Play("UpgradeHolderBack");
        Panel.GetComponent<Animator>().Play("AfterUpgrade");
        choosing = 0;
        CallUpgrade = false;
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
        //Default is 0.2f
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

    #region Use this Function to upgrade Enemy Attributes. 
    private void LidlFinderSpawner(int Attr, float increaseEagle, float increaseFrog, float increaseOpossum)
    {
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            switch (i.name)
            {
                case "EnemySpawner":
                    if (i.activeInHierarchy)
                    {
                        EnemySpawning.EagleStuff[Attr] += increaseEagle;
                    }
                    break;
                case "EnemySpawner (1)":
                    if (i.activeInHierarchy)
                    {
                        EnemySpawning.FrogStuff[Attr] += increaseFrog;
                    }
                    break;
                case "EnemySpawner (2)":
                    if (i.activeInHierarchy)
                    {
                        EnemySpawning.OpossumStuff[Attr] += increaseOpossum;
                    }
                    break;
            }            
        }
    }
    #endregion

    private IEnumerator Strengthen_Enemies(float TimeUpgrade, int Attribute, float increaseEagle, float increaseFrog, float increaseOpossum)
    {
        while (true)
        {
            yield return new WaitForSeconds(TimeUpgrade);
            LidlFinderSpawner(Attribute, increaseEagle, increaseFrog, increaseOpossum);
        }
    }   

    private IEnumerator Call_Upgrades()
    {
        while (true)
        {
            yield return new WaitForSeconds(UpgradeCall_Delay);
            CallUpgrade = true;
        }
    }
}