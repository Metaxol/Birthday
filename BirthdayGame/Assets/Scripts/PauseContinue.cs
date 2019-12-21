using UnityEngine;
using UnityEngine.UI;

public class PauseContinue : MonoBehaviour {

    private EndGame GetEndGame;

    private GameObject UpgradeText;

    private void Awake()
    {
        GetEndGame = FindObjectOfType<EndGame>();

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
