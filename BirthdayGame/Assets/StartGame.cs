using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Level_Scene");
        }
    }
}
