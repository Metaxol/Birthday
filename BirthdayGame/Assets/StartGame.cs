using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    private GameObject StartGameButton;
    private GameObject HelpButton;

    private bool canChoose;

    private void Start()
    {
        StartGameButton = GameObject.Find("StartGame");
        HelpButton = GameObject.Find("Help");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && canChoose)
        {
            StartGameButton.GetComponent<Image>().color = new Color(25f /255, 155f / 255, 167f / 255, 255f / 255);
            HelpButton.GetComponent<Image>().color = new Color(68f / 255, 239f / 255, 255f / 255, 255f / 255);
        }else if (Input.GetKeyDown(KeyCode.DownArrow) && canChoose)
        {
            StartGameButton.GetComponent<Image>().color = new Color(68f / 255, 239f / 255, 255f / 255, 255f / 255);
            HelpButton.GetComponent<Image>().color = new Color(25f / 255, 155f / 255, 167f / 255, 255f / 255);
        }

        if(StartGameButton.GetComponent<Image>().color == new Color(25f / 255, 155f / 255, 167f / 255, 255f / 255) && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level_Scene");
        }else if(HelpButton.GetComponent<Image>().color == new Color(25f / 255, 155f / 255, 167f / 255, 255f / 255) && Input.GetKeyDown(KeyCode.Return))
        {
            print("Help");
        }
    }
}
