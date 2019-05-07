using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public AudioClip impact;
    AudioSource audioSource;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            audioSource.PlayOneShot(impact);
            SceneManager.LoadScene("Level_Scene");
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
