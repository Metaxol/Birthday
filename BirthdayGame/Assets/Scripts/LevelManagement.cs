﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {

    public int Enemies_Killed;

    private PlayerShooting GetPlayerShooting;
    private Player_Monster GetPlayer_Monster;
    private PlayerMovement GetPlayerMovement;
    [HideInInspector] public BossSpawner GetBossSpawner;

    float choosing;

    private Animator UpgradeHolderAnimator;

    [HideInInspector] public GameObject UpgradeHolder;
    public GameObject Panel;
    [SerializeField] private List<GameObject> UpgradeSprites = new List<GameObject>();

    private bool Control;

    private GameObject EnemSpawner1;
    private GameObject EnemSpawner2;

    private float UpgradeCall_Delay = 10f;
    private bool CallUpgrade = false;

    private GameObject ShowAtt;
    private int ShowAttChoosing = -1;
    [SerializeField] private Text[] AttTexts = new Text[3];

    public AudioClip UpgradeAppeared;
    public AudioClip impact;
    AudioSource audioSource;
    private bool PlayAudioOnce = true;
    public AudioClip UpgradeChosen;

    public void ResetVariables()
    {
        Enemies_Killed = 0;
        GetPlayer_Monster.PlayerHealth = 3;
        EnemyFollow.PlayerToEnemy_Damage = 1;
        GetPlayerMovement.PlayerMovSpeed = 100;
        BulletController.BulletRange = 0.5f;
        GetPlayerShooting.BulletSpeed = 0.1f;
        GetPlayerShooting.Player_Bullets.transform.localScale = new Vector3(2, 2, 0);
        GetPlayerShooting.cooldown = 2;
        GetPlayerShooting.Delay = 1;
        EnemySpawning.SetOriginalState();
        GetBossSpawner.SetOriginalState();
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            switch (i.name)
            {
                case "UpgradeHolder_Tiles":
                    UpgradeSprites.Add(i.gameObject);
                    break;
                case "UpgradeHolder":
                    UpgradeHolder = i;
                    UpgradeHolderAnimator = UpgradeHolder.GetComponent<Animator>();
                    break;
                case "Panel":
                    Panel = i;
                    break;
                case "EnemySpawner (1)":
                    EnemSpawner1 = i;
                    break;
                case "EnemySpawner (2)":
                    EnemSpawner2 = i;
                    break;
                case "ShowStats":
                    ShowAtt = i;
                    break;
                case "NormalMonster_Text":
                    AttTexts[0] = i.GetComponent<Text>();
                    break;
                case "BossMonster_Text":
                    AttTexts[1] = i.GetComponent<Text>();
                    break;
                case "PlayerAttButton_Text":
                    AttTexts[2] = i.GetComponent<Text>();
                    break;
                case "SpecialSpawner":
                    GetBossSpawner = i.GetComponent<BossSpawner>();
                    break;
            }
        }

        GetPlayerShooting = FindObjectOfType<PlayerShooting>();
        GetPlayer_Monster = FindObjectOfType<Player_Monster>();
        GetPlayerMovement = FindObjectOfType<PlayerMovement>();       
    }

    private void Start()
    {
        StartCoroutine(Strengthen_Enemies(10f, 0, -0.4f, -0.4f, -0.4f));

        GetBossSpawner.SetOriginalState();

        StartCoroutine(Call_Upgrades());

        StartCoroutine(SpawnItems());
    }

    private void Update()
    {
        ShowEnt_Attributes();
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            switch (i.name)
            { 
                case "EagleHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.EagleStuff[2], 1).ToString();
                    break;
                case "EagleDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.EagleStuff[1], 1).ToString();
                    break;
                case "EagleSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.EagleStuff[3], 1).ToString();
                    break;                   
                case "OpossumHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.OpossumStuff[2], 1).ToString();
                    break;
                case "OpossumDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.OpossumStuff[1], 1).ToString();
                    break;
                case "OpossumSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.OpossumStuff[3], 1).ToString();
                    break;
                case "FrogHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.FrogStuff[2], 1).ToString();
                    break;
                case "FrogDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.FrogStuff[1], 1).ToString();
                    break;
                case "FrogSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(EnemySpawning.FrogStuff[3], 1).ToString();
                    break;
                    
                case "EagleBossHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.EagleBoss.GetComponent<EnemyFollow>().Enemy_Health, 1).ToString();
                    break;
                case "EagleBossDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.EagleBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage, 1).ToString();
                    break;
                case "EagleBossSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.EagleBoss.GetComponent<EnemyFollow>().EnemySpeed, 1).ToString();
                    break;
                case "OpossumBossHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.OpossumBoss.GetComponent<EnemyFollow>().Enemy_Health, 1).ToString();
                    break;
                case "OpossumBossDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage, 1).ToString();
                    break;
                case "OpossumBossSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.OpossumBoss.GetComponent<EnemyFollow>().EnemySpeed, 1).ToString();
                    break;
                case "FrogBossHealth_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.FrogBoss.GetComponent<EnemyFollow>().Enemy_Health, 1).ToString();
                    break;
                case "FrogBossDamage_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.FrogBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage, 1).ToString();
                    break;
                case "FrogBossSpeed_Text":
                    i.GetComponent<Text>().text = System.Math.Round(GetBossSpawner.FrogBoss.GetComponent<EnemyFollow>().EnemySpeed, 1).ToString();
                    break;
                    

                case "PlayerHealthText":
                    i.GetComponent<Text>().text = System.Math.Round(GetPlayer_Monster.PlayerHealth, 1).ToString();
                    break;
                case "PlayerDamageText":
                    i.GetComponent<Text>().text = EnemyFollow.PlayerToEnemy_Damage.ToString();
                    break;
                case "PlayerSpeedText":
                    i.GetComponent<Text>().text = GetPlayerMovement.PlayerMovSpeed.ToString();
                    break;
                case "BulletStuffText":
                    i.GetComponent<Text>().text = System.Math.Round((GetPlayerShooting.BulletSpeed + BulletController.BulletRange), 1).ToString();
                    break;
                case "BulletSizeText":
                    i.GetComponent<Text>().text = GetPlayerShooting.Player_Bullets.transform.localScale.x.ToString();
                    break;
                case "TimeReduceText":
                    i.GetComponent<Text>().text = System.Math.Round((GetPlayerShooting.cooldown + GetPlayerShooting.Delay), 1).ToString(); //Some bugs, fix them.
                    break;
                default:
                    break;                   
            }
            
        }

        if (CallUpgrade)
        {
            ChoosingUpgrade();
        }

        switch (Enemies_Killed)
        {
            case 6:
                EnemSpawner1.SetActive(true);
                break;
            case 12:
                EnemSpawner2.SetActive(true);
                break;
            case 20:
                GetBossSpawner.gameObject.SetActive(true);
                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if (i.name == "NewBackGround")
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
            if (PlayAudioOnce)
            {
                audioSource.PlayOneShot(UpgradeAppeared);
                PlayAudioOnce = false;
            }
            UpgradeHolder.SetActive(true);
            Panel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                audioSource.PlayOneShot(impact);
                choosing -= 1;
                if (choosing < 1) //1
                {
                    choosing = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                audioSource.PlayOneShot(impact);
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
                                UpgradeHealth((int)Mathf.Round(GetBossSpawner.FrogBoss.GetComponent<EnemyFollow>().Enemy_Health));
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
                                UpgradePlayerDamage((int)Mathf.Round(GetBossSpawner.OpossumBoss.GetComponent<EnemyFollow>().EnemyToPlayer_Damage));
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
            audioSource.PlayOneShot(impact);
            ShowAtt.SetActive(true);
            foreach(Text i in AttTexts)
            {
                i.gameObject.SetActive(true);
            }

            foreach(Transform i in AttTexts[0].transform) //Most Lidl code I've written... change that if you have time...
            {
                i.gameObject.SetActive(false);
            }
            foreach (Transform i in AttTexts[1].transform)
            {
                i.gameObject.SetActive(false);
            }
            foreach (Transform i in AttTexts[2].transform)
            {
                i.gameObject.SetActive(false);
            }
            Time.timeScale = 0;
            
        }
        else if(Input.GetKeyDown(KeyCode.O) && !UpgradeHolder.activeSelf && !GetPlayer_Monster.TryAgainQuit.activeInHierarchy && ShowAtt.activeInHierarchy)
        {
            foreach (Text i in AttTexts)
            {
                i.gameObject.SetActive(false);
            }
            Time.timeScale = 1;
            ShowAttChoosing = -1;
            ShowAtt.SetActive(false);
        }

        if (ShowAtt.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                audioSource.PlayOneShot(impact);
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
                audioSource.PlayOneShot(impact);
                ShowAttChoosing -= 1;
                if (ShowAttChoosing < 1)
                {
                    ShowAttChoosing = 1;
                }
            }

            foreach(Transform c in ShowAtt.transform)
            {
                if (c.name != "Tiles")
                {
                    if (c.GetSiblingIndex() == ShowAttChoosing)
                    {
                        c.GetComponent<SpriteRenderer>().color = new Color(99f / 255f, 97f / 255f, 97f / 255f, 255f / 255f);

                        foreach (Transform x in c.transform)
                        {
                            x.gameObject.SetActive(true);
                        }
                    }else                                     
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

        if (ShowAttChoosing == 1)
        {
            HelpShowAttTexts(0, true, false, false);
        }
        else if (ShowAttChoosing == 2)
        {
            HelpShowAttTexts(1, false, true, false);
        }
        else if (ShowAttChoosing == 3)
        {
            HelpShowAttTexts(2, false, false, true);
        }
    }

    private void HelpShowAttTexts(int alwaysTrue, bool true1, bool true2, bool true3)
    {
        AttTexts[alwaysTrue].gameObject.SetActive(true);

        foreach (Transform o in AttTexts[0].transform)
        {
            o.gameObject.SetActive(true1);
        }
        foreach (Transform o in AttTexts[1].transform)
        {
            o.gameObject.SetActive(true2);
        }
        foreach (Transform o in AttTexts[2].transform)
        {
            o.gameObject.SetActive(true3);
        }
    }

    private void StopAndContinue()
    {
        audioSource.PlayOneShot(UpgradeChosen);
        PlayAudioOnce = true;
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

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(UpgradeCall_Delay + 1f); //use "UpgradeCall_Delay", is good time, +2f so it's not the same.
            SpawnObject();
        }
    }

    [SerializeField] private GameObject spawn1_Item;
    [SerializeField] private GameObject spawn2_Item;

    private void SpawnObject()
    {
        float x = Random.Range(-5.568901f, 5.49f);
        float y = Random.Range(8.021715f, 0.11f);

        int randomItem = Random.Range(1, 3);

        if(randomItem == 1)
        {
            Instantiate(spawn1_Item, new Vector3(x, y, 0), Quaternion.identity);
        }
        else if(randomItem == 2)
        {
            Instantiate(spawn2_Item, new Vector3(x, y, 0), Quaternion.identity);
        }
    }
}