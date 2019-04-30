using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public bool forStartMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (forStartMenu == true)
            {
                SceneManager.LoadScene("Level_Scene");
            }           
        }
    }
}
