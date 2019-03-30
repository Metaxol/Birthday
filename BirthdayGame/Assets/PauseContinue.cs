using UnityEngine;
using UnityEngine.UI;

public class PauseContinue : MonoBehaviour {

    private EndGame GetEndGame;
    private LevelManagement GetLevelManagement;

    private GameObject UpgradeText;

    private void Awake()
    {
        GetEndGame = FindObjectOfType<EndGame>();
        GetLevelManagement = FindObjectOfType<LevelManagement>();

        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if(i.name == "TryAgain_Quit")
            {
                GetEndGame = i.GetComponent<EndGame>();
            }
            else if (i.name == "Canvas")
            {
                UpgradeText = i.transform.GetChild(1).gameObject;
            }
        }
    }

    private void DeActivateChildren(bool[] UpgradeAvailable)
    {
        foreach(Transform child in transform)
        {
            switch (child.GetSiblingIndex())
            {
                case 1:
                    child.gameObject.SetActive(UpgradeAvailable[0]);
                    break;
                case 2:
                    child.gameObject.SetActive(UpgradeAvailable[1]);
                    break;
                case 3:
                    child.gameObject.SetActive(UpgradeAvailable[2]);
                    break;
                case 4:
                    child.gameObject.SetActive(UpgradeAvailable[3]);
                    break;
                case 5:
                    child.gameObject.SetActive(UpgradeAvailable[4]);
                    break;
                case 6:
                    child.gameObject.SetActive(UpgradeAvailable[5]);
                    break;
            }
        }
    }

    private void Update()
    {
        switch (GetLevelManagement.Enemies_Killed)
        {
            case 2:
                DeActivateChildren(new bool[] { true, false, false, false, false, false });
                GetLevelManagement.RangeChoose = new float[] { 1, 1 };
                break;
            case 5:
                DeActivateChildren(new bool[] { true, true, false, false, false, false });
                GetLevelManagement.RangeChoose = new float[] { 1, 2 };
                break;
            case 8:
                DeActivateChildren(new bool[] { true, true, true, false, false, false });
                GetLevelManagement.RangeChoose = new float[] { 1, 3 };
                break;
            case 11:
                DeActivateChildren(new bool[] { true, true, true, true, false, false });
                GetLevelManagement.RangeChoose = new float[] { 1, 4 };
                break;
            case 15:
                DeActivateChildren(new bool[] { true, true, true, true, true, false });
                GetLevelManagement.RangeChoose = new float[] { 1, 5 };
                break;
            default:
                DeActivateChildren(new bool[] { true, true, true, true, true, true });
                GetLevelManagement.RangeChoose = new float[] { 1, 6 };
                break;
        }
    }

    private void StopGame()
    {
        Time.timeScale = 0;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;        
        gameObject.SetActive(false);
    }

    void DeActivateText()
    {
        UpgradeText.SetActive(false);
    }

    private void StartText()
    {
        UpgradeText.SetActive(true);
        StartCoroutine(GetEndGame.TextScroll("Choose your Upgrade!", UpgradeText.GetComponent<Text>()));
    }
}
