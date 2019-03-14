using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D PlayerRigidbody2D;
    private bool PlayerCanMove = true;

    private void Move_Player(float MoveSpeed) //This function is for moving the player.
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (PlayerCanMove)
        {
            PlayerRigidbody2D.velocity = new Vector3(x * MoveSpeed * Time.deltaTime, y * MoveSpeed * Time.deltaTime, 0);
        }
    }
   
    private void FixedUpdate()
    {
        Move_Player(100f); //Parameter decides MoveSpeed. 
    }
    
    private void Awake()
    {
        PlayerRigidbody2D = this.gameObject.GetComponent<Rigidbody2D>();
    }
}