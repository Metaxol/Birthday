using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

    private Text End_Text;
    private Player_Monster GetPlayer_Monster;

    private LevelManagement GetLevelManagement;

    private void EndGame_Text() //This is lidl code. (Make it better, maybe)
    {
        foreach (GameObject x in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (x.name == "TryAgain&QuitGame")
            {
                End_Text = x.GetComponent<Text>();
                x.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        GetLevelManagement = FindObjectOfType<LevelManagement>();
        EndGame_Text();
        GetPlayer_Monster = FindObjectOfType<Player_Monster>();
    }

    private void PlayEndText()
    {
        if(GetLevelManagement.Enemies_Killed == 1)
        {
            StartCoroutine(TextScroll("Press 'I' to quit the game! Press 'U' to try again!" + "\n" + "You killed " + GetLevelManagement.Enemies_Killed + " Enemy!"));
        }
        else
        {
            StartCoroutine(TextScroll("Press 'I' to quit the game! Press 'U' to try again!" + "\n" + "You killed " + GetLevelManagement.Enemies_Killed + " Enemies!"));
        }
    }

    private void ChooseOption()
    {
        GetPlayer_Monster.Try_Again = true;
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        End_Text.text = "";
        while (letter < lineOfText.Length - 1)
        {
            End_Text.text += lineOfText[letter];
            letter += 1;
            //Play sound when the text is scrolling.
            yield return new WaitForSeconds(0);
        }
        End_Text.text = lineOfText;
    }   
}