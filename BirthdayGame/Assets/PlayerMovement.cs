using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    [HideInInspector] public Rigidbody2D PlayerRigidbody2D;
    [HideInInspector] public bool PlayerCanMove = true;
    [HideInInspector] public Animator PlayerAnimator;
    public float PlayerMovSpeed;

    private void Move_Player(float MoveSpeed) //This function is for moving the player.
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (PlayerCanMove)
        {
            PlayerRigidbody2D.velocity = new Vector3(x * MoveSpeed * Time.deltaTime, y * MoveSpeed * Time.deltaTime, 0);
            PlayerAnimator.SetBool("isWalking", true);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Have to test this code. (Activates Idle Animation after having walked)
        Vector2 mag = PlayerRigidbody2D.velocity;
        if(mag.magnitude == 0) //Have to check on another PC if this is gonna work.
        {
            PlayerAnimator.SetBool("isWalking", false); 
        }
        //Test.

        Move_Player(PlayerMovSpeed); //Parameter decides MoveSpeed.         
    }
    
    private void Awake()
    {
        PlayerAnimator = gameObject.GetComponent<Animator>();
        PlayerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
}