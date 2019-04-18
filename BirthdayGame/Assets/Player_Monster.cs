using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Monster : MonoBehaviour
{

    private PlayerMovement GetPlayerMovement;
    [HideInInspector] public bool Try_Again = false;
    public float PlayerHealth;

    private LevelManagement GetLevelManagement;

    [HideInInspector] public GameObject TryAgainQuit;

    private void Life_Function()
    {
        GetPlayerMovement.PlayerCanMove = false;
        GetPlayerMovement.PlayerRigidbody2D.velocity = new Vector3(0, 0, 0);
        GetPlayerMovement.gameObject.GetComponent<Collider2D>().enabled = false;

        GetPlayerMovement.PlayerAnimator.Play("Player_Death");
    }

    private void Call_EndGame()
    {
        foreach (GameObject x in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (x.name == "Panel")
            {
                x.SetActive(true);
            }else if(x.name == "TryAgain_Quit")
            {
                TryAgainQuit = x;
                TryAgainQuit.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (PlayerHealth <= 0)
        {
            Life_Function();
            PlayerHealth = 0;
        }

        if (Try_Again)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                GetLevelManagement.ResetVariables();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Application.Quit();
            }
        }
    }

    private void Awake()
    {
        foreach (GameObject x in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if(x.name == "TryAgain_Quit")
            {
                TryAgainQuit = x;
            }
        }

        GetLevelManagement = FindObjectOfType<LevelManagement>();
        GetPlayerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private Collision2D OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (PlayerHealth > 0)
                {
                    GetPlayerMovement.PlayerAnimator.Play("Player_Hurt");
                }
                break;
            case "Items":
                break;
        }

        return collision;
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

    private void PlayerIsHurt()
    {
        GetPlayerMovement.PlayerCanMove = false;
        GetPlayerMovement.PlayerRigidbody2D.velocity = Vector3.zero;
        GetPlayerMovement.gameObject.GetComponent<Collider2D>().enabled = false;
        GetPlayerMovement.PlayerAnimator.Play("Player_Hurt");
    }

    private void PlayerDoneHurt()
    {
        if (PlayerHealth != 0)
        {
            GetPlayerMovement.PlayerCanMove = true;
            GetPlayerMovement.gameObject.GetComponent<Collider2D>().enabled = true;
            GetPlayerMovement.PlayerAnimator.Play("Player_Idle");
        }
    }
}
