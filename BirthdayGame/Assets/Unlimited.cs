using UnityEngine;
using UnityEngine.UI;

public class Unlimited : MonoBehaviour {

    private EndGame GetEndGame;
    private LevelManagement GetLevelManagement;

    private Text Unlimited_Text;

    private bool ControlBool = false;

    private void Start()
    {
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if(i.name == "TryAgain_Quit")
            {
                GetEndGame = i.GetComponent<EndGame>();
            }else if(i.name == "Unlimited?")
            {
                Unlimited_Text = i.GetComponent<Text>();
            }else if(i.name == "LevelManager")
            {
                GetLevelManagement = i.GetComponent<LevelManagement>();
            }
        }      
        
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (ControlBool)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                Application.Quit();
            }else if (Input.GetKeyDown(KeyCode.U))
            {
                //Start "Unlimited - Mode".
                GetLevelManagement.UnlimitedControl = true;
                gameObject.GetComponent<Animator>().Play("UnlimitedBack");
            }
        }
    }

    private void ResumePanel()
    {
        GetLevelManagement.Panel.GetComponent<Animator>().Play("AfterUpgrade");
    }

    private void StartText()
    {
       StartCoroutine(GetEndGame.TextScroll("You defeated 100 Enemies! Press 'U' to continue in 'Unlimited Mode', press 'I' to close the game.", Unlimited_Text));
    }
}
