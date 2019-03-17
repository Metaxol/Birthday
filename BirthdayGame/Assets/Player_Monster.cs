using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Monster : MonoBehaviour {

    private PlayerMovement GetPlayerMovement;
    [HideInInspector] public bool Try_Again = false;

    private void Life_Function()
    {
        GetPlayerMovement.PlayerCanMove = false;
        GetPlayerMovement.PlayerRigidbody2D.velocity = new Vector3(0, 0, 0);
        GetPlayerMovement.gameObject.GetComponent<Collider2D>().enabled = false;

        GetPlayerMovement.PlayerAnimator.Play("Player_Death");
    }

    private void Call_EndGame()
    {
        foreach(GameObject x in  Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if(x.name == "Panel" || x.name == "TryAgain_Quit")
            {
                x.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (Try_Again)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                SceneManager.LoadScene("Level_Scene");
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Application.Quit();
            }
        }
    }

    private void Awake()
    {
        GetPlayerMovement = this.gameObject.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Life_Function();
                break;
            case "Items":
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Enemy":
                Life_Function();
                break;
            case "Items":
                break;
        }
    }
}
