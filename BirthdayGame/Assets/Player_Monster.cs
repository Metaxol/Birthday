using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Monster : MonoBehaviour
{

    private PlayerMovement GetPlayerMovement;
    [HideInInspector] public bool Try_Again = false;
    public int PlayerHealth;

    private Text HealthText;

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
            if (x.name == "Panel" || x.name == "TryAgain_Quit")
            {
                x.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if(PlayerHealth > 0)
        {
            HealthText.text = "Health: " + PlayerHealth;
        }
        else if (PlayerHealth <= 0)
        {
            Life_Function();
            HealthText.text = "Health: " + 0;
        }

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
        HealthText = GameObject.Find("HealthText").GetComponent<Text>();
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
