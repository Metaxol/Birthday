using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [HideInInspector] public Rigidbody2D PlayerRigidbody2D;
    [HideInInspector] public bool PlayerCanMove = true;
    [HideInInspector] public Animator PlayerAnimator;

    private Vector3 LastPosition;

    private void Move_Player(float MoveSpeed) //This function is for moving the player.
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (PlayerCanMove)
        {
            PlayerRigidbody2D.velocity = new Vector3(x * MoveSpeed * Time.deltaTime, y * MoveSpeed * Time.deltaTime, 0);
            PlayerAnimator.SetBool("isWalking", true);

            if (transform.position.x < LastPosition.x)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                LastPosition.x = transform.position.x;
            }

            else if (transform.position.x > LastPosition.x)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                LastPosition.x = transform.position.x;
            }
        }
    }
    
    private void FixedUpdate()
    {
        //Have to test this code. (Activates Idle Animation)
        Vector2 mag = PlayerRigidbody2D.velocity;
        if(mag.magnitude == 0) //Have to check on another PC if this is gonna work.
        {
            PlayerAnimator.SetBool("isWalking", false); 
        }
        //Test.

        Move_Player(100f); //Parameter decides MoveSpeed.         
    }
    
    private void Awake()
    {
        LastPosition = transform.position;
        PlayerAnimator = this.gameObject.GetComponent<Animator>();
        PlayerRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
}